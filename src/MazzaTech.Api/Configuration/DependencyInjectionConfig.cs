using MazzaTech.Api.Extensions;
using MazzaTech.Business.Intefaces;
using MazzaTech.Business.Intefaces.repository;
using MazzaTech.Business.Intefaces.services;
using MazzaTech.Business.Notificacoes;
using MazzaTech.Business.Services;
using MazzaTech.Data.Context;
using MazzaTech.Data.Repository;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MazzaTech.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}