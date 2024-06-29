using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly IRestauranteRepositorio _restauranteRepositorio;
        public RestauranteController(IRestauranteRepositorio restauranteRepositorio)
        {
            _restauranteRepositorio = restauranteRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<RestauranteModel>>> BuscarTodosRestaurantes()
        {
            List<RestauranteModel> restaurante = await _restauranteRepositorio.BuscarTodosRestaurantes();
            return Ok(restaurante);
        }

        [HttpGet("{cnpj}")]
        public async Task<ActionResult<RestauranteModel>> BuscarPorCNPJ(string cnpj)
        {
            RestauranteModel restaurante = await _restauranteRepositorio.BuscarPorCNPJ(cnpj);
            return Ok(restaurante);
        }

        [HttpGet("{cnpj}/imagem")]
        public async Task<IActionResult> ObterImagemRestaurante(string cnpj)
        {
            var restaurante = await _restauranteRepositorio.BuscarPorCNPJ(cnpj);
            if (restaurante == null || restaurante.Imagem == null)
            {
                return NotFound();
            }

            // Converte a imagem para base64
            string base64String = Convert.ToBase64String(restaurante.Imagem);
            string imageDataUrl = $"data:image/png;base64,{base64String}";

            return Ok(new { ImagemUrl = imageDataUrl });
        }

        [HttpGet("{cnpj}/{key}")]
        public async Task<ActionResult<RestauranteModel>> Login(string cnpj, string key)
        {
            RestauranteModel rest = await _restauranteRepositorio.BuscarPorCNPJ(cnpj);
            if (rest == null)
            {
                return NotFound(); 
            }
            else
            {
                Console.WriteLine($"Senha do usuário: {rest.Senha}");
                if (key == rest.Senha)
                {
                    string token = TokenGenerator.GenerateToken(cnpj, key);
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

        [HttpPost]
        public async Task<ActionResult<RestauranteModel>> Cadastrar([FromForm] RestauranteModel restauranteModel, [FromForm] IFormFile? imagem)
        {
            if (imagem != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagem.CopyToAsync(memoryStream);
                    restauranteModel.Imagem = memoryStream.ToArray();
                }
            }

            RestauranteModel restaurante = await _restauranteRepositorio.Adicionar(restauranteModel);
            return Ok(restaurante);
        }

        [HttpPut("{cnpj}")]
        public async Task<ActionResult<RestauranteModel>> Atualizar([FromForm] RestauranteModel restauranteModel, [FromForm] IFormFile? imagem, string cnpj)
        {
            if (imagem != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagem.CopyToAsync(memoryStream);
                    restauranteModel.Imagem = memoryStream.ToArray();
                }
            }

            restauranteModel.Cnpj = cnpj;
            RestauranteModel restaurante = await _restauranteRepositorio.Atualizar(restauranteModel, cnpj);
            return Ok(restaurante);
        }


        [HttpDelete("{cnpj}")]
        public async Task<ActionResult<RestauranteModel>> Apagar([FromBody] RestauranteModel restauranteModel, string cnpj)
        {
            bool apagado = await _restauranteRepositorio.Apagar(cnpj);
            return Ok(apagado);

        }
    }
}

