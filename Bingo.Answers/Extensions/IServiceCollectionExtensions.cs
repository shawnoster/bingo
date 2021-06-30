// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Extensions
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.DependencyInjection;
    using Bingo.Answers.Data;
    using Bingo.Answers.Settings;

    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Register a singleton instance of Cosmos Db Container Factory, which is a wrapper for the CosmosClient.
        /// </summary>
        public static IServiceCollection AddCosmosDb(this IServiceCollection services,
                                                     string endpointUrl,
                                                     string primaryKey,
                                                     string databaseName,
                                                     List<ContainerInfo> containers)
        {
            var cosmosClient = new CosmosClientBuilder(endpointUrl, primaryKey)
                .WithSerializerOptions(
                    new CosmosSerializationOptions
                    {
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                    })
                .Build();

            var cosmosDbContainerFactory = new CosmosDbContainerFactory(
                databaseName,
                containers,
                cosmosClient);

            // Microsoft recommends a singleton client instance to be used throughout the application
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.cosmos.cosmosclient?view=azure-dotnet#definition
            // "CosmosClient is thread-safe. Its recommended to maintain a single instance of CosmosClient per lifetime of the application which enables efficient connection management and performance"
            services.AddSingleton<ICosmosDbContainerFactory>(cosmosDbContainerFactory);

            return services;
        }
    }
}
