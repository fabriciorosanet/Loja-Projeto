namespace Loja_Projeto.Model
{
    public class UsuarioModel
    {
        public int UsuarioId { get; set; }
        public string NomeUsuario {  get; set; }
        public string EmailUsuario { get; set; }
        public string Senha {  get; set; }
        public string? Regra {  get; set; }

        public UsuarioModel() 
        {
        }
        public UsuarioModel(int usuarioId, string email, string nomeUsuario, string senha, string regra) 
        {
            UsuarioId = usuarioId;
            NomeUsuario = nomeUsuario;
            EmailUsuario = email;
            Senha = senha;
            Regra = regra;
        }

    }
}
