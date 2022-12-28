using ControleContatos.Enums;
using ControleContatos.Helper;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ControleContatos.Models
{
    public class UsuarioModel : DefaultModel
    {        

        [Required(ErrorMessage ="Digite o Nome do usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Login do usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o email do contato")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a Senha do usuário")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Selecione o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }     

        public virtual List<ContatoModel> Contatos { get; set; }

        public virtual List<ProdutoModel> Produtos { get; set; }

        //verifica se a senha está correta
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public void setNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8); 

            Senha = novaSenha.GerarHash();

            return novaSenha;
        }
    }
}
