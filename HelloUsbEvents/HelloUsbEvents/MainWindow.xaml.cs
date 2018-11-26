using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace HelloUsbEvents
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
			//HwndSource source = PresentationSource.FromVisual(this) as HwndSource;

			if (source != null)
			{
				//var windowHandle = source.Handle;
				source.AddHook(HwndHandler);
				//UsbNotification.RegisterUsbDeviceNotification(windowHandle);
			}
		}

		/// <summary>
		/// Method that receives window messages.
		/// </summary>
		private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
		{
			//Console.WriteLine($"{hwnd}; {msg:X}; {(long)wparam:X}; {lparam}");

			if (msg == UsbNotification.WmDevicechange)
			{
				switch ((int)wparam)
				{
					case UsbNotification.DbtDeviceremovecomplete:
						Console.WriteLine("USB Device Removed"); //Usb_DeviceRemoved(); // this is where you do your magic
						break;
					case UsbNotification.DbtDevicearrival:
						Console.WriteLine("USB Device Inserted"); //Usb_DeviceAdded(); // this is where you do your magic
						break;
				}
			}

			handled = false;
			return IntPtr.Zero;
		}
	}

	internal static class UsbNotification
	{
		public const int DbtDevicearrival = 0x8000; // system detected a new device        
		public const int DbtDeviceremovecomplete = 0x8004; // device is gone      
		public const int WmDevicechange = 0x0219; // device change event      
		private const int DbtDevtypDeviceinterface = 5;
		private static readonly Guid GuidDevinterfaceUSBDevice = new Guid("A5DCBF10-6530-11D2-901F-00C04FB951ED"); // USB devices
		private static IntPtr notificationHandle;

		/// <summary>
		/// Registers a window to receive notifications when USB devices are plugged or unplugged.
		/// </summary>
		/// <param name="windowHandle">Handle to the window receiving notifications.</param>
		public static void RegisterUsbDeviceNotification(IntPtr windowHandle)
		{
			DevBroadcastDeviceinterface dbi = new DevBroadcastDeviceinterface
			{
				DeviceType = DbtDevtypDeviceinterface,
				Reserved = 0,
				ClassGuid = GuidDevinterfaceUSBDevice,
				Name = 0
			};

			dbi.Size = Marshal.SizeOf(dbi);
			IntPtr buffer = Marshal.AllocHGlobal(dbi.Size);
			Marshal.StructureToPtr(dbi, buffer, true);

			notificationHandle = RegisterDeviceNotification(windowHandle, buffer, 0);
		}

		/// <summary>
		/// Unregisters the window for USB device notifications
		/// </summary>
		public static void UnregisterUsbDeviceNotification()
		{
			UnregisterDeviceNotification(notificationHandle);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);

		[DllImport("user32.dll")]
		private static extern bool UnregisterDeviceNotification(IntPtr handle);

		[StructLayout(LayoutKind.Sequential)]
		private struct DevBroadcastDeviceinterface
		{
			internal int Size;
			internal int DeviceType;
			internal int Reserved;
			internal Guid ClassGuid;
			internal short Name;
		}
	}
}
