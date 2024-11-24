using Loja_Projeto.Model;

namespace Loja_Projeto.Repository.Interface
{
    public interface IProdutoRepository
    {
        Task<IList<ProdutoModel>> FindAllAsync();
        Task<IList<ProdutoModel>> FindAllAsync(int pagina, int tamanho);
        Task<IList<ProdutoModel>> FindAllAsync(DateTime? dataRef, int tamanho);
        Task<int> CountAsync();
        Task <IList<ProdutoModel>> FindByNomeAsync(string nome);
        Task<ProdutoModel> FindByIdAsync(int id);
        Task<int> InsertAsync(ProdutoModel produtoModel);
        Task UpdateAsync(ProdutoModel produtoModel);
        Task DeleteAsync(int id);
        Task DeleteAsync(ProdutoModel produtoModel);
    }
}
