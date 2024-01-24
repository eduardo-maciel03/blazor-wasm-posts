namespace ClientBlazorWASM.Models
{
    public class RespostaRegistro
    {
        public bool registroCorreto { get; set; }
        public IEnumerable<string> Erros { get; set; }
    }
}
