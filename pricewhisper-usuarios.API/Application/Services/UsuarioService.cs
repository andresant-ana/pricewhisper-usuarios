using Application.DTOs;
using Application.Interfaces;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using System.Linq;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IEmpresaRepository empresaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            // Mapeamento após materializar os dados na memória
            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                UsuarioId = u.UsuarioId,
                Nome = u.Nome,
                NomeUsuario = u.NomeUsuario,
                EmpresaId = u.EmpresaId,
                RazaoSocialEmpresa = u.Empresa?.RazaoSocial
            });

            return usuariosDto;
        }

        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return null;

            var usuarioDto = new UsuarioDto
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                NomeUsuario = usuario.NomeUsuario,
                EmpresaId = usuario.EmpresaId,
                RazaoSocialEmpresa = usuario.Empresa?.RazaoSocial
            };

            return usuarioDto;
        }

        public async Task AddAsync(UsuarioDto usuarioDto)
        {
            var empresa = await _empresaRepository.GetByIdAsync(usuarioDto.EmpresaId);
            if (empresa == null)
                throw new Exception("Empresa não encontrada.");

            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                NomeUsuario = usuarioDto.NomeUsuario,
                Senha = usuarioDto.Senha,
                EmpresaId = usuarioDto.EmpresaId
            };

            await _usuarioRepository.AddAsync(usuario);

            // Atualizar o UsuarioId e RazaoSocialEmpresa no DTO
            usuarioDto.UsuarioId = usuario.UsuarioId;
            usuarioDto.RazaoSocialEmpresa = empresa.RazaoSocial;
        }

        public async Task UpdateAsync(UsuarioDto usuarioDto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioDto.UsuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            usuario.Nome = usuarioDto.Nome;
            usuario.NomeUsuario = usuarioDto.NomeUsuario;

            // Atualiza a senha somente se for fornecida
            if (!string.IsNullOrEmpty(usuarioDto.Senha))
            {
                usuario.Senha = usuarioDto.Senha;
            }

            usuario.EmpresaId = usuarioDto.EmpresaId;

            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            await _usuarioRepository.DeleteAsync(usuario);
        }
    }
}