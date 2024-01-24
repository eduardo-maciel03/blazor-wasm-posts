using ApiBlog.Models;

namespace ApiBlog.Repositorio.IRepositorio
{
    public interface IPostRepositorio
    {
        ICollection<Post> GetPosts();
        Post GetPost(int id);
        bool ExistePost(string nome);
        bool ExistePost(int id);
        bool CriarPost(Post post);
        bool AtualizarPost(Post post);
        bool DeletarPost(Post post);
        bool Guardar();
    }
}
