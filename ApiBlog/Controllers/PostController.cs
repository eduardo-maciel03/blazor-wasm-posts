using ApiBlog.Models;
using ApiBlog.Models.Dtos;
using ApiBlog.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlog.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepositorio _repo;
        private readonly IMapper _mapper;

        public PostController(IPostRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPosts()
        {
            var listaPosts = _repo.GetPosts();

            var listaPostsDto = new List<PostDto>();

            foreach (var lista in listaPosts)
            {
                listaPostsDto.Add(_mapper.Map<PostDto>(lista));
            }

            return Ok(listaPostsDto);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPost(int id)
        {
            var itemPost = _repo.GetPost(id);

            if(itemPost == null)
            {
                return NotFound();
            }

            var itemPostDto = _mapper.Map<PostDto>(itemPost);
            return Ok(itemPostDto);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PostCriarDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CriarPost([FromBody] PostCriarDto criarPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (criarPost == null)
            {
                return BadRequest(ModelState);
            }

            if (_repo.ExistePost(criarPost.Titulo))
            {
                ModelState.AddModelError("", "O post já existe");
                return StatusCode(404, ModelState);
            }

            var post = _mapper.Map<Post>(criarPost);
            if (!_repo.CriarPost(post))
            {
                ModelState.AddModelError("", $"Algo de errado ao registrar o {post.Titulo}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPost", new { id = post.Id }, post);

        }

        [Authorize]
        [HttpPatch("{id:int}", Name = "AtualizarPatchPost")]
        [ProducesResponseType(201, Type = typeof(PostAtualizarDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AtualizarPatchPost(int id, [FromBody] PostAtualizarDto atualizarPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (atualizarPost == null || id != atualizarPost.Id)
            {
                return BadRequest(ModelState);
            }

            var post = _mapper.Map<Post>(atualizarPost);
            if (!_repo.AtualizarPost(post))
            {
                ModelState.AddModelError("", $"Algo de errado ao atualizar o {post.Titulo}");
                return StatusCode(500, ModelState);
            }

            _repo.AtualizarPost(post);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id:int}", Name = "ApagarPost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ApagarPost(int id)
        {
            if (!_repo.ExistePost(id))
            {
                return NotFound();
            }

            var post = _repo.GetPost(id);

            if (!_repo.DeletarPost(post))
            {
                ModelState.AddModelError("", $"Algo deu errado ao apagar {post.Titulo}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
