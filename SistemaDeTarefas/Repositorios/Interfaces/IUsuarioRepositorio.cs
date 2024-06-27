// Importa o namespace necessário para os modelos de dados
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    // Define a interface para o repositório de usuários
    public interface IUsuarioRepositorio
    {
        // Método para buscar todos os usuários
        Task<List<UsuarioModel>> BuscarTodosUsuarios();
       // Task<UsuarioModel> EnviarEmail(EmailModel email);

        // Método para buscar um usuário pelo email
        Task<UsuarioModel> BuscarPorId(string email);

        // Método para adicionar um novo usuário
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);

        // Método para atualizar um usuário existente pelo email
        Task<UsuarioModel> Atualizar(UsuarioModel usuario, string email);

        // Método para apagar um usuário pelo email
        Task<bool> Apagar(string email);
    }
}
