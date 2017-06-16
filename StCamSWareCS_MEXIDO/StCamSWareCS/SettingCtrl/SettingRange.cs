using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS.SettingCtrl
{
	public partial class SettingRange : UserControl, ISettingRange2
	{
		public SettingRange()
		{
			InitializeComponent();
		}

		#region ISettingValue
		int m_nPos1 = 0;
		int m_nPos2 = 0;
		int m_nMax = 100;
		int m_nMin = 0;
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
				return (m_nPos1);
			}
			set
			{
				if (value <= m_nPos2)
				{
					m_nPos1 = value;
				}
				else
				{
					m_nPos1 = m_nPos2;
					m_nPos2 = value;
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
		#region ISettingRange2

		public int SettingValue2
		{
			get
			{
				return (m_nPos2);
			}
			set
			{
				if (m_nPos1 <= value)
				{
					m_nPos2 = value;
				}
				else
				{
					m_nPos2 = m_nPos1;
					m_nPos1 = value;
				}
			}
		}

		#endregion

		#region Mouse
		private Rectangle m_rectMin;
		private Rectangle m_rectMax;
		private eMouseTarget m_mouseTarget = eMouseTarget.None;

		private enum eMouseTarget
		{
			None,
			Min,
			Max,
		}
		private eMouseTarget GetTarget(Point pt)
		{
			eMouseTarget mouseTarget = eMouseTarget.None;
			Point ptClient = this.PointToClient(pt);
			if (m_rectMin.Contains(ptClient))
			{
				mouseTarget = eMouseTarget.Min;
			}
			else if (m_rectMax.Contains(ptClient))
			{
				mouseTarget = eMouseTarget.Max;
			}

			return (mouseTarget);
		}
		private void SettingRange_MouseMove(object sender, MouseEventArgs e)
		{
			Point pt = System.Windows.Forms.Cursor.Position;

			if (m_mouseTarget != eMouseTarget.None)
			{
				//Update
				Point ptClient = this.PointToClient(pt);
				int val = (ptClient.X - m_nSliderSize) * (m_nMax - m_nMin) / (ClientRectangle.Width - m_nSliderSize * 2) + m_nMin;
				if (val < m_nMin)
				{
					val = m_nMin;
				}
				else if (m_nMax < val)
				{
					val = m_nMax;
				}
				switch (m_mouseTarget)
				{
					case (eMouseTarget.Min):
						SettingValue = val;
						break;
					case (eMouseTarget.Max):
						SettingValue2 = val;
						break;
				}
				if (SettingValueChanged != null)
				{
					SettingValueChanged(this, new EventArgs());
				}
				Invalidate();
			}

			if (
				(m_mouseTarget != eMouseTarget.None) ||
				(eMouseTarget.None != GetTarget(pt))
			)
			{
				Cursor.Current = System.Windows.Forms.Cursors.Hand;
			}
			else
			{
				Cursor.Current = Cursors.Arrow;
			}
		}

		private void SettingRange_MouseUp(object sender, MouseEventArgs e)
		{
			m_mouseTarget = eMouseTarget.None;
		}

		private void SettingRange_MouseDown(object sender, MouseEventArgs e)
		{
			Point pt = System.Windows.Forms.Cursor.Position;
			m_mouseTarget = GetTarget(pt);
		}

		#endregion


		#region Draw
		private const int m_nSliderSize = 5;

		private Color m_SliderColor = Color.PaleGoldenrod;
		public Color SliderColor
		{
			get { return (m_SliderColor); }
			set { m_SliderColor = value; Invalidate(); }
		}

		private Color m_SliderEdgeColor = Color.Black;
		public Color SliderEdgeColor
		{
			get { return (m_SliderEdgeColor); }
			set { m_SliderEdgeColor = value; Invalidate(); }
		}

		private Color m_BarFrontColor = Color.LightSkyBlue;
		public Color BarFrontColor
		{
			get { return (m_BarFrontColor); }
			set { m_BarFrontColor = value; Invalidate(); }
		}

		private Color m_BarBackColor = Color.DimGray;
		public Color BarBackColor
		{
			get { return (m_BarBackColor); }
			set { m_BarBackColor = value; Invalidate(); }
		}

		private Rectangle DrawAreaRectangle
		{
			get
			{
				const int marginV = 1;
				Rectangle rect = new Rectangle(ClientRectangle.X + m_nSliderSize, ClientRectangle.Y + marginV, ClientRectangle.Width - m_nSliderSize * 2, ClientRectangle.Height - marginV * 2);
				return (rect);
			}
		}
		private void SettingRange_Paint(object sender, PaintEventArgs e)
		{
			Graphics grfx = e.Graphics;

			Rectangle rect = DrawAreaRectangle;
			int stratBarY = rect.Height / 4;
			int endBarY = stratBarY * 3;
			int barHeight = rect.Height / 2;

			int startBarX = rect.Width * (m_nPos1 - m_nMin) / (m_nMax - m_nMin) + rect.X;
			int endBarX = rect.Width * (m_nPos2 - m_nMin) / (m_nMax - m_nMin) + rect.X;
			const int edgeSize = 1;

			//Bar
			using (Brush brushB = new SolidBrush(m_BarBackColor))
			using (Brush brushL = new SolidBrush(m_BarFrontColor))
			{
				grfx.FillRectangle(brushB, rect.X, stratBarY + rect.Y, rect.Width, barHeight);
				grfx.FillRectangle(brushL, startBarX, stratBarY + edgeSize + rect.Y, endBarX - startBarX, barHeight - edgeSize * 2);
			}

			//Slider
			if (Enabled)
			{
				using (Brush brush = new SolidBrush(m_SliderColor))
				using (Pen pen = new Pen(m_SliderEdgeColor, 1))
				{
					//Slider1
					Point[] ptMin = new Point[]{
						new Point(startBarX, barHeight + rect.Y),
						new Point(startBarX - m_nSliderSize, rect.Y),
						new Point(startBarX + m_nSliderSize, rect.Y)};
					grfx.FillPolygon(brush, ptMin);
					grfx.DrawPolygon(pen, ptMin);
					m_rectMin = new Rectangle(ptMin[1].X, ptMin[1].Y, m_nSliderSize * 2, barHeight);

					//Slider2
					Point[] ptMax = new Point[]{
						new Point(endBarX, barHeight + rect.Y),
						new Point(endBarX - m_nSliderSize, rect.Height + rect.Y),
						new Point(endBarX + m_nSliderSize, rect.Height + rect.Y)};
					grfx.FillPolygon(brush, ptMax);
					grfx.DrawPolygon(pen, ptMax);
					m_rectMax = new Rectangle(ptMax[1].X, ptMax[0].Y, m_nSliderSize * 2, barHeight);
				}
			}
		}
		#endregion

	}
}
