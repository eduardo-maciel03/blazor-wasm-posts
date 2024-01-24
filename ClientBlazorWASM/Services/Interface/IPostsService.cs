using ClientBlazorWASM.Models;

namespace ClientBlazorWASM.Services.Interface
{
    public interface IPostsService
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPost(int id);
        public Task<Post> CriarPost(Post post);
        public Task<Post> AtualizarPost(int id, Post post);
        public Task<bool> DeletarPost(int id);
        public Task<string> UploadImages(MultipartFormDataContent content);
    }
}
