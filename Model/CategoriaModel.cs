using System.ComponentModel.DataAnnotations;

namespace Loja_Projeto.Model
{
    public class CategoriaModel
    {
        public int CategoriaId { get; set; }
        [Required]
        public string NomeCategoria { get; set; }

    }
}
