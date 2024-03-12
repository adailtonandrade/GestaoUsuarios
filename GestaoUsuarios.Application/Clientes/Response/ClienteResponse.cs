namespace GestaoUsuarios.Application.Clientes.Response
{
    public record ClienteResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string PorteEmpresa { get; set; }
    }
}
