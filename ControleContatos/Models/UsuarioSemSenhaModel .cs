using ControleContatos.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ControleContatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digite o Nome do usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Login do usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o email do Usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Selecione o perfil do Usuário")]
        public PerfilEnum? Perfil { get; set; }

 
    }
}
