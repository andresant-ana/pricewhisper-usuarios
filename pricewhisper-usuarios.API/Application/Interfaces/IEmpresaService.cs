using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEmpresaService
    {
        Task<IEnumerable<EmpresaDto>> GetAllAsync();
        Task<EmpresaDto?> GetByIdAsync(int id);
        Task AddAsync(EmpresaDto empresaDto);
        Task UpdateAsync(EmpresaDto empresaDto);
        Task DeleteAsync(int id);
    }
}