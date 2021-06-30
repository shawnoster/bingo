// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Settings
{
    using System.Collections.Generic;

    public class CosmosDbSettings
    {
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
