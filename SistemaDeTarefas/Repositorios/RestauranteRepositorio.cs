using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class RestauranteRepositorio : IRestauranteRepositorio
    {
        private readonly SistemaTarefasDBContex _dbContext;
        public RestauranteRepositorio(SistemaTarefasDBContex sistemaTarefasDBContex)
        {
            _dbContext = sistemaTarefasDBContex;
        }
        public async Task<RestauranteModel> BuscarPorCNPJ(string cnpj)
        {
            return await _dbContext.Restaurante.FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }

        public async Task<List<RestauranteModel>> BuscarTodosRestaurantes()
        {
            return await _dbContext.Restaurante.ToListAsync();
        }

        public async Task<RestauranteModel> Adicionar(RestauranteModel rest)
        {
            await _dbContext.Restaurante.AddAsync(rest);
            await _dbContext.SaveChangesAsync();

            return rest;
        }

        public async Task<RestauranteModel> Atualizar(RestauranteModel rest, string cnpj)
        {
            RestauranteModel restaurantePorCNPJ = await BuscarPorCNPJ(cnpj);
            if (restaurantePorCNPJ == null)
            {
                throw new Exception($"CNPJ do restaurante: {cnpj} não foi encontrado.");
            }

            restaurantePorCNPJ.Nome = rest.Nome;
            restaurantePorCNPJ.Cnpj = rest.Cnpj;
            restaurantePorCNPJ.Intolerancia = rest.Intolerancia;
            restaurantePorCNPJ.Senha = rest.Senha;
            restaurantePorCNPJ.Telefone = rest.Telefone;
            restaurantePorCNPJ.Cep = rest.Cep;
            restaurantePorCNPJ.Email = rest.Email;
            restaurantePorCNPJ.Culinaria = rest.Culinaria;



        _dbContext.Restaurante.Update(restaurantePorCNPJ);
            await _dbContext.SaveChangesAsync();
            return restaurantePorCNPJ;
        }

        public async Task<bool> Apagar(string cnpj)
        {
            RestauranteModel restaurantePorCNPJ = await BuscarPorCNPJ(cnpj);
            if (restaurantePorCNPJ == null)
            {
                throw new Exception($"CNPJ do restaurante: {cnpj} não foi encontrado.");
            }
            _dbContext.Restaurante.Remove(restaurantePorCNPJ);
            await _dbContext.SaveChangesAsync();   
            return true;
        }




    }
}
