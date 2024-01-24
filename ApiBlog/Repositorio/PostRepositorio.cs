using ApiBlog.Data;
using ApiBlog.Models;
using ApiBlog.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Repositorio
{
    public class PostRepositorio : IPostRepositorio
    {
        private readonly ApplicationDbContext _context;

        public PostRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AtualizarPost(Post post)
        {
            var dataAgora = DateTime.Now.ToString();
            post.DataAtualizacao = dataAgora;
            var imagemContext = _context.Post.AsNoTracking().FirstOrDefault(c => c.Id == post.Id);

            if(post.RotaImagem == null)
            {
                post.RotaImagem = imagemContext.RotaImagem;
            }

            _context.Post.Update(post);
            return Guardar();
        }

        public bool CriarPost(Post post)
        {
            var dataAgora = DateTime.Now.ToString();
            post.DataCriacao = dataAgora;

            _context.Post.Add(post);
            return Guardar();
        }

        public bool DeletarPost(Post post)
        {
            _context.Post.Remove(post);
            return Guardar();
        }

        public bool ExistePost(string nome)
        {
            bool resultado = _context.Post.Any(c => c.Titulo.ToLower().Trim() == nome.ToLower().Trim());
            return resultado;
        }

        public bool ExistePost(int id)
        {
            return _context.Post.Any(c => c.Id == id);
        }

        public Post GetPost(int id)
        {
            return _context.Post.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Post> GetPosts()
        {
            return _context.Post.OrderBy(c => c.Id).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
