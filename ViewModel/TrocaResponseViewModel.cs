using Loja_Projeto.Model;

namespace Loja_Projeto.ViewModel
{
    public class TrocaResponseViewModel
    {
        public Guid TrocaId { get; set; }

        public TrocaStatus TrocaStatus { get; set; }

        public DateTime DataCriacao { get; set; }

        public int ProdutoIdMeu { get; set; }

        public string NomeProdutoMeu { get; set; } // Nome do Produto do usuário

        public int ProdutoIdEscolhido { get; set; }

        public string NomeProdutoEscolhido { get; set; } // Nome do Produto escolhido
    }
}
