using ApiBlog.Models;
using ApiBlog.Models.Dtos;
using ApiBlog.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBlog.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repo;
        protected RespostasAPI _respostaAPI;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _respostaAPI = new();
        }

        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            bool validarNomeUnico = _repo.UnicoUsuario(usuarioRegistroDto.NomeUsuario);

            if (!validarNomeUnico)
            {
                _respostaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respostaAPI.IsSuccess = false;
                _respostaAPI.ErrorMessages.Add("O nome de usuário já existe!");
                return BadRequest(_respostaAPI);
            }

            var usuario = await _repo.Registrar(usuarioRegistroDto);
            if(usuario == null)
            {
                _respostaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respostaAPI.IsSuccess = false;
                _respostaAPI.ErrorMessages.Add("Erro no registro!");
                return BadRequest(_respostaAPI);
            }

            _respostaAPI.StatusCode = HttpStatusCode.OK;
            _respostaAPI.IsSuccess = true;
            return Ok(_respostaAPI);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var respostaLogin = await _repo.Login(usuarioLoginDto);
            if (respostaLogin.Usuario == null || string.IsNullOrEmpty(respostaLogin.Token))
            {
                _respostaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respostaAPI.IsSuccess = false;
                _respostaAPI.ErrorMessages.Add("Nome ou senha incorretos!");
                return BadRequest(_respostaAPI);
            }

            _respostaAPI.StatusCode = HttpStatusCode.OK;
            _respostaAPI.IsSuccess = true;
            _respostaAPI.Result = respostaLogin;
            return Ok(_respostaAPI);
        }

        [Authorize] 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _repo.GetUsuarios();

            var listaUsuariosDto = new List<UsuarioDto>();

            foreach(var lista in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
            }
            return Ok(listaUsuariosDto);
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuario(int id)
        {
            var itemUsuario = _repo.GetUsuario(id);

            if(itemUsuario == null)
            {
                return NotFound();
            }

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }

    }
}
