using ControleContatos.Data;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.Repositorio
{
    public class FotosProdutoRepositorio : IFotosProdutoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public FotosProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ProdutoModel Adicionar(FotosProdutoModel fotosProduto)
        {
            throw new NotImplementedException();
        }

        public List<FotosProdutoModel> BuscarFotosPorIdProduto(int id)
        {
            return _bancoContext.FotosProduto
                .Include(x => x.Produto)
                .Where(x => x.ProdutoId == id)
                .ToList();
        }

        public bool Excluir(int id)
        {
            throw new NotImplementedException();
        }
    }
}
