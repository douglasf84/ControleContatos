using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace ControleContatos.Models
{
    public class ContatoModel : DefaultModel
    {
        [Required(ErrorMessage = "Digite o nome do contato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o email do contato")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage = "O celular informado não é válido!")]
        public string Celular { get; set; }

        public int? UsuarioId { get; set; }

        public UsuarioModel? Usuario { get; set; }
    }
}
