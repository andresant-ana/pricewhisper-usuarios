using System.Threading.Tasks;
using Infrastructure.ExternalServices.Models;

namespace Core.Interfaces.Services
{
    public interface ICNPJaService
    {
        Task<CNPJaResponse?> ConsultarCNPJ(string cnpj);
    }
}
