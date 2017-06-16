using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SensorTechnology;
using System.Collections;
namespace StCamSWareCS
{
	public partial class frmSelectCamera : Form
	{
		public frmSelectCamera()
		{
			InitializeComponent();
		}
	
		private System.EventHandler eventActivated = null;
		private ArrayList m_arrayStCamera = new ArrayList(127);
		private int m_keepOpenIndex = -1;

		private bool mOpenAllCamera()
		{
			bool result = true;
			for (int i = 0; i < m_arrayStCamera.Capacity; i++)
			{
				CStCamera stCamera = new CStCamera();
				if (stCamera.Open())
				{
					m_arrayStCamera.Add(stCamera);
				}
				else
				{
					break;
				}
			}
			return (result);
		}

		private bool mCloseCamera()
		{
			bool result = true;
			while ((m_keepOpenIndex < m_arrayStCamera.Count - 1) && (0 < m_arrayStCamera.Count))
			{
				CStCamera stCamera = m_arrayStCamera[m_arrayStCamera.Count - 1] as CStCamera;
				m_arrayStCamera.RemoveAt(m_arrayStCamera.Count - 1);
				stCamera.Dispose();
			}

			while (1 < m_arrayStCamera.Count)
			{
				CStCamera stCamera = m_arrayStCamera[0] as CStCamera;
				m_arrayStCamera.RemoveAt(0);
				stCamera.Dispose();
			}

			return (result);
		}

		public CStCamera OpendCamera
		{
			get
			{
				CStCamera stCamera = null;
				if((0 <= m_keepOpenIndex) && (m_keepOpenIndex < m_arrayStCamera.Count))
				{
					stCamera = m_arrayStCamera[m_keepOpenIndex] as CStCamera;
				}

				return (stCamera);
			}
		}

		private bool mUpdateDisplay()
		{
			bool result = true;
			int nSelectedIndex = cmbCameraList.SelectedIndex;

			cmbCameraList.Items.Clear();

			foreach (CStCamera stCamera in m_arrayStCamera)
			{
				cmbCameraList.Items.Add(stCamera);
			}

			if (0 < cmbCameraList.Items.Count)
			{
				if (nSelectedIndex < 0)
				{
					nSelectedIndex = 0;
				}
				else if (cmbCameraList.Items.Count <= nSelectedIndex)
				{
					nSelectedIndex = cmbCameraList.Items.Count - 1;
				}

				cmbCameraList.SelectedIndex = nSelectedIndex;

				btnOK.Enabled = true;
				cmbCameraList.Enabled = true;
				
				if(cmbCameraList.Items.Count == 1)
				{
					btnOK.PerformClick();
				}
			}
			else
			{
				btnOK.Enabled = false;
				cmbCameraList.Enabled = false;
			}
			return (result);
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			m_keepOpenIndex = cmbCameraList.SelectedIndex;
		}

		private void frmSelectCamera_Activated(object sender, System.EventArgs e)
		{
			Activated -= eventActivated;
			timerCheckCamera.Enabled = true;
			btnOK.Enabled = false;
			cmbCameraList.Enabled = false;
		}

		private void frmSelectCamera_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			timerCheckCamera.Enabled = false;
			mCloseCamera();
		}

		private void timerCheckCamera_Tick(object sender, System.EventArgs e)
		{
			bool result = true;

			do
			{
				result = mCloseCamera();
				if (!result) break;

				result = mOpenAllCamera();
				if (!result) break;

				result = mUpdateDisplay();
				if (!result) break;
			} while (false);
		}


	}
}