using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly SistemaTarefasDBContex _dBContex;
        public ProdutoRepositorio(SistemaTarefasDBContex sistemaTarefasDBContex)
        {
            _dBContex = sistemaTarefasDBContex;
        }
        public async Task<ProdutoModel> BuscarPorIdProduto(string idproduto)
        {
            return await _dBContex.Produto.FirstOrDefaultAsync(x => x.IdProduto == idproduto);
        }

        public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        {
            return await _dBContex.Produto.ToListAsync();
        }

        public async Task<ProdutoModel> Adicionar(ProdutoModel produto)
        {
            await _dBContex.Produto.AddAsync(produto);
            await _dBContex.SaveChangesAsync();

            return produto;
        }

        public async Task<ProdutoModel> Atualizar(ProdutoModel produto, string idproduto)
        {
            ProdutoModel produtoPorID = await BuscarPorIdProduto(idproduto);
            if (produtoPorID == null)
            {
                throw new Exception($"{idproduto}Produto não encontrado");
            }

            produtoPorID.IdProduto = produto.IdProduto;
            produtoPorID.Nome = produto.Nome;
            produtoPorID.Descricao = produto.Descricao;
           // produtoPorID.Imagem = produto.Imagem;  AJUSTAR
            produtoPorID.Valor = produto.Valor;
            produtoPorID.Restricao = produto.Restricao;

            _dBContex.Produto.Update(produtoPorID);
            await _dBContex.SaveChangesAsync();


            return produtoPorID;
        }

         public async Task<bool> Apagar(string idproduto)
         {
            ProdutoModel produtoPorID = await BuscarPorIdProduto(idproduto);

            if (produtoPorID == null)
            {
                throw new Exception($"{idproduto}Produto não encontrado");
            }

            _dBContex.Produto.Remove(produtoPorID);
            await _dBContex.SaveChangesAsync();
            return true;
        }




    }
}
