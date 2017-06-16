using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS
{
	public partial class frmVersionInfo : Form
	{
		public frmVersionInfo()
		{
			InitializeComponent();
		}

		private void frmVersionInfo_Load(object sender, EventArgs e)
		{
			labelProductName.Text = Application.ProductName + " v" + Application.ProductVersion;
		}
	}
}