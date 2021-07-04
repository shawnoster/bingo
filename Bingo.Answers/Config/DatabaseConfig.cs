// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Config
{
    using System;
    using Bingo.Answers.Data;
    using Bingo.Answers.Interfaces;
    using Bingo.Answers.Extensions;
    using Bingo.Answers.Options;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DatabaseConfig
    {
        /// <summary>
        /// Setup Cosmos DB
        /// </summary>
        public static void SetupCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind database options. Invalid configuration will terminate the application startup.
            ConnectionStringsOptions connectionStringsSettings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsOptions>();
            CosmosDbOptions cosmosDbSettings = configuration.GetSection("CosmosDb").Get<CosmosDbOptions>();
            var (serviceEndpoint, primaryKey) = connectionStringsSettings.ActiveConnectionStringOptions;

            // register CosmosDB client and data repositories
            services.AddCosmosDb(serviceEndpoint,
                                   primaryKey,
                                   cosmosDbSettings.DatabaseName,
                                   cosmosDbSettings.Containers);

            services.AddScoped<IAnswerRepository, AnswerRepository>();
        }
    }
}
