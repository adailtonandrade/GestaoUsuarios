using GestaoUsuarios.Application.Interfaces;
using GestaoUsuarios.Application.Usuarios.Commands;
using GestaoUsuarios.Domain.Core;
using GestaoUsuarios.Domain.Core.Interfaces;
using GestaoUsuarios.Domain.Core.Response;
using GestaoUsuarios.Domain.Identity;
using MediatR;

namespace GestaoUsuarios.Application.Usuarios.Handlers
{
    public class CriarUsuarioCommandHandler : BaseCommandHandler, IRequestHandler<CriarUsuarioCommand, Response<string>>
    {
        private readonly IIdentityService _identityService;

        public CriarUsuarioCommandHandler(IIdentityService identityService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _identityService = identityService;
        }

        public async Task<Response<string>> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {

            var nomeDeUsuarioEhUnico = await _identityService.IsUniqueUserName(request.NomeUsuario);
            if (!nomeDeUsuarioEhUnico)
            {
                AddError("Já existe um Usuário cadastrado com esse username.");
                return Fail<string>(ResultadoValidacao);
            }
            var novoUsuario = new Usuario
            {
                UserName = request.NomeUsuario,
                Email = request.Email,
                NomeExibicao = request.NomeExibicao,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var resultado = await _identityService.CreateUserAsync(novoUsuario, request.Senha);
            if (resultado.Succeeded)
                return Success("Usuario Cadastrado com Sucesso");
            else
            {
                AddError("Falha ao Cadastrar novo usuário: " + string.Join(", ",resultado.Errors.Select(e=>e.Description)));
                return Fail<string>(ResultadoValidacao);
            }
        }
    }
}