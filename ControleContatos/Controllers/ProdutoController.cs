using ControleContatos.Filters;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IFotosProdutoRepositorio _fotosProdutoRepositorio;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ISessao _sessao;

        public ProdutoController(IProdutoRepositorio produtoRepositorio, IFotosProdutoRepositorio fotosProdutoRepositorio, IHttpContextAccessor httpContextAccessor, ISessao sessao)
        {
            _produtoRepositorio = produtoRepositorio;
            _fotosProdutoRepositorio = fotosProdutoRepositorio;
            _httpContext = httpContextAccessor;
            _sessao = sessao;   
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

            ProdutoModel produtoModel = _produtoRepositorio.BuscarPorId(id);

            string valor = JsonConvert.SerializeObject(produtoModel.Id);
            _httpContext.HttpContext.Session.SetString("sessaoProduto", valor);

            return PartialView("_FotosProduto", fotosProdutos);
        }

        [HttpPost]
        public IActionResult AdicionarFoto(FotosProdutoModel fotosProdutoModel)
        {
            try
            {
                string sessaoProduto = _httpContext.HttpContext.Session.GetString("sessaoProduto");                
                fotosProdutoModel.ProdutoId = int.Parse(sessaoProduto);

                if (!ModelState.IsValid)
                {
                    _fotosProdutoRepositorio.Adicionar(fotosProdutoModel);

                    TempData["MensagemSucesso"] = "Foto do Produto Cadastrado com sucesso";

                    return PartialView("_FotosProduto");

                }
                return View(fotosProdutoModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos Adicionar a Foto do seu produto, tente novamente! Detalhe do erro: {erro.Message}";
                return RedirectToAction();
            }
        }
    }
}
