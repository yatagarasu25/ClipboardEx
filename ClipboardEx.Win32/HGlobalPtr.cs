using System;
using System.Runtime.InteropServices;

namespace ClipboardEx.Win32
{
	public class HGlobalPtr : IDisposable
	{
		IntPtr ptr = IntPtr.Zero;
		public static implicit operator IntPtr(HGlobalPtr ptr) => ptr.ptr;

		public HGlobalPtr(IntPtr ptr)
		{
			this.ptr = ptr;
		}

		public void Dispose()
		{
			if (ptr != IntPtr.Zero)
				Marshal.FreeHGlobal(ptr);
		}
	}
}
