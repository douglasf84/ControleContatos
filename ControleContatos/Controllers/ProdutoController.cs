using ControleContatos.Filters;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IFotosProdutoRepositorio _fotosProdutoRepositorio;
        private readonly ISessao _sessao;

        public ProdutoController(IProdutoRepositorio produtoRepositorio, ISessao sessao, IFotosProdutoRepositorio fotosProdutoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _sessao = sessao;
            _fotosProdutoRepositorio = fotosProdutoRepositorio;
        }

        public IActionResult Index()
        {
            List<ProdutoModel> produtos = _produtoRepositorio.BuscarTodos();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ProdutoModel produto)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                produto.UsuarioId = usuarioLogado.Id;

                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Adicionar(produto);

                    TempData["MensagemSucesso"] = "Produto Cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos Cadastrar seu produto, tente novamente! Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult ListarFotosProdutosPorId(int id)
        {
            List<FotosProdutoModel> fotosProdutos = _fotosProdutoRepositorio.BuscarFotosPorIdProduto(id);
            return PartialView("_FotosProduto", fotosProdutos);
        }
    }   
}
