using Core.Domain.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
        Task<bool> ExistsAsync(int id);
    }
}