using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SensorTechnology;
using StCamSWareCS.SettingCtrl;

namespace StCamSWareCS
{
	public partial class frmSetting : frmSetBase
	{
		public frmSetting(CStCamera stCamera) : base(stCamera)
		{
			InitializeComponent();
			m_aTabPageList = new TabPage[tabControl.TabPages.Count];
			for (int index = 0; index < tabControl.TabPages.Count; index++)
			{
				m_aTabPageList[index] = tabControl.TabPages[index];
			}
		}

		private bool m_IsAdvanced = false;
		private TabPage[] m_aTabPageList = null;
		private Form m_frmSub = null;

		private void frmSetting_Load(object sender, System.EventArgs e)
		{
			SetSettingValueChangeEvent(this.Controls);
			InitComboBox(this.Controls);
			SetVisibleTabPages();
			UpdateDisplay();
		}
		protected override void UpdateDisplay()
		{
			if (tabControl.SelectedTab == null) return;
			UpdateSettingCtrls(tabControl.SelectedTab.Controls);
			btnALCWeightArea.Enabled = m_StCamera.IsAEEOn || m_StCamera.IsAGCOn;
			btnALCWeightArea.Visible = !m_StCamera.HasCameraSideALC;
		}


		private void btnSimpleAdvanced_Click(object sender, EventArgs e)
		{
			m_IsAdvanced = !m_IsAdvanced;
			btnSimpleAdvanced.Text = m_IsAdvanced ? "Simple" : "Advanced";
			SetVisibleTabPages();
		}
		private void SetVisibleTabPages()
		{
			tabControl.SuspendLayout();
			tabControl.TabPages.Clear();

			for (int index = 0; index < m_aTabPageList.GetLength(0); index++)
			{
				TabPage tabpage = m_aTabPageList[index];
				bool visible = true;

				if (m_IsAdvanced)
				{
					if (
						(tabpage.Equals(tabPageProperty1)) ||
						(tabpage.Equals(tabPageProperty2))
					)
					{
						visible = false;
					}
					else if (
						(tabpage.Equals(tabPageWB)) ||
						(tabpage.Equals(tabPageColor)) ||
						(tabpage.Equals(tabPageColorGamma))
					)
					{
						visible = m_StCamera.IsColor;
					}
					else if(tabpage.Equals(tabPageDefectPixelCorrection))
					{
						ushort wCount = m_StCamera.EnableDefectPixelCorrectionCount;
						settingDefectPixelPosition1.DefectPixelCount = wCount;
						visible = (0 < wCount);
					}
					else if (tabpage.Equals(tabPageEEPROM))
					{
						visible = m_StCamera.HasStoreCameraSetting;
					}
				}
				else
				{
					if (
						(tabpage.Equals(tabPageGainShutter)) ||
						(tabpage.Equals(tabPageWB)) ||
						(tabpage.Equals(tabPageY)) ||
						(tabpage.Equals(tabPageColor)) ||
						(tabpage.Equals(tabPageColorGamma)) ||
						(tabpage.Equals(tabPageOther)) ||
						(tabpage.Equals(tabPageDefectPixelCorrection)) ||
						(tabpage.Equals(tabPageEEPROM))
					)
					{
						visible = false;
					}
				}
				if (visible)
				{
					tabpage.BackColor = SystemColors.Control;
					tabControl.TabPages.Add(tabpage);
				}
			}
			tabControl.ResumeLayout();
			UpdateDisplay();
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_StCamera.ReadAutoControlSettingValue();
			UpdateDisplay();
		}

		private void btnALCWeightArea_Click(object sender, EventArgs e)
		{
			m_frmSub = new frmALCWeightArea(m_StCamera);
			m_frmSub.FormClosed += new FormClosedEventHandler(m_frmSub_FormClosed);
			m_frmSub.StartPosition = FormStartPosition.Manual;
			m_frmSub.Left = this.Left + (this.Width - m_frmSub.Width) / 2;
			m_frmSub.Top = this.Top + (this.Height - m_frmSub.Height) / 2;
			m_frmSub.Show(this);
			this.Enabled = false;
		}


		void m_frmSub_FormClosed(object sender, FormClosedEventArgs e)
		{
			m_frmSub = null;
			this.Enabled = true;
		}

		private void frmSetting_Activated(object sender, EventArgs e)
		{
			if (m_frmSub != null)
			{
				m_frmSub.Activate();
			}
		}

		private void btnColorMatrix_Click(object sender, EventArgs e)
		{
			m_frmSub = new frmColorMatrix(m_StCamera);
			m_frmSub.FormClosed += new FormClosedEventHandler(m_frmSub_FormClosed);
			m_frmSub.StartPosition = FormStartPosition.Manual;
			m_frmSub.Left = this.Left + (this.Width - m_frmSub.Width) / 2;
			m_frmSub.Top = this.Top + (this.Height - m_frmSub.Height) / 2;
			m_frmSub.Show(this);
			this.Enabled = false;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string fileName = null;
			do
			{
				if (DialogResult.Yes == MessageBox.Show("Save as initial setting for this camera type?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					fileName = CStCamera.StartUpSettingFileName();
				}
				else
				{
					using (SaveFileDialog fd = new SaveFileDialog())
					{
						fd.DefaultExt = ".cfg";
						fd.Filter = "CFG files(*.cfg)|*.cfg|All files(*.*)|*.*";
						fd.InitialDirectory = CStCamera.SettingFilePath;
						if (DialogResult.OK == fd.ShowDialog(this))
						{
							fileName = fd.FileName;
						}
						else
						{
							break;
						}
					}
				}
				m_StCamera.SaveSettingFile(fileName);
			} while (false);
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			string fileName = null;
			do
			{
				using (OpenFileDialog fd = new OpenFileDialog())
				{
					fd.DefaultExt = ".cfg";
					fd.Filter = "CFG files(*.cfg)|*.cfg|All files(*.*)|*.*";
					fd.InitialDirectory = CStCamera.SettingFilePath;
					if (DialogResult.OK == fd.ShowDialog(this))
					{
						fileName = fd.FileName;
					}
					else
					{
						break;
					}
				}

				m_StCamera.LoadSettingFile(fileName);
				UpdateDisplay();
			} while (false);
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			m_StCamera.ResetSetting();
			UpdateDisplay();
		}

		private void buttonDetectDefectPixel_Click(object sender, EventArgs e)
		{
			bool result = m_StCamera.DetectDefectPixel((ushort)numericUpDownDefectPixelThreshold.Value);
			UpdateDisplay();
		}
	}
}