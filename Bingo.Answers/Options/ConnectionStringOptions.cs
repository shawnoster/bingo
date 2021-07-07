// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Options
{
    public class ConnectionStringOptions
    {
        public string ServiceEndpoint { get; set; }
        public string PrimaryKey { get; set; }

        public void Deconstruct(out string serviceEndpoint, out string primaryKey)
        {
            serviceEndpoint = ServiceEndpoint;
            primaryKey = PrimaryKey;
        }
    }
}
