namespace ControleContatos.Models
{
    public class FotosProdutoModel 
    {
        public int Id { get; set; }

        public string ImagemUrl { get; set; }
        public string? Descricao { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }

    }
}
