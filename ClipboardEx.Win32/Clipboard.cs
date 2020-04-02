using System;
using System.Runtime.InteropServices;
using System.Text;
using static ClipboardEx.Win32.NativeFunctions;

namespace ClipboardEx.Win32
{
	public class Clipboard : IDisposable
	{
		public ClipboardFormat Format { get; protected set; }
		public Lazy<byte[]> Data { get; protected set; }

		public Clipboard(ClipboardFormat format = ClipboardFormat.ANY)
		{
			Format = ClipboardFormat.EMPTY;

			if (format != ClipboardFormat.ANY)
			{
				if (!IsClipboardFormatAvailable(format))
					return;
			}

			if (!OpenClipboard(IntPtr.Zero))
				return;

			Format = EnumClipboardFormats(ClipboardFormat.ANY);
			Data = new Lazy<byte[]>(() => GetClipboardData(Format).GetBytes());
		}

		public string Text {
			get {
				switch (Format)
				{
					case ClipboardFormat.UNICODETEXT:
						return Encoding.Unicode.GetString(Data.Value).TrimEnd('\0');
				}

				return null;
			}
			set {
				EmptyClipboard();
				Format = ClipboardFormat.EMPTY;
				Data = new Lazy<byte[]>(() => GetClipboardData(Format).GetBytes());

				using (var ptr = new HGlobalPtr(Marshal.StringToHGlobalUni(value)))
				{
					Format = ClipboardFormat.UNICODETEXT;
					SetClipboardData(Format, ptr);
				}
			}
		}

		public void Dispose()
		{
			CloseClipboard();
		}
	}
}
