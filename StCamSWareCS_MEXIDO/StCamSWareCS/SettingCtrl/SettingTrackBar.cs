using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingTrackBar : TrackBar,  ISettingRange
	{
		public SettingTrackBar()
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
				return (Value);
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
				return (Minimum);
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
				return (Maximum);
			}
			set
			{
				Maximum = value;
			}
		}

		#endregion

		private void SettingTrackBar_Scroll(object sender, EventArgs e)
		{

			if (SettingValueChanged != null)
			{
				SettingValueChanged(this, new EventArgs());
			}
		}
	}
}
