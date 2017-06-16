using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SensorTechnology;

namespace StCamSWareCS
{
	public partial class frmColorMatrix : frmSetBase
	{
		public frmColorMatrix(CStCamera stCamera)
			: base(stCamera)
		{
			InitializeComponent();
		}

		private void frmColorMatrix_Load(object sender, EventArgs e)
		{
			SetSettingValueChangeEvent(this.Controls);
			InitComboBox(this.Controls);
			UpdateDisplay();
		}
		protected override void UpdateDisplay()
		{
			UpdateSettingCtrls(this.Controls);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnConst_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			short[] pshtMat = null;

			switch(int.Parse(btn.Tag.ToString()))
			{
				case(0):
					pshtMat = new short[] { 100, 0, 0, 0, 0, 100, 0, 0, 0, 0, 100, 0 };
					break;
				case (1):
					pshtMat = new short[] { 30, 59, 11, 0, 30, 59, 11, 0, 30, 59, 11, 0 };
					break;
				case (2):
					pshtMat = new short[] { -100, 0, 0, 25500, 0, -100, 0, 25500, 0, 0, -100, 25500 };
					break;
			}
			if (pshtMat != null)
			{
				m_StCamera.ColorMatrix = pshtMat;
				UpdateDisplay();
			}

		}
	}
}

