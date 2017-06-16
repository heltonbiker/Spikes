using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace AplicaçãoPai
{
	public class ViewModel : ViewModelBase
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;

			public int Width { get { return Math.Abs(right - left); } }
			public int Height { get { return Math.Abs(bottom - top); } }
		}

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT Rect);

		Process _processoFilho = new Process();

		public ViewModel()
		{
			string filename = Path.Combine(Environment.CurrentDirectory, "../../../AplicaçãoFilha/bin/Debug/AplicaçãoFilha.exe");
			//string filename = @"C:\Miotec\BioGames\FarmWindows\FarmVR.exe";

			if (!File.Exists(filename))
				throw new IOException(filename + " não existe");

			_processoFilho.StartInfo.FileName = filename;
			_processoFilho.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

			_processoFilho.Start();
		}



		public ICommand ComandoMover
			=> new RelayCommand<string>(Mover);
		void Mover(string pos)
		{
			int left = 0, top = 0;

			RECT currentRect = new RECT();
			GetWindowRect(_processoFilho.MainWindowHandle, ref currentRect);

			double screenWidth = SystemParameters.MaximizedPrimaryScreenWidth;
			double screenHeight = SystemParameters.MaximizedPrimaryScreenHeight;			


			if (pos.StartsWith("T"))
			{
				top = 0;
			}
			else 
			if (pos.StartsWith("B"))
			{
				top = Convert.ToInt32(screenHeight - currentRect.Height);
			}

			if (pos.EndsWith("R"))
			{
				left = Convert.ToInt32(screenWidth - currentRect.Width);
			}
			else
			if (pos.EndsWith("L"))
			{
				left = 0;
			}


			MoveWindow(_processoFilho.MainWindowHandle, left, top, currentRect.Width, currentRect.Height, true);

		}

	}
}
