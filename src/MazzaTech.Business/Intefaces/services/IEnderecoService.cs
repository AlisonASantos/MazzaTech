using MazzaTech.Business.Models;

namespace MazzaTech.Business.Intefaces.services
{
    public interface IEnderecoService : IDisposable
    {
        Task<List<EnderecoEntity>> ObterTodos();
        Task<EnderecoEntity> ObterPorId(Guid id);
        Task Adicionar(EnderecoEntity Cliente);
        Task Atualizar(EnderecoEntity Cliente);
        Task Remover(Guid id);
    }
}