using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    // Define a interface para o repositório de usuários
    public interface IUsuarioRepositorio
    {

        Task<List<UsuarioModel>> BuscarTodosUsuarios();

        Task<UsuarioModel> BuscarPorId(string email);

        Task<UsuarioModel> Adicionar(UsuarioModel usuario);

        Task<UsuarioModel> Atualizar(UsuarioModel usuario, string email);

        Task<bool> Apagar(string email);
    }
}
