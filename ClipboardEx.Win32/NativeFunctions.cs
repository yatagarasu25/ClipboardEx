using System;
using System.Runtime.InteropServices;

namespace ClipboardEx.Win32
{
	public enum ClipboardFormat : uint
	{
		EMPTY = 0,
		ANY = 0,
		TEXT = 1,
		BITMAP = 2,
		METAFILEPICT = 3,
		SYLK = 4,
		DIF = 5,
		TIFF = 6,
		OEMTEXT = 7,
		DIB = 8,
		PALETTE = 9,
		PENDATA = 10,
		RIFF = 11,
		WAVE = 12,
		UNICODETEXT = 13,
		ENHMETAFILE = 14,
		HDROP = 15,
		LOCALE = 16,
		DIBV5 = 17,
		MAX = 18,
		OWNERDISPLAY = 0x0080,
		DSPTEXT = 0x0081,
		DSPBITMAP = 0x0082,
		DSPMETAFILEPICT = 0x0083,
		DSPENHMETAFILE = 0x008E,
		PRIVATEFIRST = 0x0200,
		PRIVATELAST = 0x02FF,
		GDIOBJFIRST = 0x0300,
		GDIOBJLAST = 0x03FF
	}

	internal class NativeFunctions
	{
		#region Win32

		[DllImport("User32.dll", SetLastError = true)]
		public static extern ClipboardFormat EnumClipboardFormats(ClipboardFormat format);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsClipboardFormatAvailable(ClipboardFormat format);

		[DllImport("User32.dll", SetLastError = true)]
		public static extern IntPtr GetClipboardData(ClipboardFormat format);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseClipboard();

		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern IntPtr GlobalLock(IntPtr hMem);

		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GlobalUnlock(IntPtr hMem);

		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern int GlobalSize(IntPtr hMem);

		#endregion
	}
}
