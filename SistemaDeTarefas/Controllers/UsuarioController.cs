using Azure;
using Microsoft.AspNetCore.Http;
using SendGrid;
using SendGrid.Helpers.Mail;
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
        [HttpGet("{email}/{key}")]
        public async Task<ActionResult<UsuarioModel>> Login(string email, string key)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(email);
            if (usuario == null)
            {
                return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado
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
                    return Ok(responseLogin); // Retorna uma resposta HTTP 200 OK com o objeto responseLogin

                }
                else
                {
                    return NotFound(); // Retorna uma resposta HTTP 404 Not Found se o usuário não for encontrado
                }
                

               
            }
        }

        // Enpoint de disparo de emil
        //[HttpPost("api/email/enviar")]
        //public async Task<ActionResult<string>> EnviarEmail([FromBody] EmailModel emailModel)
        //{
        //    try
        //    {
        //        var client = new SendGridClient("EM2YJC44WM75ZXE2C5M3SJQF");

        //        var from = new EmailAddress("saboriaoficial@gmail.com", "Seu Nome");
        //        var to = new EmailAddress(emailModel.Destinatario, "Destinatário");
        //        var subject = emailModel.Subject;
        //        var htmlContent = "";

        //        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);

        //        var response = await client.SendEmailAsync(msg);

        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            return Ok("E-mail enviado com sucesso!");
        //        }
        //        else
        //        {
        //            return StatusCode(500, $"Erro ao enviar o e-mail: {response}");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Erro ao enviar o e-mail: {ex.Message}");
        //    }

        //}


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
