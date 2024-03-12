using GestaoUsuarios.Application.Interfaces;
using GestaoUsuarios.Application.Usuarios.Commands;
using GestaoUsuarios.Application.Usuarios.Response;
using GestaoUsuarios.Domain.Core;
using GestaoUsuarios.Domain.Core.Interfaces;
using GestaoUsuarios.Domain.Core.Response;
using MediatR;

namespace GestaoUsuarios.Application.Usuarios.Handlers
{
    public class LoginCommandHandler : BaseCommandHandler, IRequestHandler<LoginCommand, Response<UsuarioLoginResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<Response<UsuarioLoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var usuario = await _identityService.GetUserByUserNameAsync(request.NomeUsuario);
            if (usuario == null || !await _identityService.CheckPasswordAsync(usuario, request.Senha))
            {
                AddError("Falha ao realizar o Login");
                return Fail<UsuarioLoginResponse>(ResultadoValidacao);
            }
            var (token, dataExpiracao) = _tokenGenerator.GerarJWTToken((usuario.Id.ToString(), request.NomeUsuario));
            var usuarioLoginResponse = new UsuarioLoginResponse
            {
                Token = token,
                DataExpiracao = dataExpiracao
            };
            return Success(usuarioLoginResponse, ResultadoValidacao);
        }
    }
}
