using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingDefectPixelPosition : UserControl
	{
		public SettingDefectPixelPosition()
		{
			InitializeComponent();
			InitializePositionCtrl();
		}
		protected int m_nDefectPixelCount = 64;
		protected SettingPixelPosition[] m_aPixelPosition = null;

		public int DefectPixelCount
		{
			get { return (m_nDefectPixelCount); }
			set
			{
				m_nDefectPixelCount = value;
				InitializePositionCtrl();
			}
		}
		protected void InitializePositionCtrl()
		{
			DisposePositionCtrl();

			this.SuspendLayout();
			m_aPixelPosition = new SettingPixelPosition[m_nDefectPixelCount];
			for (int i = 0; i < m_aPixelPosition.Length; i++)
			{
				SettingPixelPosition pp = new SettingPixelPosition();
				pp.Parent = this;
				pp.Location = new System.Drawing.Point(4, i * pp.Height);
				pp.SetCtrlIDX(SettingIDs.DEFECT_PIXEL_POS_X_00 + i * 2, "Defect pixel[" + i.ToString() + "] X");
				pp.SetCtrlIDY(SettingIDs.DEFECT_PIXEL_POS_Y_00 + i * 2, "Defect pixel[" + i.ToString() + "] Y");
				this.Controls.Add(pp);
				m_aPixelPosition[i] = pp;
			}
			this.ResumeLayout();
		}
		protected void DisposePositionCtrl()
		{
			if (m_aPixelPosition != null)
			{
				this.SuspendLayout();
				for (int i = 0; i < m_aPixelPosition.Length; i++)
				{
					if (m_aPixelPosition[i] != null)
					{
						m_aPixelPosition[i].Dispose();
						m_aPixelPosition[i] = null;
					}
				}
				m_aPixelPosition = null;
				this.ResumeLayout();
			}
		}
	}
}
