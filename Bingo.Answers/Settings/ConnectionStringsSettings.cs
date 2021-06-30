// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Settings
{
    public enum ConnectionStringMode
    {
        Azure,
        Emulator
    }

    public class ConnectionStringsSettings
    {
        public ConnectionStringMode ConnectionMode { get; set; }
        public ConnectionStringSettings Azure { get; set; }
        public ConnectionStringSettings Emulator { get; set; }

        public ConnectionStringSettings ActiveConnectionStringOptions =>
            ConnectionMode == ConnectionStringMode.Azure ? Azure : Emulator;
    }
}
