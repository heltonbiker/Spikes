using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SensorTechnology;

namespace StCamSWareCS
{
	public partial class ThumbnailCtrl : UserControl
	{
		public ThumbnailCtrl()
		{
			InitializeComponent();
			m_listStillImage = new List<CStillImage>();
			this.DoubleBuffered = true;
		}

		private int m_nPreviewSize = -3;
		List<CStillImage> m_listStillImage = null;
		const int m_nMarginTop = 5;
		const int m_nMarginBottom = 5;
		const int m_nMarginLeft = 5;
		const int m_nMarginRight = 5;
		private bool m_bShowNo = true;
		public bool AreAllImagesSaved
		{
			get
			{
				bool reval = true;
				foreach (CStillImage si in m_listStillImage)
				{
					if (!si.IsSaved)
					{
						reval = false;
						break;
					}
				}

				return (reval);
			}
		}
		public void AddStillImage(CStillImage stillImage)
		{
			m_listStillImage.Add(stillImage);
			UpdatePanelSize();
			Invalidate();

		}

		private void ThumbnailCtrl_SizeChanged(object sender, EventArgs e)
		{

		}

		public void UpdateThubnailCtrlSize()
		{
			UpdatePanelSize();
			Invalidate();


		}
		private void ThumbnailCtrl_Scroll(object sender, ScrollEventArgs e)
		{
			Invalidate();

		}

		private void UpdatePanelSize()
		{
			Size sizeFrame;
			int nColCount = 0;
			int nRowCount = 0;

			GetFrameSizeAndRowColCount(out sizeFrame, out nColCount, out nRowCount);

			Size sizePanel = new Size(sizeFrame.Width * nColCount, sizeFrame.Height * nRowCount);
			if ((Size.Width != sizePanel.Width) || (Size.Height != sizePanel.Height))
			{
				//this.Size = sizePanel;
				AutoScrollMinSize = sizePanel;
			}

		}
		private Size GetMaxImageSize()
		{
			int nWidth = 0;
			int nHeight = 0;

			foreach (CStillImage stillImage in m_listStillImage)
			{
				if (nWidth < stillImage.Width)
				{
					nWidth = stillImage.Width;
				}
				if (nHeight < stillImage.Height)
				{
					nHeight = stillImage.Height;
				}
			}
			return (new Size(nWidth, nHeight));
		}
		private void GetFrameSizeAndRowColCount(out Size sizeFrame, out int nColCount, out int nRowCount)
		{
			Size sizeMax = GetMaxImageSize();
			if (0 < m_nPreviewSize)
			{
				sizeMax.Width <<= m_nPreviewSize;
				sizeMax.Height <<= m_nPreviewSize;
			}
			else if (m_nPreviewSize < 0)
			{
				sizeMax.Width >>= -m_nPreviewSize;
				sizeMax.Height >>= -m_nPreviewSize;
			}

			sizeFrame = new Size(sizeMax.Width + m_nMarginLeft + m_nMarginRight, sizeMax.Height + m_nMarginTop + m_nMarginBottom);

			int nClientWidth = Width;

			if (Parent != null)
			{
				nClientWidth = ClientSize.Width;

				if (this.VScroll)
				{
					
					nClientWidth -= SystemInformation.VerticalScrollBarWidth;
				}
			}
			nColCount = nClientWidth / sizeFrame.Width;
			if (nColCount < 1)
			{
				nColCount = 1;
			}
			nRowCount = (m_listStillImage.Count + nColCount - 1) / nColCount;
			if (nRowCount < 1)
			{
				nRowCount = 1;
			}

			return;
		}

		private void showToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (CStillImage st in m_listStillImage)
			{
				if (st.IsSelected)
				{
					st.Show();
				}
			}
		}

