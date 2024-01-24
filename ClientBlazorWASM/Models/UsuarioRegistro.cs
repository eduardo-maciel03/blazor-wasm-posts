﻿using System.ComponentModel.DataAnnotations;

namespace ClientBlazorWASM.Models
{
    public class UsuarioRegistro
    {
        [Required(ErrorMessage = "O usuário é obrigatório")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
    }
}
