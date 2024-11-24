using Loja_Projeto.Model;

namespace Loja_Projeto.Services
{
    public interface ITrocaService
    {
        public Task<Guid> Trocar(TrocaModel trocaModel);

    }
}
