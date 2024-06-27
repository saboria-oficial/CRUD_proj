using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
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
        public async Task<ActionResult<ProdutoModel>> Cadastrar([FromBody] ProdutoModel produtoModel)
        {
            ProdutoModel produto = await _produtoRepositorio.Adicionar(produtoModel);
            return Ok(produto);
        }

        [HttpPut("{idproduto}")]
        public async Task<ActionResult<ProdutoModel>> Atualizar([FromBody] ProdutoModel produtoModel, string idproduto)
        {
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
