using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace SensorTechnology
{
	class Native
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetScrollPos(IntPtr hWnd, int nBar);
		public const int SB_HORZ = 0x0;
		public const int SB_VERT = 0x1;


		[DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		private static extern int FormatMessage(int dwFlags, IntPtr lpSource,
			int dwMessageId, int dwLanguageId, System.Text.StringBuilder lpBuffer, int nSize, IntPtr Arguments);
		[DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		private static extern IntPtr LoadLibraryEx(String lpszLibFile, IntPtr hFile, int dwFlags);
		[DllImport("kernel32.dll")]
		private static extern int FreeLibrary(IntPtr hModule);


		public static string GetErrorMsg(uint dwErrorCode)
		{

			int FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
			int FORMAT_MESSAGE_FROM_HMODULE = 0x00000800;

			int DONT_RESOLVE_DLL_REFERENCES = 0x00000001;

			System.Text.StringBuilder strErrorMsg = new System.Text.StringBuilder(255);
			int dwFlags = FORMAT_MESSAGE_FROM_SYSTEM;
			IntPtr ptrlpSource = IntPtr.Zero;

			int iFoundErrMsg = FormatMessage(dwFlags, ptrlpSource, (int)dwErrorCode, 0, strErrorMsg, strErrorMsg.Capacity, IntPtr.Zero);
			if (iFoundErrMsg == 0)
			{
				ptrlpSource = LoadLibraryEx("StCamMsg.dll", IntPtr.Zero, DONT_RESOLVE_DLL_REFERENCES);
				dwFlags |= FORMAT_MESSAGE_FROM_HMODULE;

				if (!ptrlpSource.Equals(IntPtr.Zero))
				{
					iFoundErrMsg = FormatMessage(dwFlags, ptrlpSource, (int)dwErrorCode, 0, strErrorMsg, strErrorMsg.Capacity, IntPtr.Zero);
					FreeLibrary(ptrlpSource);
				}
			}
			return (strErrorMsg.ToString());
		}

		public static void ShowErrorMsg(uint dwErrorCode)
		{
			string strErrorMsg = GetErrorMsg(dwErrorCode);
			System.Windows.Forms.MessageBox.Show(strErrorMsg, "Error Message", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

		}

		public const uint ERROR_ACCESS_DENIED = 5;

	}
}
