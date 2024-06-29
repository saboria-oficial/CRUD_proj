using Azure;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models; 
using SistemaDeTarefas.Repositorios; 
using SistemaDeTarefas.Repositorios.Interfaces; 

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        // Construtor que injeta IUsuarioRepositorio para acesso ao repositório de usuários
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // Endpoint para buscar todos os usuários
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios); 
        }

        // Endpoint para buscar um usuário por email
        [HttpGet("{email}")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(string email)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(email);
            if (usuario == null)
            {
                return NotFound(); 
            }
            return Ok(usuario); 
        }

        // Endpoint para login
        [HttpGet("{email}/{key}")]
        public async Task<ActionResult<UsuarioModel>> Login(string email, string key)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(email);
            if (usuario == null)
            {
                return NotFound(); 
            }
            else
            {
                Console.WriteLine($"Senha do usuário: {usuario.SENHA}");
                if (key == usuario.SENHA)
                {
                    string token = TokenGenerator.GenerateToken(email, key);
                    ResponseLoginModel responseLogin = new ResponseLoginModel();

                    Console.WriteLine("Token" + token);

                    if (token != null)
                    {
                        responseLogin.Response = "succes";
                        responseLogin.Key = token;
                    }
                    else
                    {
                        responseLogin.Response = "error";
                        responseLogin.Key = null;
                    }
                    return Ok(responseLogin); 

                }
                else
                {
                    return NotFound(); 
                }
                

               
            }
        }

        // Endpoint para cadastrar um novo usuário
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);
            return Ok(usuario); 
        }

        // Endpoint para atualizar um usuário por email
        [HttpPut("{email}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, string email)
        {
            usuarioModel.Email = email; 
            UsuarioModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, email);
            if (usuario == null)
            {
                return NotFound(); 
            }
            return Ok(usuario); 
        }

        // Endpoint para apagar um usuário por email
        [HttpDelete("{email}")]
        public async Task<ActionResult<bool>> Apagar(string email)
        {
            bool apagado = await _usuarioRepositorio.Apagar(email);
            if (!apagado)
            {
                return NotFound(); 
            }
            return Ok(apagado); 
        }
    }
}
