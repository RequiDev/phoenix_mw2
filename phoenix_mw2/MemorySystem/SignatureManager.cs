using System;
using System.Diagnostics;
using System.Text;

namespace Phoenix.MemorySystem
{
    internal static class SignatureManager
    {
        private static ProcessMemory Memory => Phoenix.Memory;

        public static IntPtr GetViewAngle()
        {
			return (IntPtr)0xBC76D0;
		}

        public static IntPtr GetEntityList()
        {
			return (IntPtr)0x9A4090;
		}

        public static IntPtr GetWorldToViewMatrix()
        {
			return (IntPtr)0x90B5C8;
		}

        public static IntPtr GetLocalIndex()
        {
			return (IntPtr)0x8A0E50;
		}
    }
}
