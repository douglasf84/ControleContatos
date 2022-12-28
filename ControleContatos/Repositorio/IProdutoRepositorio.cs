using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IProdutoRepositorio
    {
        ProdutoModel Adicionar(ProdutoModel produto);
        List<ProdutoModel> BuscarTodos();
        List<ProdutoModel> BuscarTodosPorIdProduto(int id);
        List<ProdutoModel> BuscarTodosPorUsuarioLogado(int id);
        ProdutoModel BuscarPorId(int id);
        ProdutoModel Atualizar(ProdutoModel produto);
        bool Excluir(int id);
    }
}
