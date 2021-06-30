// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Data
{
    using Microsoft.Azure.Cosmos;

    public interface ICosmosDbContainer
    {
        /// <summary>
        /// Azure Cosmos DB Container
        /// </summary>
        Container Container { get; }
    }
}
