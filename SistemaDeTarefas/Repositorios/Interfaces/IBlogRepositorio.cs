using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface IBlogRepositorio
    {
        Task<List<BlogModel>> BuscarTodosBlogs();
        Task<BlogModel> BuscarPorId(int id);
        Task<BlogModel> Adicionar(BlogModel blog);
        Task<BlogModel> Atualizar(BlogModel blog, int id);
        Task<bool> Apagar(int id);
    }
}
