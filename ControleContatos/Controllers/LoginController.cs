using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //se o usuario estiver logado redirecionar para a home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Senha do Usuário é inválida. Por favor tente novamente.";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor tente novamente.";
                    }

                }
                return View("Index");
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, nao conseguimos realizar seu login, tente novamente, detalhe do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();                      

                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha",mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para seu e-mail cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não foi possível enviar o e-mail. Por favor tente novamente.";
                        }                        
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Não foi possível redefinir a sua senha. Por favor verifique seus dados e tente novamente.";
                        
                    }
                }

                return View("RedefinirSenha");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, nao conseguimos realizar a redefinição de senha, tente novamente, detalhe do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
