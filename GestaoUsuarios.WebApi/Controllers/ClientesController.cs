using GestaoUsuarios.Application.Clientes.Commands;
using GestaoUsuarios.Application.Clientes.Queries;
using GestaoUsuarios.Application.Clientes.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoUsuarios.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ISender _mediator;
        public ClientesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        public async Task<IActionResult> Cliente([FromBody] CriarClienteCommand criarClienteCommand)
        {
            try
            {
                var resultado = await _mediator.Send(criarClienteCommand);
                if (resultado.IsValid)
                    return Created(uri: string.Empty, "Cliente cadastrado com sucesso!");
                return BadRequest(string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteModelResponse))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cliente = await _mediator.Send(new ObterClientePorIdQuery(id));
                if (cliente != null)
                    return Ok(cliente);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClienteResponse>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clientes = await _mediator.Send(new ObterTodosOsClientesQuery());
                if (clientes != null)
                    return Ok(clientes);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> Put([FromBody] EditarClienteCommand editarClienteCommand)
        {
            try
            {
                var resultado = await _mediator.Send(editarClienteCommand);
                if (resultado.IsValid)
                    return Ok("Cliente alterado com sucesso!");
                return BadRequest(string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _mediator.Send(new DesativarClienteCommand(id));
                if (resultado.IsValid)
                    return Ok("Cliente desativado com sucesso!");
                return BadRequest(string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}