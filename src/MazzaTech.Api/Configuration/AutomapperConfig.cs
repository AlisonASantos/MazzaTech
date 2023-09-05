using AutoMapper;
using MazzaTech.Api.ViewModels;
using MazzaTech.Business.Models;

namespace MazzaTech.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ClienteEntity, ClienteViewModel>().ReverseMap();
            CreateMap<EnderecoEntity, EnderecoViewModel>().ReverseMap();
        }
    }
}