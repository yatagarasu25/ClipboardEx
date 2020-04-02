using System;
using System.Runtime.InteropServices;
using static ClipboardEx.Win32.NativeFunctions;

namespace ClipboardEx.Win32
{
	public class GlobalDataLock : IDisposable
	{
		IntPtr handle;
		IntPtr pointer;

		public bool IsNull => handle == IntPtr.Zero;
		public byte[] Data;

		public GlobalDataLock(IntPtr handle)
		{
			this.handle = handle;

			if (!IsNull)
			{
				pointer = GlobalLock(handle);

				if (pointer != IntPtr.Zero)
				{
					int size = GlobalSize(handle);
					Data = new byte[size];

					Marshal.Copy(pointer, Data, 0, size);
				}
			}
		}

		public void Dispose()
		{
			if (pointer != IntPtr.Zero)
			{
				GlobalUnlock(pointer);
			}
		}
	}

	public static class GlobalDataEx
	{
		public static byte[] GetBytes(this IntPtr handle)
		{
			using (var gd = new GlobalDataLock(handle))
				return gd.Data;
		}
	}
}
