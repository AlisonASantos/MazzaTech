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
    [Route("api/v{version:apiVersion}/Enderecos")]
    public class EnderecosController : MainController
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecosController(IMapper mapper,
                                    INotificador notificador,
                                    IUser user,
                                    IEnderecoService enderecoService) : base(notificador, user)
        {
            _mapper = mapper;
            _enderecoService = enderecoService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<EnderecoViewModel>> ObterTodos()
        {
            return _mapper.Map<List<EnderecoViewModel>>(await _enderecoService.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EnderecoViewModel>> ObterPorId(Guid id)
        {
            var Cliente = _mapper.Map<EnderecoViewModel>(await _enderecoService.ObterPorId(id));

            if (Cliente == null) return NotFound();

            return Cliente;
        }

        [ClaimsAuthorize("Cliente","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<EnderecoViewModel>> Adicionar(EnderecoViewModel enderecoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _enderecoService.Adicionar(_mapper.Map<EnderecoEntity>(enderecoViewModel));

            return CustomResponse(enderecoViewModel);
        }

        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Atualizar(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(enderecoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _enderecoService.Atualizar(_mapper.Map<EnderecoEntity>(enderecoViewModel));

            return CustomResponse(enderecoViewModel);
        }

        [ClaimsAuthorize("Cliente", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(Guid id)
        {
            var ClienteViewModel = await _enderecoService.ObterPorId(id);

            if (ClienteViewModel == null) return NotFound();

            await _enderecoService.Remover(id);

            return CustomResponse(ClienteViewModel);
        }
    }
}