using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IFotosProdutoRepositorio
    {
        List<FotosProdutoModel> BuscarFotosPorIdProduto(int id);

        FotosProdutoModel Adicionar(FotosProdutoModel fotosProduto);
      
        bool Excluir(int id);


    }
}
