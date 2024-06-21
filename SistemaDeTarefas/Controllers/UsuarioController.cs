using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models; // Importa o modelo UsuarioModel
using SistemaDeTarefas.Repositorios; // Importa o namespace Repositorios
using SistemaDeTarefas.Repositorios.Interfaces; // Importa a interface IUsuarioRepositorio

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
            return Ok(usuarios); // Retorna uma resposta HTTP 200 OK com a lista de usuários
        }

        // Endpoint para buscar um usuário por email
        [HttpGet("{email}")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(string email)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(email);
            if (usuario == null)
            {
                return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado
            }
            return Ok(usuario); // Retorna uma resposta HTTP 200 OK com o usuário encontrado
        }

        // Endpoint para login
        [HttpGet("{email}/{senha}")]
        public async Task<ActionResult<UsuarioModel>> Login(string email, string senha)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(email);
            if (usuario == null)
            {
                return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado
            }
            else
            {
                string token = TokenGenerator.GenerateToken(email, senha);
                if (token != null)
                {
                    Response[] pessoas = new ResponseLoginModel[]
                {
                new ResponseLoginModel { Response = "true", Key = token } }
                }
                
            }
            return Ok(Response); // Retorna uma resposta HTTP 200 OK com o usuário encontrado
        }




        // Endpoint para cadastrar um novo usuário
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);
            return Ok(usuario); // Retorna uma resposta HTTP 200 OK com o usuário cadastrado
        }

        // Endpoint para atualizar um usuário por email
        [HttpPut("{email}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, string email)
        {
            usuarioModel.Email = email; // Define o email do usuário no modelo recebido
            UsuarioModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, email);
            if (usuario == null)
            {
                return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado para atualização
            }
            return Ok(usuario); // Retorna uma resposta HTTP 200 OK com o usuário atualizado
        }

        // Endpoint para apagar um usuário por email
        [HttpDelete("{email}")]
        public async Task<ActionResult<bool>> Apagar(string email)
        {
            bool apagado = await _usuarioRepositorio.Apagar(email);
            if (!apagado)
            {
                return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado para exclusão
            }
            return Ok(apagado); // Retorna uma resposta HTTP 200 OK indicando se o usuário foi excluído com sucesso
        }
    }
}
