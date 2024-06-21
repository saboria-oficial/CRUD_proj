using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models; // Importa o modelo UsuarioModel
using SistemaDeTarefas.Repositorios; // Importa o namespace Repositorios
using SistemaDeTarefas.Repositorios.Interfaces; // Importa a interface IUsuarioRepositorio

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly IUsuarioRepositorio _restauranteRepositorio;

        // Construtor que injeta IUsuarioRepositorio para acesso ao repositório de usuários
        public RestauranteController(IUsuarioRepositorio restauranteRepositorio)
        {
            _restauranteRepositorio = restauranteRepositorio;
        }

        // Endpoint para buscar todos os usuários
        [HttpGet]
        public async Task<ActionResult<List<RestauranteModel>>> BuscarTodosRestaurantes()
        {
            List<RestauranteModel> usuarios = await _restauranteRepositorio.BuscarTodosRestaurantes();
            return Ok(usuarios); // Retorna uma resposta HTTP 200 OK com a lista de usuários
        }

        // Endpoint para buscar um usuário por email
        [HttpGet("{CNPJ}")]
        public async Task<ActionResult<RestauranteModel>> BuscarPorCNPJ(string CNPJ)
        {
            RestauranteModel restaurante = await _restauranteRepositorio.BuscarPorId(CNPJ);
            if (restaurante == null)
            {
                return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado
            }
            return Ok(restaurante); // Retorna uma resposta HTTP 200 OK com o usuário encontrado
        }

        // Endpoint para cadastrar um novo usuário
        [HttpPost]
        public async Task<ActionResult<RestauranteModel>> Cadastrar([FromBody] RestauranteModel RestauranteModel)
        {
            RestauranteModel restaurante = await _restauranteRepositorio.Adicionar(RestauranteModel);
            return Ok(restaurante); // Retorna uma resposta HTTP 200 OK com o usuário cadastrado
        }

        // Endpoint para atualizar um usuário por email
        [HttpPut("{CNPJ}")]
        public async Task<ActionResult<RestauranteModel>> Atualizar([FromBody] RestauranteModel RestauranteModel, string CNPJ)
        {
            RestauranteModel.CNPJ = CNPJ; // Define o email do usuário no modelo recebido
            RestauranteModel restaurante = await _restauranteRepositorio.Atualizar(RestauranteModel, CNPJ);
            if (restaurante == null)
            {
                return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado para atualização
            }
            return Ok(restaurante); // Retorna uma resposta HTTP 200 OK com o usuário atualizado
        }

        // Endpoint para apagar um usuário por email
        [HttpDelete("{CNPJ}")]
        public async Task<ActionResult<bool>> Apagar(string CNPJ)
        {
            bool apagado = await _restauranteRepositorio.Apagar(CNPJ);
            if (!apagado)
            {
                return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado para exclusão
            }
            return Ok(apagado); // Retorna uma resposta HTTP 200 OK indicando se o usuário foi excluído com sucesso
        }
    }
}

