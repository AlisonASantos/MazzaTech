using MazzaTech.Business.Models;

namespace MazzaTech.Business.Intefaces.repository
{
    public interface IClienteRepository : IRepository<ClienteEntity>
    {
        Task<List<EnderecoEntity>> ObterListaEndereco(Guid id);
    }
}