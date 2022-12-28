using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ProdutoModel Adicionar(ProdutoModel produto)
        {
            _bancoContext.Produtos.Add(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public ProdutoModel Atualizar(ProdutoModel produto)
        {
            ProdutoModel objeto = BuscarPorId(produto.Id);
            if (objeto == null) throw new Exception("Houve um erro na atualização do Contato!");

            _bancoContext.Produtos.Update(objeto);
            _bancoContext.SaveChanges();

            return objeto;
        }

        public ProdutoModel BuscarPorId(int id)
        {
            var resultado = _bancoContext.Produtos.FirstOrDefault(b => b.Id == id);
            return resultado;
        }

        public List<ProdutoModel> BuscarTodos()
        {
            return _bancoContext.Produtos
                .Where(x => x.Excluido == false)
                .ToList();
        }

        public List<ProdutoModel> BuscarTodosPorIdProduto(int id)
        {
            return _bancoContext.Produtos
                .Where(x => x.Id == id)
                .ToList();
        }

        public List<ProdutoModel> BuscarTodosPorUsuarioLogado(int id)
        {
            return _bancoContext.Produtos.Where(x => x.UsuarioId == id).ToList();
        }

        public bool Excluir(int id)
        {
            ProdutoModel objeto = BuscarPorId(id);

            if (objeto == null) throw new Exception("Houve um erro na Exclusão do contato.");

            objeto.Excluido = true;
            _bancoContext.Produtos.Update(objeto);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
