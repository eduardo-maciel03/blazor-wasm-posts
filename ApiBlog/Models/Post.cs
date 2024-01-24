using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descricao { get; set; }
        public string? RotaImagem { get; set; }
        [Required]
        public string Etiqueta { get; set; }
        public string? DataCriacao { get; set; }
        public string? DataAtualizacao { get; set; }
    }
}
