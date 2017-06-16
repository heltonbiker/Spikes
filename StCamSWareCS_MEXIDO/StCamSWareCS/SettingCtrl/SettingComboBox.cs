using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingComboBox : ComboBox, ISettingValue
	{
		public SettingComboBox()
		{
			InitializeComponent();
		}

		private SettingIDs m_SettingID = SettingIDs.UNKNOWN;

		private void SettingComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			if (SettingValueChanged != null)
			{
				SettingValueChanged(sender, e);
			}
		}

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
				if (0 <= SelectedIndex)
				{
					CItemForComboBox ifcb = Items[SelectedIndex] as CItemForComboBox;
					val = ifcb.SettingValue;
				}
				return (val);
			}
			set
			{
				for (int index = 0; index < Items.Count; index++)
				{
					CItemForComboBox ifcb = Items[index] as CItemForComboBox;
					if (ifcb.SettingValue == value)
					{
						SelectedIndex = index;
						break;
					}
				}
			}
		}
	}
}
