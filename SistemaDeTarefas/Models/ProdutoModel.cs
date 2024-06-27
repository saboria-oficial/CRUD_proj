namespace SistemaDeTarefas.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string? IdProduto { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public byte[] Imagem { get; set; }
        public string? Valor { get; set; }
        public string? Restricao { get; set; }

    }
}
