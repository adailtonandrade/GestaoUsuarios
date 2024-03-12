namespace GestaoUsuarios.Application.Clientes.Response
{
    public record ClienteModelResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int? PorteEmpresa { get; set; }
    }
}
