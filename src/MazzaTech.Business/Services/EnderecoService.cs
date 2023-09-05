using MazzaTech.Business.Intefaces;
using MazzaTech.Business.Intefaces.repository;
using MazzaTech.Business.Intefaces.services;
using MazzaTech.Business.Models;
using MazzaTech.Business.Models.Validations;

namespace MazzaTech.Business.Services
{
    public class EnderecoService : BaseService, IEnderecoService
    {
        private readonly IClienteRepository _ClienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IClienteRepository ClienteRepository, 
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _ClienteRepository = ClienteRepository;
            _enderecoRepository = enderecoRepository;
        }


        public async Task<List<EnderecoEntity>> ObterTodos()
        {
            return await _enderecoRepository.ObterTodos();
        }

        public async Task<EnderecoEntity> ObterPorId(Guid id)
        {
            return await _enderecoRepository.ObterPorId(id);
        }

        public async Task Adicionar(EnderecoEntity endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            if (!_ClienteRepository.Buscar(f => f.Id == endereco.ClienteId).Result.Any())
            {
                Notificar("Não existe um Cliente com 'id' infomado.");
                return;
            }

            await _enderecoRepository.Adicionar(endereco);
        }

        public async Task Atualizar(EnderecoEntity endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            if (!_ClienteRepository.Buscar(f => f.Id == endereco.ClienteId).Result.Any())
            {
                Notificar("Não existe um Cliente com este 'id' infomado.");
                return;
            }

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            var endereco = _enderecoRepository.ObterPorId(id).Result;
            if (endereco == null)
            {
                Notificar("Endereço não encontrado na base de dados!");
                return;
            }

            await _ClienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _ClienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}