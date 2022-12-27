using ControleContatos.Filters;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = new List<ContatoModel>();
                

            if (usuarioLogado.Perfil == Enums.PerfilEnum.Administrador)
            {
               contatos = _contatoRepositorio.BuscarTodos();
            }
            else
            {
               contatos = _contatoRepositorio.BuscarTodosPorUsuarioLogado(usuarioLogado.Id);
            }           

            return View(contatos);


        }

        [HttpGet]
        public IActionResult Criar()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                contato.UsuarioId = usuarioLogado.Id;

                if (ModelState.IsValid)
                {                 
                    _contatoRepositorio.Adicionar(contato);

                    TempData["MensagemSucesso"] = "Contato Cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos Cadastrar seu contato, tente novamente! Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Editar(ContatoModel contato)
        {
            try
            {

                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                contato.UsuarioId = usuarioLogado.Id;

                if (ModelState.IsValid)
                {     
                    _contatoRepositorio.Atualizar(contato);

                    TempData["MensagemSucesso"] = "Contato Editado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, ocorreu um erro durante o processo de Edição, tente novamente ! Detalhe do erro: {erro.Message}";
                return View(contato);
            }

        }
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Excluir(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato Excluído com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, ocorreu um erro durante o processo de Exclusão, tente novamente!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, ocorreu um erro durante o processo de Exclusão, tente novamente ! Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }


        }
    }
}
