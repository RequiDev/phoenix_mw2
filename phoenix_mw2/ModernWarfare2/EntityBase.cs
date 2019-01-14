using Phoenix.MemorySystem;
using Phoenix.ModernWarfare2.Structs;
using System;
using System.Collections.Generic;

namespace Phoenix.ModernWarfare2
{
    internal class EntityBase
    {
        private static ProcessMemory Memory => Phoenix.Memory;

        public static void Update()
        {
            if (Phoenix.EntityList == null)
                Phoenix.EntityList = new EntityList();

            var players = new List<Entity>(Memory.ReadArray<Entity>(SignatureManager.GetEntityList(), 18));
			var clients = new List<ClientInfo>(Memory.ReadArray<ClientInfo>((IntPtr)0x997860, 18));
			
            Phoenix.EntityList.Players = players;
			Phoenix.EntityList.Clients = clients;
		}
    }
}
