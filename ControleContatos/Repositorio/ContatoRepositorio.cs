using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;

        }

        public ContatoModel BuscarPorId(int id)
        {
            var contato = _bancoContext.Contatos.FirstOrDefault(b => b.Id == id);
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoModel = BuscarPorId(contato.Id);
            if (contatoModel == null) throw new Exception("Houve um erro na atualização do Contato!");

            contatoModel.Nome = contato.Nome;
            contatoModel.Email = contato.Email;
            contatoModel.Celular = contato.Celular;

            _bancoContext.Contatos.Update(contatoModel);
            _bancoContext.SaveChanges();

            return contatoModel;
        }

        public bool Excluir(int id)
        {
            ContatoModel contatoModel = BuscarPorId(id);

            if (contatoModel == null) throw new Exception("Houve um erro na Exclusão do contato.");

            _bancoContext.Contatos.Remove(contatoModel);
            _bancoContext.SaveChanges();

            return true;
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }

        public List<ContatoModel> BuscarTodosPorUsuarioLogado(int usuarioId)
        {
            return _bancoContext.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }
    }
}
