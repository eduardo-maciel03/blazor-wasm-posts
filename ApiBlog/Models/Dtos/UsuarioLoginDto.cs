using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }
    }
}
