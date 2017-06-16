using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SensorTechnology;

namespace StCamSWareCS
{
	public partial class frmALCWeightArea : frmSetBase
	{
		public frmALCWeightArea(CStCamera stCamera) : base(stCamera)
		{
			InitializeComponent();

			m_PreviewGDIEvent = new CStCamera.PreviewGDIEvent(m_StCamera_OnPreviewGDI);
		}

		private CStCamera.PreviewGDIEvent m_PreviewGDIEvent = null;
		private byte[] m_pbyteALCWeight = null;
		private ushort[] m_pwControlAreaX = null;
		private ushort[] m_pwControlAreaY = null;
		
		
		private void frmALCWeightArea_Load(object sender, EventArgs e)
		{
			m_StCamera.OnPreviewGDI += m_PreviewGDIEvent;
			SetSettingValueChangeEvent(this.Controls);
			InitComboBox(this.Controls);
			UpdateDisplay();
		}

		protected override void UpdateDisplay()
		{
			UpdateSettingCtrls(this.Controls);
			m_pbyteALCWeight = m_StCamera.ALCWeight;
			m_pwControlAreaX = m_StCamera.ControlAreaX;
			m_pwControlAreaY = m_StCamera.ControlAreaY;
		}
		private void frmALCWeightArea_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_StCamera.OnPreviewGDI -= m_PreviewGDIEvent;

		}
		private void m_StCamera_OnPreviewGDI(System.IntPtr hDC, uint dwWidth, uint dwHeight, uint dwFrameNo)
		{
			if (
				(m_pbyteALCWeight == null) ||
				(m_pwControlAreaX == null) ||
				(m_pwControlAreaY == null)
			)
			{
				return;
			}
			else if(
					(m_pbyteALCWeight.GetLength(0) < 16) ||
					(m_pwControlAreaX.GetLength(0) < 3) ||
					(m_pwControlAreaY.GetLength(0) < 3)

				)
			{
				return;
			}



			Font font = this.Font;
			using(Graphics grfx = Graphics.FromHdc(hDC))
			using(Pen pen = new Pen(Color.Red, 2))
			using(SolidBrush brushW = new SolidBrush(Color.White))
			using(SolidBrush brushB = new SolidBrush(Color.Black))
			{
				int[] startX = new int[] { 0, (int)(dwWidth * m_pwControlAreaX[0] / 10000), (int)(dwWidth * m_pwControlAreaX[1] / 10000), (int)(dwWidth * m_pwControlAreaX[2] / 10000) };
				int[] startY = new int[] { 0, (int)(dwHeight * m_pwControlAreaY[0] / 10000), (int)(dwHeight * m_pwControlAreaY[1] / 10000), (int)(dwHeight * m_pwControlAreaY[2] / 10000) }; 

				//Weight
				for (int y = 0; y < 4; y++)
				{
					for (int x = 0; x < 4; x++)
					{
						string str = m_pbyteALCWeight[y * 4 + x].ToString();
						SizeF sizefText = grfx.MeasureString(str, font);
						grfx.FillRectangle(brushW, startX[x], startY[y], sizefText.Width, sizefText.Height);
						grfx.DrawString(str, font, brushB, startX[x], startY[y]);

					}
				}

				//Area
				int nMaxY = 0;
				int nMaxX = 0;
				for(int i = 1; i < 4; i++)
				{
					if (nMaxY < startY[i])
					{
						grfx.DrawLine(pen, 0, startY[i], dwWidth, startY[i]);
						nMaxY = startY[i];
					}

					if (nMaxX < startX[i])
					{
						grfx.DrawLine(pen, startX[i], 0, startX[i], dwHeight);
						nMaxX = startX[i];
					}
				}

			}
			
		}



		private void btnOK_Click(object sender, EventArgs e)
		{
			Close();

		}
	}
}