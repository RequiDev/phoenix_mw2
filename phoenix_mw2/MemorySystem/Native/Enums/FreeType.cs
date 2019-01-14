﻿using System;

namespace Phoenix.MemorySystem.Native.Enums
{
    [Flags]
    internal enum FreeType
    {
        Decommit = 0x4000,
        Release = 0x8000
    }
}
