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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContex _dbcontext;

        // Construtor que recebe o contexto do banco de dados por injeção de dependência
        public UsuarioRepositorio(SistemaTarefasDBContex sistemaTarefasDBContex)
        {
            _dbcontext = sistemaTarefasDBContex;
        }
        public async Task<UsuarioModel> BuscarPorId(string email)
        {
            return await _dbcontext.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbcontext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbcontext.Usuarios.AddAsync(usuario);
            await _dbcontext.SaveChangesAsync();

            return usuario;
        }

        // Método para atualizar um usuário
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, string email)
        {
            // Busca o usuário pelo email fornecido
            UsuarioModel usuarioPorId = await BuscarPorId(email);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o Email: {email} não foi encontrado.");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Telefone = usuario.Telefone;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.Restricao = usuario.Restricao;
            usuarioPorId.CEP = usuario.CEP;
            usuarioPorId.SENHA = usuario.SENHA;
            usuarioPorId.Tipo = usuario.Tipo;
            usuarioPorId.GoogleLogin = usuario.GoogleLogin;
           
            _dbcontext.Usuarios.Update(usuarioPorId);
            await _dbcontext.SaveChangesAsync();

            return usuarioPorId;
        }

        // Método para apagar um usuário
        public async Task<bool> Apagar(string email)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(email);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o Email: {email} não foi encontrado.");
            }

            _dbcontext.Usuarios.Remove(usuarioPorId);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
