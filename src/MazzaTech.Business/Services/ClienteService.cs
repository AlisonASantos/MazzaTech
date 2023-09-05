using MazzaTech.Business.Intefaces;
using MazzaTech.Business.Intefaces.repository;
using MazzaTech.Business.Intefaces.services;
using MazzaTech.Business.Models;
using MazzaTech.Business.Models.Validations;

namespace MazzaTech.Business.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public ClienteService(IClienteRepository ClienteRepository, 
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _clienteRepository = ClienteRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<List<ClienteEntity>> ObterTodos()
        {
            return await _clienteRepository.ObterTodos();
        }

        public async Task<ClienteEntity> ObterPorId(Guid id)
        {
            return await _clienteRepository.ObterPorId(id);
        }

        public async Task Adicionar(ClienteEntity Cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), Cliente)) return;

            foreach (var item in Cliente.Enderecos)
            {
                if (!ExecutarValidacao(new EnderecoValidation(), item)) return;

            }

            if (_clienteRepository.Buscar(f => f.Email == Cliente.Email).Result.Any())
            {
                Notificar("Já existe um Cliente com este email infomado.");
                return;
            }

            if (_clienteRepository.Buscar(f => f.Enderecos == Cliente.Enderecos).Result.Any())
            {
                Notificar("Já existe este Endereço cadastrado.");
                return;
            }

            await _clienteRepository.Adicionar(Cliente);
        }

        public async Task Atualizar(ClienteEntity Cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), Cliente)) return;

            if (_clienteRepository.Buscar(f => f.Email == Cliente.Email && f.Id != Cliente.Id).Result.Any())
            {
                Notificar("Já existe um Cliente com este email infomado.");
                return;
            }


            if (_clienteRepository.Buscar(f => f.Enderecos == Cliente.Enderecos).Result.Any())
            {
                Notificar("Já existe este Endereço cadastrado.");
                return;
            }

            await _clienteRepository.Atualizar(Cliente);
        }

        public async Task Remover(Guid id)
        {
            var cliente = _clienteRepository.ObterPorId(id).Result;
            if (cliente == null)
            {
                Notificar("Cliente não encontrado na base de dados!");
                return;
            }

            var endereco = await _clienteRepository.ObterListaEndereco(id);

            if (endereco != null)
            {
                endereco.ForEach(e => _enderecoRepository.Remover(e.Id));
            }

            await _clienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}