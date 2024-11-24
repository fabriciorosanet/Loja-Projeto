using Loja_Projeto.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Loja_Projeto.Repository.Interface
{
    public interface ITrocaRepository
    {
        public Task<Guid> Insert(Model.TrocaModel trocaModel);
        public Task<TrocaModel> FindById(Guid id);
    }
}
