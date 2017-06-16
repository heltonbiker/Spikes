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
	public partial class frmAVISetting : StCamSWareCS.frmSetBase
	{
		public frmAVISetting(CStCamera stCamera)
			: base(stCamera)
		{
			InitializeComponent();
		}

		protected override void UpdateDisplay()
		{
			UpdateSettingCtrls(this.Controls);
			tbFileSize.Text = m_StCamera.AVIFileSize.ToString("#,##0") + "[bytes]";
			double time = m_StCamera.AVIFileTime;
			
			int min = 0;
			int sec = 0;
			int msec = 0;
			if(60.0 <= time)
			{
				min = (int)(time / 60.0);
				time -= min * 60.0;
			}
			if(1.0 <= time)
			{
				sec = (int)time;
				time -= sec;
			}
			msec = (int)(time * 1000.0);
			string strTime = ((0 < min)?(min.ToString() + "m"):"")
							+ ((0 < sec)?(sec.ToString() + "s"):"")
							+ msec.ToString() + "msec";
			tbRecordingTime.Text = strTime;
		}

		private void frmAVISetting_Load(object sender, EventArgs e)
		{
			SetSettingValueChangeEvent(this.Controls);
			InitComboBox(this.Controls);
			UpdateDisplay();

		}
	}
}

