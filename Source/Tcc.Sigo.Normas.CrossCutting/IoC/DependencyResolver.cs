using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Tcc.Sigo.Normas.Application;
using Tcc.Sigo.Normas.Application.Services;
using Tcc.Sigo.Normas.Domain.Adapters;
using Tcc.Sigo.Normas.Domain.Repositories;
using Tcc.Sigo.Normas.Domain.Services;
using Tcc.Sigo.Normas.MomAdapter;
using Tcc.Sigo.Normas.MomAdapter.Acl;
using Tcc.Sigo.Normas.Repository;
using Tcc.Sigo.Normas.Repository.Repositories;

namespace Tcc.Sigo.Normas.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterServices(services);
            RegisterAdapters(services);
            RegisterRepositories(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INormaService, NormaService>();
        }

        private static void RegisterAdapters(IServiceCollection services)
        {
            services.AddScoped<IAclAdapter, AclAdapter>();
            services.AddScoped<IMomAdapter, AzureServiceBusMomAdapter>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<INormaReadOnlyRepository, NormaRepository>();
            services.AddTransient<INormaWriteOnlyRepository, NormaRepository>();
        }
    }
}
