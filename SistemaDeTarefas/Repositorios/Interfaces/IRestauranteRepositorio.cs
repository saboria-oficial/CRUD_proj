// Importa o namespace necessário para os modelos de dados
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    // Define a interface para o repositório de usuários
    public interface IRestauranteRepositorio
    {
        // Método para buscar todos os usuários
        Task<List<RestauranteModel>> BuscarTodosRestaurante();

        // Método para buscar um usuário pelo email
        Task<RestauranteModel> BuscarPorCNPJ(string email);

        // Método para adicionar um novo usuário
        Task<RestauranteModel> Adicionar(RestauranteModel rest);

        // Método para atualizar um usuário existente pelo email
        Task<RestauranteModel> Atualizar(RestauranteModel rest, string CNPJ);

        // Método para apagar um usuário pelo email
        Task<bool> Apagar(string CNPJ);
    }
}

