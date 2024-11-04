using Application.DTOs;
using Application.Interfaces;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System.Linq;

namespace Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ICNPJaService _cnpjaService;

        public EmpresaService(IEmpresaRepository empresaRepository, ICNPJaService cnpjaService)
        {
            _empresaRepository = empresaRepository;
            _cnpjaService = cnpjaService;
        }

        public async Task<IEnumerable<EmpresaDto>> GetAllAsync()
        {
            var empresas = await _empresaRepository.GetAllAsync();

            var empresasDto = empresas.Select(e => new EmpresaDto
            {
                EmpresaId = e.EmpresaId,
                CNPJ = e.CNPJ,
                RazaoSocial = e.RazaoSocial,
                NomeFantasia = e.NomeFantasia,
                Usuarios = e.Usuarios.Select(u => new UsuarioDto
                {
                    UsuarioId = u.UsuarioId,
                    Nome = u.Nome,
                    NomeUsuario = u.NomeUsuario,
                    EmpresaId = u.EmpresaId,
                    RazaoSocialEmpresa = e.RazaoSocial
                }).ToList()
            });

            return empresasDto;
        }

        public async Task<EmpresaDto?> GetByIdAsync(int id)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id);
            if (empresa == null)
                return null;

            var empresaDto = new EmpresaDto
            {
                EmpresaId = empresa.EmpresaId,
                CNPJ = empresa.CNPJ,
                RazaoSocial = empresa.RazaoSocial,
                NomeFantasia = empresa.NomeFantasia,
                Usuarios = empresa.Usuarios.Select(u => new UsuarioDto
                {
                    UsuarioId = u.UsuarioId,
                    Nome = u.Nome,
                    NomeUsuario = u.NomeUsuario,
                    EmpresaId = u.EmpresaId,
                    RazaoSocialEmpresa = empresa.RazaoSocial
                }).ToList()
            };

            return empresaDto;
        }

        public async Task AddAsync(EmpresaDto empresaDto)
        {
            var cnpjResponse = await _cnpjaService.ConsultarCNPJ(empresaDto.CNPJ);
            if (cnpjResponse == null || cnpjResponse.Company == null)
                throw new Exception("CNPJ inválido ou não encontrado.");

            var empresa = new Empresa
            {
                CNPJ = empresaDto.CNPJ,
                RazaoSocial = empresaDto.RazaoSocial,
                NomeFantasia = empresaDto.NomeFantasia
            };

            await _empresaRepository.AddAsync(empresa);

            empresaDto.EmpresaId = empresa.EmpresaId;
        }

        public async Task UpdateAsync(EmpresaDto empresaDto)
        {
            var empresa = await _empresaRepository.GetByIdAsync(empresaDto.EmpresaId);
            if (empresa == null)
                throw new Exception("Empresa não encontrada.");

            var cnpjResponse = await _cnpjaService.ConsultarCNPJ(empresaDto.CNPJ);
            if (cnpjResponse == null || cnpjResponse.Company == null)
                throw new Exception("CNPJ inválido ou não encontrado.");

            empresa.CNPJ = empresaDto.CNPJ;
            empresa.RazaoSocial = empresaDto.RazaoSocial;
            empresa.NomeFantasia = empresaDto.NomeFantasia;

            await _empresaRepository.UpdateAsync(empresa);
        }

        public async Task DeleteAsync(int id)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id);
            if (empresa == null)
                throw new Exception("Empresa não encontrada.");

            await _empresaRepository.DeleteAsync(empresa);
        }
    }
}