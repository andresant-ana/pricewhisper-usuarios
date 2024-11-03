using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto?> GetByIdAsync(int id);
        Task AddAsync(UsuarioDto usuarioDto);
        Task UpdateAsync(UsuarioDto usuarioDto);
        Task DeleteAsync(int id);
    }
}