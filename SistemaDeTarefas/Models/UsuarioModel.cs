﻿namespace SistemaDeTarefas.Models
{
    // Define a classe que representa um usuário no sistema de tarefas
    public class UsuarioModel
    {
        public int ID_Cliente { get; set; }

        public string? Nome { get; set; }

        public string? Telefone { get; set; }

        public string? Email { get; set; }

        public string? Restricao { get; set; }

        public string? CEP { get; set; }

        public string? SENHA { get; set; }

        public string? Tipo { get; set; }

        public bool? GoogleLogin { get; set; }
    }
}
