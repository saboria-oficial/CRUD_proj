using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface IRestauranteRepositorio
    {
        Task<List<RestauranteModel>> BuscarTodosRestaurantes();
        Task<RestauranteModel> BuscarPorCNPJ(string cnpj);
        Task<RestauranteModel> Adicionar(RestauranteModel rest);
        Task<RestauranteModel> Atualizar(RestauranteModel rest, string cnpj);
        Task<bool> Apagar(string cnpj);
    }
}
