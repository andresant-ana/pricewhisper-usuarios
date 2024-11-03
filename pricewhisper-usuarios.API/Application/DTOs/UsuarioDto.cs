using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Senha { get; set; }

        public int EmpresaId { get; set; }
        public string? RazaoSocialEmpresa { get; set; } // Opcional
    }
}