namespace SistemaDeTarefas.Models
{
    // Define a classe que representa um usuário no sistema de tarefas
    public class UsuarioModel
    {
        // Propriedade que representa o ID do cliente
        public int ID_Cliente { get; set; }

        // Propriedade que representa o nome do usuário
        public string? Nome { get; set; }

        // Propriedade que representa o telefone do usuário
        public string? Telefone { get; set; }

        // Propriedade que representa o email do usuário
        public string? Email { get; set; }

        // Propriedade que representa possíveis restrições do usuário
        public string? Restricao { get; set; }

        // Propriedade que representa o CEP do usuário
        public string? CEP { get; set; }

        // Propriedade que representa a senha do usuário
        public string? SENHA { get; set; }

        // Propriedade que representa o tipo do usuário
        public string? Tipo { get; set; }

        public bool? GoogleLogin { get; set; }
    }
}
