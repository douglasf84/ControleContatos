using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IContatoRepositorio contatoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _contatoRepositorio = contatoRepositorio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos cadastrar seu usuário, favor tente novamente.";
                    return View(usuario);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuário, favor tente novamente. Detalhe do Erro: {erro.Message}";
                throw;
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;

                // para nao editar todos os registros
                usuario = new UsuarioModel()
                {
                    Id = usuarioSemSenhaModel.Id,
                    Nome = usuarioSemSenhaModel.Nome,
                    Login = usuarioSemSenhaModel.Login,
                    Email = usuarioSemSenhaModel.Email,
                    Perfil = usuarioSemSenhaModel.Perfil
                };

                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário Alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, correu um erro ao Alterar o Usuário, detalhe do erro ! {erro.Message} ";
                return RedirectToAction("Index");
                throw;
            }
        }

        [HttpGet]
        public IActionResult ListarContatosPorUsuarioId(int id)
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodosPorUsuarioLogado(id);
            return PartialView("_ContatosUsuario", contatos);
        }

        [HttpGet]
        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Excluir(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário Excluído com sucesso !";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos excluir o cadastro, tentenovamente";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos excluir o cadastro, tentenovamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
