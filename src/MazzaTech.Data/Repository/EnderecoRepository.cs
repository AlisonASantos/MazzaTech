using MazzaTech.Business.Intefaces.repository;
using MazzaTech.Business.Models;
using MazzaTech.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MazzaTech.Data.Repository
{
    public class EnderecoRepository : Repository<EnderecoEntity>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context) { }

        public async Task<EnderecoEntity> ObterEnderecoPorCliente(Guid ClienteId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.ClienteId == ClienteId);
        }
    }
}