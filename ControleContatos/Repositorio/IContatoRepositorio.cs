using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel Adicionar(ContatoModel conato);
        List<ContatoModel> BuscarTodos();
        List<ContatoModel> BuscarTodosPorUsuarioLogado(int usuarioId);
        ContatoModel BuscarPorId(int id);
        ContatoModel Atualizar(ContatoModel contato);
        bool Excluir(int id);
    }
}