		private void snapShotNoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			m_bShowNo = !m_bShowNo;
			Invalidate();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (CStillImage st in m_listStillImage)
			{
				if (st.IsSelected)
				{
					st.SaveImage();
				}
			}
			Invalidate();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int index = 0;
			while (index < m_listStillImage.Count)
			{
				CStillImage st = m_listStillImage[index];
				if (st.IsSelected)
				{
					st.Dispose();
					m_listStillImage.RemoveAt(index);
				}
				else
				{
					index++;
				}
			}
			UpdatePanelSize();
			Invalidate();
		}

		private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear();
		}
		public void Clear()
		{
			int index = 0;
			while (0 < m_listStillImage.Count)
			{
				CStillImage st = m_listStillImage[index];
				st.Dispose();
				m_listStillImage.RemoveAt(index);
			}
			UpdatePanelSize();
			Invalidate();

		}

		private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
			int nSize = int.Parse(tsmi.Tag.ToString());
			m_nPreviewSize = nSize;
			UpdatePanelSize();
			Invalidate();
		}

		private void ThumbnailCtrl_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{

				Size sizeFrame;
				int nColCount = 0;
				int nRowCount = 0;

				GetFrameSizeAndRowColCount(out sizeFrame, out nColCount, out nRowCount);

				int row = (e.Y - this.AutoScrollPosition.Y) / sizeFrame.Height;
				int col = (e.X - this.AutoScrollPosition.X) /sizeFrame.Width;
				int index = nColCount * row + col;

				if (index < m_listStillImage.Count)
				{
					m_listStillImage[index].IsSelected = !m_listStillImage[index].IsSelected;
					Invalidate();
				}
			}
		}

		private void ThumbnailCtrl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{

				Size sizeFrame;
				int nColCount = 0;
				int nRowCount = 0;

				GetFrameSizeAndRowColCount(out sizeFrame, out nColCount, out nRowCount);

				int row = (e.Y - this.AutoScrollPosition.Y) / sizeFrame.Height;
				int col = (e.X - this.AutoScrollPosition.X) / sizeFrame.Width;
				int index = nColCount * row + col;

				if (index < m_listStillImage.Count)
				{
					m_listStillImage[index].Show();
				}
			}
		}

		private void ThumbnailCtrl_Paint(object sender, PaintEventArgs e)
		{
			Size sizeFrame;
			int nColCount = 0;
			int nRowCount = 0;

			GetFrameSizeAndRowColCount(out sizeFrame, out nColCount, out nRowCount);

			Graphics grfx = e.Graphics;
			Point pointScroll = AutoScrollPosition;

			int rowStart = Math.Abs(pointScroll.Y) / sizeFrame.Height;
			int rowEnd = (Math.Abs(pointScroll.Y) + ClientSize.Height + sizeFrame.Height - 1) / sizeFrame.Height;
			if (nRowCount < rowEnd)
			{
				rowEnd = nRowCount;
			}

			int colStart = Math.Abs(pointScroll.X) / sizeFrame.Width;
			int colEnd = (Math.Abs(pointScroll.X) + ClientSize.Width + sizeFrame.Width - 1) / sizeFrame.Width;
			if (nColCount < colEnd)
			{
				colEnd = nColCount;
			}

			System.Diagnostics.Debug.WriteLine("Paint:Thumbnail");

			using (Brush brushB = new SolidBrush(Color.Blue))
			using (Brush brushR = new SolidBrush(Color.Red))
			using (Pen penB = new Pen(Color.Blue, 2))
			{
				for (int row = rowStart; row < rowEnd; row++)
				{
					int nTop = row * sizeFrame.Height + pointScroll.Y;
					for (int col = 0; col < nColCount; col++)
					{
						int nLeft = col * sizeFrame.Width + pointScroll.X;
						int index = row * nColCount + col;

						if (m_listStillImage.Count <= index)
						{
							break;
						}

						System.Diagnostics.Debug.WriteLine("Paint:" + index.ToString());

						int nDispWidth = m_listStillImage[index].Bitmap.Width;
						int nDispHeight = m_listStillImage[index].Bitmap.Height;
						if (0 < m_nPreviewSize)
						{
							nDispWidth <<= m_nPreviewSize;
							nDispHeight <<= m_nPreviewSize;
						}
						else if (m_nPreviewSize < 0)
						{
							nDispWidth >>= -m_nPreviewSize;
							nDispHeight >>= -m_nPreviewSize;
						}
						grfx.DrawImage(m_listStillImage[index].Bitmap, nLeft + m_nMarginLeft, nTop + m_nMarginTop, nDispWidth, nDispHeight);

						if (m_bShowNo)
						{
							String strText = m_listStillImage[index].Counter.ToString();
							SizeF sizeText = grfx.MeasureString(strText, Font);
							Brush brushText = brushR;
							if (m_listStillImage[index].IsSaved)
							{
								brushText = brushB;
							}
							grfx.DrawString(strText, Font, brushText, new Point(nLeft + nDispWidth + m_nMarginLeft - (int)sizeText.Width, nTop + nDispHeight + m_nMarginTop - (int)sizeText.Height));
						}
						if (m_listStillImage[index].IsSelected)
						{
							Rectangle rect = new Rectangle(nLeft + m_nMarginLeft - (int)(penB.Width / 2), nTop + m_nMarginTop - (int)(penB.Width / 2), nDispWidth + (int)penB.Width, nDispHeight + (int)penB.Width);
							grfx.DrawRectangle(penB, rect);
						}
					}
				}
			}
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			bool isImageExist = (0 < m_listStillImage.Count);
			bool isSelected = false;
			if(isImageExist)
			{
				foreach(CStillImage si in m_listStillImage)
				{
					if(si.IsSelected)
					{
						isSelected = true;
						break;
					}
				}
			}
			showToolStripMenuItem.Enabled = isSelected;
			saveToolStripMenuItem.Enabled = isSelected;
			deleteToolStripMenuItem.Enabled = isSelected;

			deleteAllToolStripMenuItem.Enabled = isImageExist;
			
			bool[] aCheck = new bool[]{false, false, false, false, false, false, false};
			int nIndex = 3 - m_nPreviewSize;

			if ((0 <= nIndex) && (nIndex < 7))
			{
				aCheck[nIndex] = true;
			}
			x8ToolStripMenuItem.Checked = aCheck[0];
			x4ToolStripMenuItem.Checked = aCheck[1];
			x2ToolStripMenuItem.Checked = aCheck[2];
			x1ToolStripMenuItem.Checked = aCheck[3];
			x12ToolStripMenuItem.Checked = aCheck[4];
			x14ToolStripMenuItem.Checked = aCheck[5];
			x18ToolStripMenuItem.Checked = aCheck[6];
		}
		



	}
}
