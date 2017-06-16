using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingCheckBox : CheckBox, ISettingValue
	{
		public SettingCheckBox()
		{
			InitializeComponent();
		}

		private SettingIDs m_SettingID = SettingIDs.UNKNOWN;

		public event System.EventHandler SettingValueChanged = null;

		public SettingIDs SettingID
		{
			get { return (m_SettingID); }
			set { m_SettingID = value; }
		}
		public int SettingValue
		{
			get
			{
				int val = 0;
				if (Checked)
				{
					val = CheckedValue;
				}
				return (val);
			}
			set
			{
				Checked = (value != 0);
			}
		}

		protected int m_CheckedValue = -1;
		public int CheckedValue
		{
			set { m_CheckedValue = value; }
			get { return (m_CheckedValue); }
		}

		private void SettingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (SettingValueChanged != null)
			{
				SettingValueChanged(sender, e);
			}
		}
	}
}
