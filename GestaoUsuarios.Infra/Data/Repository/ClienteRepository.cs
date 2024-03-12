using GestaoUsuarios.Domain.Core.Interfaces;
using GestaoUsuarios.Domain.Entities;
using GestaoUsuarios.Domain.Interfaces;
using GestaoUsuarios.Infra.Context;

namespace GestaoUsuarios.Infra.Data.Repository
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(GestaoUsuariosContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {
        }
    }
}
