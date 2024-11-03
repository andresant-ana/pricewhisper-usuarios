using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [NotMapped]
        public string? RazaoSocialEmpresa { get; set; }
    }
}