using Microsoft.EntityFrameworkCore; // Namespace para trabalhar com Entity Framework Core
using SistemaDeTarefas.Data; // Namespace onde está definido o contexto do banco de dados
using SistemaDeTarefas.Models; // Namespace onde está definido o modelo UsuarioModel
using SistemaDeTarefas.Repositorios.Interfaces; // Namespace onde está definida a interface IUsuarioRepositorio
using System; // Namespace para a classe Exception
using System.Collections.Generic; // Namespace para List<T>
using System.Threading.Tasks; // Namespace para trabalhar com tarefas assíncronas

namespace SistemaDeTarefas.Repositorios
{
    // Implementação do repositório de usuários que utiliza Entity Framework Core
    public class RestauranteRepositorio : IRestauranteRepositorio
    {
        private readonly SistemaTarefasDBContex _dbcontext;

        // Construtor que recebe o contexto do banco de dados por injeção de dependência
        public RestauranteRepositorio(SistemaTarefasDBContex sistemaTarefasDBContex)
        {
            _dbcontext = sistemaTarefasDBContex;
        }

        // Método para buscar um usuário por email
        public async Task<UsuarioModel> BuscarPorCNPJ(string email)
        {
            return await _dbcontext.Usuarios.FirstOrDefaultAsync(x => x.CNPJ == CNPJ);
        }

        // Método para buscar todos os usuários
        public async Task<List<UsuarioModel>> BuscarTodosRestaurantes()
        {
            return await _dbcontext.Usuarios.ToListAsync();
        }

        // Método para adicionar um novo usuário
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbcontext.Usuarios.AddAsync(usuario);
            await _dbcontext.SaveChangesAsync();

            return usuario;
        }

        // Método para atualizar um usuário
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, string CNPJ)
        {
            // Busca o usuário pelo CNPJ fornecido
            UsuarioModel usuarioPorCNPJ = await BuscarPorId(CNPJ);
            if (usuarioPorCNPJ == null)
            {
                throw new Exception($"Usuario para o CNPJ: {CNPJ} não foi encontrado.");
            }

            // Atualiza os dados do usuário com base no objeto recebido
            usuarioPorCNPJ.Nome = usuario.Nome;
            usuarioPorCNPJ.CNPJ = usuario.CNPJ;
            usuarioPorCNPJ.Intoleracia = usuario.Intoleracia;

            // Atualiza o usuário no contexto do banco de dados e salva as alterações
            _dbcontext.Usuarios.Update(usuarioPorCNPJ);
            await _dbcontext.SaveChangesAsync();

            return usuarioPorCNPJ;
        }

        // Método para apagar um usuário
        public async Task<bool> Apagar(string email)
        {
            // Busca o usuário pelo email fornecido
            UsuarioModel usuarioPorId = await BuscarPorCNPJ(email);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o Email: {email} não foi encontrado.");
            }

            // Remove o usuário do contexto do banco de dados e salva as alterações
            _dbcontext.Usuarios.Remove(usuarioPorId);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}

