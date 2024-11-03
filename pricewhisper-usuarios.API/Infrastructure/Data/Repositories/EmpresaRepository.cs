using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly OracleDbContext _context;

        public EmpresaRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _context.Empresas
                .Include(e => e.Usuarios)
                .ToListAsync();
        }

        public async Task<Empresa?> GetByIdAsync(int id)
        {
            return await _context.Empresas
                .Include(e => e.Usuarios)
                .FirstOrDefaultAsync(e => e.EmpresaId == id);
        }

        public async Task AddAsync(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Empresa empresa)
        {
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Empresas.AnyAsync(e => e.EmpresaId == id);
        }
    }
}