using AutoMapper;
using MazzaTech.Api.Controllers;
using MazzaTech.Api.Extensions;
using MazzaTech.Api.ViewModels;
using MazzaTech.Business.Intefaces;
using MazzaTech.Business.Intefaces.repository;
using MazzaTech.Business.Intefaces.services;
using MazzaTech.Business.Models;
using MazzaTech.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MazzaTech.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Clientes")]
    public class ClientesController : MainController
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(IMapper mapper, 
                                      IClienteService ClienteService,
                                      INotificador notificador, 
                                      IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _clienteService = ClienteService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteService.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> ObterPorId(Guid id)
        {
            var Cliente = _mapper.Map<ClienteViewModel>(await _clienteService.ObterPorId(id));

            if (Cliente == null) return NotFound();

            return Cliente;
        }

        [ClaimsAuthorize("Cliente","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> Adicionar(ClienteViewModel ClienteViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.Adicionar(_mapper.Map<ClienteEntity>(ClienteViewModel));

            return CustomResponse(ClienteViewModel);
        }

        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Atualizar(Guid id, ClienteViewModel ClienteViewModel)
        {
            if (id != ClienteViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(ClienteViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.Atualizar(_mapper.Map<ClienteEntity>(ClienteViewModel));

            return CustomResponse(ClienteViewModel);
        }

        [ClaimsAuthorize("Cliente", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(Guid id)
        {
            var ClienteViewModel = await _clienteService.ObterPorId(id);

            if (ClienteViewModel == null) return NotFound();

            await _clienteService.Remover(id);

            return CustomResponse(ClienteViewModel);
        }
    }
}