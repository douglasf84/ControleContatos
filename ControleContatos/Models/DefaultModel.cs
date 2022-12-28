namespace ControleContatos.Models
{
    public class DefaultModel
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool? Excluido { get; set; } = false;
    }
}
