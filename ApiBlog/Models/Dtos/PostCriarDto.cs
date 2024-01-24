using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models.Dtos
{
    public class PostCriarDto
    {
        [Required(ErrorMessage = "O título é obrigatório!")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória!")]
        public string Descricao { get; set; }
        public string? RotaImagem { get; set; }
        [Required(ErrorMessage = "As etiquetas são obrigatórias!")]
        public string Etiqueta { get; set; }
        public string? DataCriacao { get; set; }
    }
}
