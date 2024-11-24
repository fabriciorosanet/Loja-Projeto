using Loja_Projeto.Data;
using Loja_Projeto.Model;
using Loja_Projeto.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loja_Projeto.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _dataContext;

        public ProdutoRepository(DataContext ctx)
        {
            _dataContext = ctx;
        }

        public async Task<IList<ProdutoModel>> FindAllAsync()
        {
            var produtos = await _dataContext.Produtos.AsNoTracking().ToListAsync();
            return produtos ?? new List<ProdutoModel>();
        }

        public async Task<IList<ProdutoModel>> FindAllAsync(int pagina = 0, int tamanho = 5)
        {
            var produtos = await _dataContext.Produtos
                                .Include(c => c.Categoria)
                                .Include(u => u.Usuario)
                                    .AsNoTracking()
                                    .OrderBy(p => p.Nome)
                                    .Skip(tamanho * pagina)
                                    .Take(tamanho)
                                    .ToListAsync();

            return produtos ?? new List<ProdutoModel>();
        }

        public async Task<IList<ProdutoModel>> FindAllAsync(DateTime? dataRef, int tamanho)
        {
            var produtos = await _dataContext.Produtos
                                .Include(c => c.Categoria)
                                .Include(u => u.Usuario)
                                    .AsNoTracking()
                                    .Where(p => p.DataCadastro > dataRef)
                                    .OrderBy(p => p.DataCadastro)
                                    .Take(tamanho)
                                    .ToListAsync();

            return produtos ?? new List<ProdutoModel>();
        }

        public async Task<IList<ProdutoModel>> FindAllByIdRefAsync(int produtoIdRef, int tamanho)
        {
            var produtos = await _dataContext.Produtos
                                .Include(c => c.Categoria)
                                .Include(u => u.Usuario)
                                    .AsNoTracking()
                                    .Where(p => p.ProdutoId > produtoIdRef)
                                    .OrderBy(p => p.DataCadastro)
                                    .Take(tamanho)
                                    .ToListAsync();

            return produtos ?? new List<ProdutoModel>();
        }

        public async Task<int> CountAsync()
        {
            return await _dataContext.Produtos.CountAsync();
        }

        public async Task<IList<ProdutoModel>> FindByNomeAsync(string nome)
        {
            var produtos = await _dataContext
                                .Produtos
                                .AsNoTracking()
                                .Include(p => p.Categoria)
                                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                                .ToListAsync();

            return produtos ?? new List<ProdutoModel>();
        }

        public async Task<ProdutoModel> FindByIdAsync(int id)
        {
            return await _dataContext.Produtos
                        .Include(c => c.Categoria)
                        .Include(u => u.Usuario)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(p => p.ProdutoId == id);
        }

        public async Task<int> InsertAsync(ProdutoModel produtoModel)
        {
            await _dataContext.Produtos.AddAsync(produtoModel);
            await _dataContext.SaveChangesAsync();
            return produtoModel.ProdutoId;
        }

        public async Task UpdateAsync(ProdutoModel produtoModel)
        {
            _dataContext.Produtos.Update(produtoModel);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProdutoModel produtoModel)
        {
            _dataContext.Produtos.Remove(produtoModel);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var produtoModel = new ProdutoModel() { ProdutoId = id };
            await DeleteAsync(produtoModel);
        }
    }
}
