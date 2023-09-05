using MazzaTech.Business.Intefaces.repository;
using MazzaTech.Business.Models;
using MazzaTech.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MazzaTech.Data.Repository
{
    public class ClienteRepository : Repository<ClienteEntity>, IClienteRepository
    {
        public ClienteRepository(MeuDbContext context) : base(context)
        {
        }

        public async Task<List<EnderecoEntity>> ObterListaEndereco(Guid id)
        {
            return await Db.Enderecos.AsNoTracking()
                .Include(c => c.ClienteId == id).ToListAsync();
        }
    }
}