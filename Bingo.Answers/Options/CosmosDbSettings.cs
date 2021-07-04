// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Options
{
    using System.Collections.Generic;

    public class CosmosDbOptions
    {
        /// <summary>
        /// Configuration section
        /// </summary>
        public const string CosmosDb = "CosmosDb";

        /// <summary>
        /// Database name
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// List of containers in the database
        /// </summary>
        public List<ContainerInfo> Containers { get; set; }
    }
}
