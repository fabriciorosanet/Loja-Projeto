using Loja_Projeto.Data;
using Loja_Projeto.Model;
using Loja_Projeto.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loja_Projeto.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DataContext _dataContext;

        public CategoriaRepository(DataContext ctx)
        {
            _dataContext = ctx;
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = new CategoriaModel() { CategoriaId = id };

            _dataContext.Categorias.Remove(categoria);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IList<CategoriaModel>> FindAllAsync()
        {
            return await _dataContext.Categorias
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CategoriaModel> FindByIdAsync(int id)
        {
            return await _dataContext.Categorias
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
        }

        public async Task<int> InsertAsync(CategoriaModel categoriaModel)
        {
            await _dataContext.Categorias.AddAsync(categoriaModel);
            await _dataContext.SaveChangesAsync();

            return categoriaModel.CategoriaId;
        }

        public async Task UpdateAsync(CategoriaModel categoriaModel)
        {
            _dataContext.Categorias.Update(categoriaModel);
            await _dataContext.SaveChangesAsync();
        }
    }
}

