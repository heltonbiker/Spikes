using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SensorTechnology;

namespace StCamSWareCS
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			frmSelectCamera selectCamera = new frmSelectCamera();
			if (DialogResult.OK == selectCamera.ShowDialog())
			{
				CStCamera stCamera = selectCamera.OpendCamera;
				selectCamera.Dispose();

				if (stCamera != null)
				{
					Application.Run(new frmMain(stCamera));
				}
			}
		}
	}
}