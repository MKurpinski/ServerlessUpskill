﻿using Application.DataStorage.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DataStorage.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDataStorageModule(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<DataStorageOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.Bind(settings);
                });

            return builder.Services;
        }
    }
}