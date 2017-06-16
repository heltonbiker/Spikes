using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingNumericUpDown : NumericUpDown, ISettingRange
	{
		public SettingNumericUpDown()
		{
			InitializeComponent();
		}
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
				return ((int)Value);
			}
			set
			{
				if (value < Minimum)
				{
					Value = Minimum;
				}
				else if (Maximum < value)
				{
					Value = Maximum;
				}
				else
				{
					Value = value;
				} 
			}
		}

		public event System.EventHandler SettingValueChanged = null;

		#endregion


		#region ISettingRange

		public int SettingMin
		{
			get
			{
				return ((int)Minimum);
			}
			set
			{
				Minimum = value;
			}
		}

		public int SettingMax
		{
			get
			{
				return ((int)Maximum);
			}
			set
			{
				Maximum = value;
			}
		}

		#endregion

		private void SettingNumericUpDown_Leave(object sender, EventArgs e)
		{
			Text = Value.ToString();
		}



		private void SettingNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (this.Focused)
			{
				if (SettingValueChanged != null)
				{
					SettingValueChanged(this, new EventArgs());
				}
			}

		}
	}
}
