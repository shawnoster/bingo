// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Options
{
    public enum ConnectionStringMode
    {
        Azure,
        Emulator
    }

    public class ConnectionStringsOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";

        public ConnectionStringMode ConnectionMode { get; set; }
        public ConnectionStringOptions Azure { get; set; }
        public ConnectionStringOptions Emulator { get; set; }

        public ConnectionStringOptions ActiveConnectionStringOptions =>
            ConnectionMode == ConnectionStringMode.Azure ? Azure : Emulator;
    }
}
