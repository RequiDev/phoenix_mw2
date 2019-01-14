using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.ModernWarfare2.Structs
{

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal struct ClientInfo
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xC)]
		public byte[] unk; //0x0
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] name; //0xC 
		public int Team; //0x1C 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] unk1; //0x20 
		public int rank; //0x24 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x504)]
		public byte[] unk2; //0x28 
	}
}
