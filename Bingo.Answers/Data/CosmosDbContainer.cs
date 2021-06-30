// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Data
{
    using Microsoft.Azure.Cosmos;

    public class CosmosDbContainer : ICosmosDbContainer
    {
        public Container Container { get; }

        public CosmosDbContainer(CosmosClient cosmosClient,
                                 string databaseName,
                                 string containerName)
        {
            Container = cosmosClient.GetContainer(databaseName, containerName);
        }
    }
}
