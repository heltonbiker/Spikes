using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingPixelPosition : UserControl
	{
		public SettingPixelPosition()
		{
			InitializeComponent();
		}
		public void SetCtrlIDX(SettingIDs id, string strTitle)
		{
			labelDefectPixelPositionX.Text = strTitle;
			trackBarDefectPixelPositionX.SettingID = id;
			numericUpDownDefectPixelPositionX.SettingID = id;
		}
		public void SetCtrlIDY(SettingIDs id, string strTitle)
		{
			labelDefectPixelPositionY.Text = strTitle;
			trackBarDefectPixelPositionY.SettingID = id;
			numericUpDownDefectPixelPositionY.SettingID = id;
		}
	}
}
