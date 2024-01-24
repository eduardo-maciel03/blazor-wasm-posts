namespace ClientBlazorWASM.Models
{
    public class RespostaAutenticacao
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public Usuario Usuario { get; set; }
    }
}
