using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}