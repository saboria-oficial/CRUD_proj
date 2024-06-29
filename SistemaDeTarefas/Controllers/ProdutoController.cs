using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        [HttpGet("{idproduto}/imagem")]
        public async Task<IActionResult> ObterImagemRestaurante(string idproduto)
        {
            var produto = await _produtoRepositorio.BuscarPorIdProduto(idproduto);
            if (produto == null || produto.Foto == null)
            {
                return NotFound();
            }

            // Converte a imagem para base64
            string base64String = Convert.ToBase64String(produto.Foto);
            string imageDataUrl = $"data:image/png;base64,{base64String}";

            return Ok(new { ImagemUrl = imageDataUrl });
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarTodosProdutos()
        {
            List<ProdutoModel> produto = await _produtoRepositorio.BuscarTodosProdutos();
            return Ok(produto);
        }

        [HttpGet("{idproduto}")]
        public async Task<ActionResult<ProdutoModel>> BuscarPorIdProduto(string idproduto)
        {
            ProdutoModel produto = await _produtoRepositorio.BuscarPorIdProduto(idproduto);
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> Cadastrar([FromForm] ProdutoModel produtoModel, [FromForm] IFormFile? foto)
        {
            if (foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await foto.CopyToAsync(memoryStream);
                    produtoModel.Foto = memoryStream.ToArray();
                }
            }
            ProdutoModel produto = await _produtoRepositorio.Adicionar(produtoModel);
            return Ok(produto);
        }

        [HttpPut("{idproduto}")]
        public async Task<ActionResult<ProdutoModel>> Atualizar([FromForm] ProdutoModel produtoModel, [FromForm] IFormFile? imagem, string idproduto)
        {
            if (imagem != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagem.CopyToAsync(memoryStream);
                    produtoModel.Foto = memoryStream.ToArray();
                }
            }
                produtoModel.IdProduto = idproduto;
            ProdutoModel produto = await _produtoRepositorio.Atualizar(produtoModel, idproduto);
            return Ok(produto);
        } 

        [HttpDelete("{idproduto}")]
        public async Task<ActionResult<ProdutoModel>> Apagar(string idproduto)
        {
            bool apagado = await _produtoRepositorio.Apagar(idproduto);
            return Ok(apagado);
        }
    }
}
