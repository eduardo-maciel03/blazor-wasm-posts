using System.ComponentModel.DataAnnotations;

namespace ClientBlazorWASM.Models
{
    public class UsuarioAutenticacao
    {
        [Required(ErrorMessage = "O usuário é obrigatório")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
    }
}
