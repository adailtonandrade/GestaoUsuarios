using FluentValidation.Results;
using GestaoUsuarios.Application.Usuarios.Commands;
using GestaoUsuarios.Application.Usuarios.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoUsuarios.WebApi.Controllers
{
    [ApiController]
    [Route("api/autenticacao")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ISender _mediator;

        public AutenticacaoController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("cadastrar")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationFailure>))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        public async Task<IActionResult> CriarUsuarioCommmand([FromBody] CriarUsuarioCommand criarUsuarioCommand)
        {
            try
            {
                var resultado = await _mediator.Send(criarUsuarioCommand);
                if (resultado.IsValid)
                    return Created(uri: string.Empty, resultado);
                return BadRequest(resultado.ValidationResult.Errors);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioLoginResponse))]
        public async Task<IActionResult> LoginCommand([FromBody] LoginCommand loginCommand)
        {
            try
            {
                var resultado = await _mediator.Send(loginCommand);
                if (resultado.IsValid)
                    return Ok(resultado.Data);
                return BadRequest(resultado.ValidationResult.Errors);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}