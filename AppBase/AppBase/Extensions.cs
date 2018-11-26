using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace AppBase
{
	public static class Extensions
	{
		public static void EnviaMensagemPraAtivarOutraJanela()
		{
			NativeMethods.PostMessage(
				(IntPtr)NativeMethods.HWND_BROADCAST,
				NativeMethods.WM_SHOWME,
				IntPtr.Zero,
				IntPtr.Zero);
		}

		public static void BringToFrontIfOtherInstance(this Window window, object sender, RoutedEventArgs e)
		{
			HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(window).Handle);
			source.AddHook(WndProc);
		}

		static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == NativeMethods.WM_SHOWME)
			{
				var window = Application.Current.MainWindow;
				if (window == null) return IntPtr.Zero;
				window.Activate();
				window.WindowState = WindowState.Maximized;
			}

			return IntPtr.Zero;
		}

		static class NativeMethods
		{
			public const int HWND_BROADCAST = 0xffff;
			public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
			[DllImport("user32")]
			public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
			[DllImport("user32")]
			static extern int RegisterWindowMessage(string message);
		}

	}
}
