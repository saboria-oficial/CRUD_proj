using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class BlogRepositorio : IBlogRepositorio
    {
        public Task<BlogModel> Adicionar(BlogModel blog)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Apagar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogModel> Atualizar(BlogModel blog, int id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogModel> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogModel>> BuscarTodosBlogs()
        {
            throw new NotImplementedException();
        }
    }
}
