using System;
using System.Text;
using static ClipboardEx.Win32.NativeFunctions;

namespace ClipboardEx.Win32
{
	public class Clipboard : IDisposable
	{
		public ClipboardFormat Format { get; protected set; }

		protected Clipboard(ClipboardFormat format = ClipboardFormat.ANY)
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
		}

		public string Text {
			get {
				IntPtr handle = GetClipboardData(Format);
				byte[] buff = handle.GetBytes();

				switch (Format)
				{
					case ClipboardFormat.UNICODETEXT:
						return Encoding.Unicode.GetString(buff).TrimEnd('\0');
				}

				return null;
			}
		}

		public void Dispose()
		{
			CloseClipboard();
		}
	}
}
