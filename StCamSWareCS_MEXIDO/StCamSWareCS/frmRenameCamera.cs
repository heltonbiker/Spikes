using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS
{
	public partial class frmRenameCamera : Form
	{
		public frmRenameCamera()
		{
			InitializeComponent();
		}
		public string CameraName
		{
			get { return (textBox.Text); }
			set { textBox.Text = value; }
		}
	}
}