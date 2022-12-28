using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class ProdutoModel : DefaultModel
    {
        [Required(ErrorMessage = "Digite o Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite a Descrição do Produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Digite o Estoque Atual do Produto")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "Digite o Estoque Minimo do Produto")]
        public int EstoqueMinimo { get; set; }

        [Required(ErrorMessage = "Digite a Marca do Produto")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Digite a Categoria do Produto")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "Digite o Preço do Produto")]
        public double Preco { get; set; }

        public virtual List<FotosProdutoModel> FotosProduto { get; set; }

        public int? UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }

        public double ValorEstoque()
        {
            return Preco * Estoque;
        }

    }
}
