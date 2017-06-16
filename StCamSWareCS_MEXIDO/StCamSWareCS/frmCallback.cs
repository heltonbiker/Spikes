using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS
{
	public partial class frmCallback : Form
	{
		public frmCallback(bool[] aIsEnabled)
		{
			InitializeComponent();
			m_aIsEnabled = aIsEnabled;
		}
		private bool[] m_aIsEnabled = null;
		public bool[] IsEnabled
		{
			get { return (m_aIsEnabled); }
		}

		private void frmCallback_Load(object sender, EventArgs e)
		{
			if (5 <= m_aIsEnabled.GetLength(0))
			{
				for (int i = 0; i < 5; i++)
				{
					string name = "";
					switch (i)
					{
						case (0):
							name = "Grid";
							break;
						case (1):
							name = "Ellipse";
							break;
						case (2):
							name = "FPS";
							break;
						case (3):
							name = "Logo";
							break;
						case (4):
							name = "Frames Received";
							break;
					}
					if (m_aIsEnabled[i])
					{
						listBoxEnabled.Items.Add(new CCallbackItem(name, i));
					}
					else
					{
						listBoxDisabled.Items.Add(new CCallbackItem(name, i));
					}
				}
				mUpdateEnableDisableBtn();

			}

		}

		private void btnEnabled_Click(object sender, EventArgs e)
		{
			if (0 <= listBoxDisabled.SelectedIndex)
			{
				CCallbackItem ci = listBoxDisabled.Items[listBoxDisabled.SelectedIndex] as CCallbackItem;
				listBoxEnabled.Items.Add(ci);
				listBoxDisabled.Items.RemoveAt(listBoxDisabled.SelectedIndex);
				m_aIsEnabled[ci.Index] = true;

				mUpdateEnableDisableBtn();
			}
		}

		private void btnDisabled_Click(object sender, EventArgs e)
		{
			if (0 <= listBoxEnabled.SelectedIndex)
			{
				CCallbackItem ci = listBoxEnabled.Items[listBoxEnabled.SelectedIndex] as CCallbackItem;
				listBoxDisabled.Items.Add(ci);
				listBoxEnabled.Items.RemoveAt(listBoxEnabled.SelectedIndex);

				m_aIsEnabled[ci.Index] = false;
				mUpdateEnableDisableBtn();
			}
		}

		private void mUpdateEnableDisableBtn()
		{
			if (0 < listBoxEnabled.Items.Count)
			{
				btnDisabled.Enabled = true;
				listBoxEnabled.SelectedIndex = 0;
			}
			else
			{
				btnDisabled.Enabled = false;
			}
			if (0 < listBoxDisabled.Items.Count)
			{
				btnEnabled.Enabled = true;
				listBoxDisabled.SelectedIndex = 0;
			}
			else
			{
				btnEnabled.Enabled = false;
			}


		}

		private class CCallbackItem
		{
			private string m_strName = "";
			private int m_Index = 0;
			public CCallbackItem(string name, int index)
			{
				m_strName = name;
				m_Index = index;
			}

			public override string ToString()
			{
				return (m_strName);
			}
			public int Index
			{
				get { return (m_Index); }
			}
		}

	}
}