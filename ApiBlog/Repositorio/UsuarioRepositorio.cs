using ApiBlog.Data;
using ApiBlog.Models;
using ApiBlog.Models.Dtos;
using ApiBlog.Repositorio.IRepositorio;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace ApiBlog.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;
        private string chaveSecreta;
        public UsuarioRepositorio(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            chaveSecreta = config.GetValue<string>("ApiSettings:Secret");
        }

        public Usuario GetUsuario(int id)
        {
            return _context.Usuario.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _context.Usuario.OrderBy(u => u.Id).ToList();
        }

        public async Task<UsuarioLoginRespostaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptada = obtermd5(usuarioLoginDto.Password);
            var usuario = _context.Usuario.FirstOrDefault(
                u => u.NomeUsuario.ToLower() == usuarioLoginDto.NomeUsuario.ToLower()
                && u.Password == passwordEncriptada
                );

            if (usuario == null)
            {
                return new UsuarioLoginRespostaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }

            var configToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(chaveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NomeUsuario.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = configToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespostaDto usuarioLoginRepostaDto = new UsuarioLoginRespostaDto()
            {
                Token = configToken.WriteToken(token),
                Usuario = usuario
            };

            return usuarioLoginRepostaDto;
        }

        public async Task<Usuario> Registrar(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passwordEncriptada = obtermd5(usuarioRegistroDto.Password);

            Usuario usuario = new Usuario()
            {
                NomeUsuario = usuarioRegistroDto.NomeUsuario,
                Nome = usuarioRegistroDto.Nome,
                Email = usuarioRegistroDto.Email,
                Password = usuarioRegistroDto.Password,
            };

            _context.Usuario.Add(usuario);
            usuario.Password = passwordEncriptada;
            await _context.SaveChangesAsync();
            return usuario;
        }

        public bool UnicoUsuario(string nome)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.NomeUsuario == nome);
            if(usuario == null)
            {
                return true;
            }

            return false;
        }

        // método para encriptar a senha com MD5, se usa tanto no acesso quanto no registro
        private static string obtermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();

            return resp;
        }
    }
}
