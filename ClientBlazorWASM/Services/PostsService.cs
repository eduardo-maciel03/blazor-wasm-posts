using ClientBlazorWASM.Helpers;
using ClientBlazorWASM.Models;
using ClientBlazorWASM.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace ClientBlazorWASM.Services
{
    public class PostsService : IPostsService
    {
        private readonly HttpClient _client;
        public PostsService(HttpClient client)
        {
            _client = client;
        }
        public async Task<Post> AtualizarPost(int id, Post post)
        {
            var content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync($"{Constants.UrlBaseApi}api/post/{id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<Post> CriarPost(Post post)
        {
            var content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{Constants.UrlBaseApi}api/post", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> DeletarPost(int id)
        {
            var response = await _client.DeleteAsync($"{Constants.UrlBaseApi}api/post/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<Post> GetPost(int id)
        {
            var response = await _client.GetAsync($"{Constants.UrlBaseApi}api/post/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var post = JsonConvert.DeserializeObject<Post>(content);
                return post;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var response = await _client.GetAsync($"{Constants.UrlBaseApi}api/post");
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<IEnumerable<Post>>(content);
            return posts;
        }

        public async Task<string> UploadImages(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync($"{Constants.UrlBaseApi}api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imagePost = Path.Combine($"{Constants.UrlBaseApi}", postContent);
                return imagePost;
            }
        }
    }
}
