using Loja_Projeto.Model;

namespace Loja_Projeto.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<IList<UsuarioModel>> FindAllAsync();
        Task<UsuarioModel> FindByIdAsync(int id);
        Task<int> InsertAsync(UsuarioModel usuarioModel);
        Task UpdateAsync(UsuarioModel usuarioModel);
        Task DeleteAsync(int id);
        Task<UsuarioModel> FindByEmailAndSenhaAsync(string email, string senha);
    }
}
