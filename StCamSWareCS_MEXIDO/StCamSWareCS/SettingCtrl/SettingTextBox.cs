using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingTextBox : TextBox,  ISettingRange
	{
		public SettingTextBox()
		{
			InitializeComponent();
		}

		private int m_nPos = 0;
		private int m_nMin = int.MinValue;
		private int m_nMax = int.MaxValue;


		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				int val = m_nPos;
				try
				{
					val = int.Parse(Text);
				}
				catch (Exception)
				{
					Text = m_nPos.ToString();
				}

				if (val < m_nMin)
				{
					val = m_nMin;
				}
				else if (m_nMax < val)
				{
					val = m_nMax;
				}
				m_nPos = val;
				if (SettingValueChanged != null)
				{
					SettingValueChanged(this, new EventArgs());
				}
			}
			base.OnKeyDown(e);
		}

		#region ISettingValue
		private SettingIDs m_SettingID = SettingIDs.UNKNOWN;

		private void SettingTextBox_Leave(object sender, System.EventArgs e)
		{
			Text = m_nPos.ToString();
		}

		public SettingCtrl.SettingIDs SettingID
		{
			get
			{
				return (m_SettingID);
			}
			set
			{
				m_SettingID = value;
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
				Text = m_nPos.ToString();
			}
		}

		public event System.EventHandler SettingValueChanged;

		#endregion

		#region ISettingRange

		public int SettingMin
		{
			get
			{
				return (m_nMin);
			}
			set
			{
				m_nMin = value;
			}
		}

		public int SettingMax
		{
			get
			{
				return (m_nMax);
			}
			set
			{
				m_nMax = value;
			}
		}

		#endregion

		private void SettingTextBox_Leave_1(object sender, EventArgs e)
		{
			Text = m_nPos.ToString();
		}
	}
}
