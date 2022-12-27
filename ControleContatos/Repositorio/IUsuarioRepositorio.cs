using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel Adicionar(UsuarioModel usuario);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel BuscarPorId(int id);
        UsuarioModel Atualizar(UsuarioModel usuario);
        bool Excluir(int id);

        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email, string login);

        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
    }
}
