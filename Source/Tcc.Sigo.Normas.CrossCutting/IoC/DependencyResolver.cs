﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Tcc.Sigo.Normas.Application.Services;
using Tcc.Sigo.Normas.Domain.Adapters;
using Tcc.Sigo.Normas.Domain.Repositories;
using Tcc.Sigo.Normas.Domain.Services;
using Tcc.Sigo.Normas.MomAdapter;
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
            //services.AddScoped<IUser, User>();
        }

        private static void RegisterAdapters(IServiceCollection services)
        {
            services.AddScoped<IMomAdapter, RabbitMQMomAdapter>();
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