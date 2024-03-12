namespace GestaoUsuarios.Application.Interfaces
{
    public interface ITokenGenerator
    {
        public (string, DateTime) GerarJWTToken((string idUsuario, string nomeUsuario) detalhesDoUsuario);
    }
}
