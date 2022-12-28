using ControleContatos.Data;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace ControleUsuarios.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;
        public UsuarioRepositorio(BancoContext context)
        {
            _context = context;
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _context.Usuarios
                .Where(x => x.Excluido == false)
                .Include(x => x.Contatos)
                .ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;

        }

        public UsuarioModel BuscarPorId(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(b => b.Id == id);
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioModel = BuscarPorId(usuario.Id);
            if (usuarioModel == null) throw new Exception("Houve um erro na atualização do Usuario!");

            usuarioModel.Nome = usuario.Nome;
            usuarioModel.Login = usuario.Login;
            usuarioModel.Email = usuario.Email;
            usuarioModel.Perfil = usuario.Perfil;
            usuarioModel.DataAlteracao = DateTime.Now;

            _context.Usuarios.Update(usuarioModel);
            _context.SaveChanges();

            return usuarioModel;
        }

        public bool Excluir(int id)
        {
            UsuarioModel usuarioModel = BuscarPorId(id);

            if (usuarioModel == null) throw new Exception("Houve um erro na Exclusão do Usuario.");

            usuarioModel.Excluido = true;
            _context.Usuarios.Remove(usuarioModel);
            _context.SaveChanges();

            return true;

        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            var usuario = _context.Usuarios.FirstOrDefault(b => b.Login.ToUpper() == login.ToUpper());
            return usuario;
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            var usuario = _context.Usuarios.FirstOrDefault(b => b.Email.ToUpper() == email.ToUpper() && b.Login.ToUpper() == login.ToUpper());
            return usuario;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDb = BuscarPorId(alterarSenhaModel.Id);

            if (usuarioDb == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!usuarioDb.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere");

            if (usuarioDb.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da atual!");

            usuarioDb.setNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDb.DataAlteracao = DateTime.Now;

            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();

            return usuarioDb;
        }
    }
}
