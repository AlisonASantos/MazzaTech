using MazzaTech.Business.Models;

namespace MazzaTech.Business.Intefaces.services
{
    public interface IClienteService : IDisposable
    {
        Task<List<ClienteEntity>> ObterTodos();
        Task<ClienteEntity> ObterPorId(Guid id);
        Task Adicionar(ClienteEntity Cliente);
        Task Atualizar(ClienteEntity Cliente);
        Task Remover(Guid id);
    }
}