using Loja_Projeto.Model;

namespace Loja_Projeto.Repository.Interface
{
    public interface ICategoriaRepository
    {
        Task DeleteAsync(int id);
        Task<IList<CategoriaModel>> FindAllAsync();
        Task<CategoriaModel> FindByIdAsync(int id);
        Task<int> InsertAsync(CategoriaModel categoriaModel);
        Task UpdateAsync(CategoriaModel categoriaModel);
    }
}
