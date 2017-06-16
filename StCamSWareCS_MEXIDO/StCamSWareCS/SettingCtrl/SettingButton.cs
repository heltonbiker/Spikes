using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingButton : Button, ISettingValue
	{
		public SettingButton()
		{
			InitializeComponent();
		}

		private int m_nPos = 0;
		#region ISettingValue
		private SettingIDs m_SettingId = SettingIDs.UNKNOWN;
		public SettingCtrl.SettingIDs SettingID
		{
			get
			{
				return (m_SettingId);
			}
			set
			{
				m_SettingId = value;
			}
		}

		public int SettingValue
		{
			get
			{
				return (m_nPos);
			}
			set
			{
				m_nPos = value;
			}
		}

		public event System.EventHandler SettingValueChanged = null;

		#endregion



		private void SettingButton_Click(object sender, EventArgs e)
		{

			if (SettingValueChanged != null)
			{
				SettingValueChanged(this, new EventArgs());
			}
		}
	}
}
