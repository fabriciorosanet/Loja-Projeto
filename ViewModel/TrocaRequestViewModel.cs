using System.ComponentModel.DataAnnotations;

namespace Loja_Projeto.ViewModel
{
    public class TrocaRequestViewModel
    {
        [Required(ErrorMessage = "O ID do produto do usuário é obrigatório.")]
        public int ProdutoIdMeu { get; set; }

        [Required(ErrorMessage = "O ID do produto escolhido é obrigatório.")]
        public int ProdutoIdEscolhido { get; set; }
    }
}
