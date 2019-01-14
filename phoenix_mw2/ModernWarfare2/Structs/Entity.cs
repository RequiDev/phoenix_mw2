using Phoenix.ModernWarfare2.Enums;
using Phoenix.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.ModernWarfare2.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct Entity
	{
		public short IsValid; //0x0000  
		public short grenadeValid; //0x0002  
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[] unknown4; //0x0004
		public Vector3D Origin;
		public Vector3D Angles;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
		public byte[] unknown48; //0x0030
		public int IsZooming; //0x006C  
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public byte[] unknown112; //0x0070
		public Vector3D lerpOrigin2;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 84)]
		public byte[] unknown136; //0x0088
		public int ClientNum; //0x00DC  
		public int eType; //0x00E0  
		public EntityFlags Flags; //0x00E4  
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public byte[] unknown232; //0x00E8
		public Vector3D lerpOrigin3;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 112)]
		public byte[] unknown256; //0x0100
		public int clientNum2; //0x0170  
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 52)]
		public byte[] unknown372; //0x0174
		public short WeaponID; //0x01A8  
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] unknown426; //0x01AA
		public short WeaponID2; //0x01AC  
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 46)]
		public byte[] unknown430; //0x01AE
		public int IsAlive; //0x01DC  
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] unknown480; //0x01E0
		public int clientNum3; //0x0200  
	}
}
