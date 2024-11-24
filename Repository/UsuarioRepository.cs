using Loja_Projeto.Data;
using Loja_Projeto.Model;
using Loja_Projeto.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loja_Projeto.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext _dataContext;

        public UsuarioRepository(DataContext ctx)
        {
            _dataContext = ctx;
        }

        public async Task<IList<UsuarioModel>> FindAllAsync()
        {
            return await _dataContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> FindByIdAsync(int id)
        {
            return await _dataContext.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == id);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = new UsuarioModel { UsuarioId = id };
            _dataContext.Usuarios.Remove(usuario);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(UsuarioModel usuarioModel)
        {
            await _dataContext.Usuarios.AddAsync(usuarioModel);
            await _dataContext.SaveChangesAsync();
            return usuarioModel.UsuarioId;
        }

        public async Task UpdateAsync(UsuarioModel usuarioModel)
        {
            _dataContext.Usuarios.Update(usuarioModel);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<UsuarioModel> FindByEmailAndSenhaAsync(string email, string senha)
        {
            return await _dataContext.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.EmailUsuario == email && u.Senha == senha);
        }
    }
}
