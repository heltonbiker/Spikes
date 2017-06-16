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
	public partial class frmMain : Form
	{
		public frmMain(CStCamera stCamera)
		{
			InitializeComponent();
			m_StCamera = stCamera;
			eventActivated = new System.EventHandler(this.frmMain_Activated);
			Activated += eventActivated;
		}

		private CStCamera m_StCamera = null;
		private frmSetting m_frmSetting = null;
		private frmThumbnail m_frmThumbnail = null;
		private System.EventHandler eventActivated = null;

		private bool m_IsTransferRunning = false;
		private bool m_IsVideoCaptuerRunning = false;
		private bool[] m_aCallbackFunctions = new bool[] { false, false, false, false, false };

		private bool m_IsNeedReOpen = false;


		private void frmMain_Activated(object sender, System.EventArgs e)
		{
			Activated -= eventActivated;
			bool result = true;
			mUpdateTitleBar();

			m_StCamera.OnRawCallback += new CStCamera.RawEvent(m_StCamera_OnRawCallback);
			m_StCamera.OnPreviewBitmap += new CStCamera.PreviewBitmapEvent(m_StCamera_OnPreviewBitmap);
			m_StCamera.OnPreviewGDI += new CStCamera.PreviewGDIEvent(m_StCamera_OnPreviewGDI);
			m_StCamera.OnError += new CStCamera.ErrorEvent(m_StCamera_OnError);
			m_StCamera.OnImageSizeChanged += new EventHandler(m_StCamera_OnImageSizeChanged);
			m_StCamera.OnSnapShot += new CStCamera.SnapShotEvent(m_StCamera_OnSnapShot);

			result = m_StCamera.SetRcvMsgWnd(this.Handle);
			result = m_StCamera.CreateWindow(panelImage.Handle);
			m_StCamera.PreviewWindowWidth = (uint)panelImage.Width;
			m_StCamera.PreviewWindowHeight = (uint)panelImage.Height;
			adjustWindowSizeToolStripMenuItem.PerformClick();


			System.Drawing.Rectangle rectDesktop = Screen.GetWorkingArea(this);
			m_frmThumbnail.StartPosition = FormStartPosition.Manual;
			m_frmThumbnail.Left = Width + Left;
			m_frmThumbnail.Top = 0;
			m_frmThumbnail.Width = (100 < rectDesktop.Width - this.Width) ? rectDesktop.Width - this.Width : 100;
			m_frmThumbnail.Height = rectDesktop.Height;
			
			m_StCamera.StartTransfer();

		}


		void m_StCamera_OnError(object sender, uint errorNo)
		{
			if (Native.ERROR_ACCESS_DENIED == errorNo)
			{
				m_IsNeedReOpen = true;
			}
			Native.ShowErrorMsg(errorNo);

		}
		private void startstopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool result = true;
			if (m_IsTransferRunning)
			{
				m_StCamera.StopTransfer();
			}
			else
			{
				do
				{
					if (m_IsNeedReOpen)
					{

						result = m_StCamera.Open();
						if (!result) break;

						result = m_StCamera.SetRcvMsgWnd(this.Handle);
						if (!result) break;

						result = m_StCamera.CreateWindow(panelImage.Handle);
						if (!result) break;

						m_StCamera.PreviewWindowWidth = (uint)panelImage.Width;
						m_StCamera.PreviewWindowHeight = (uint)panelImage.Height;
						adjustWindowSizeToolStripMenuItem.PerformClick();
						m_IsNeedReOpen = false;
					}

					result = m_StCamera.StartTransfer();
					if (!result) break;

				} while (false);
			}
		}


		private void menuItemSetting_Click(object sender, System.EventArgs e)
		{
			if (m_frmSetting == null)
			{
				m_frmSetting = new frmSetting(m_StCamera);
				m_frmSetting.Closed += new System.EventHandler(frmSetting_Closed);
			}
			m_frmSetting.Show();
			m_frmSetting.Activate();
		}
		private void frmSetting_Closed(object sender, System.EventArgs e)
		{
			m_frmSetting = null;


		}
		private void m_StCamera_OnImageSizeChanged(object sender, System.EventArgs e)
		{
			mUpdatePreviewWindowSize();
		}
		private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			statusStrip.Visible = !statusStrip.Visible;
			statusbarToolStripMenuItem.Checked = statusStrip.Visible;
		}

		private void panelImage_Resize(object sender, EventArgs e)
		{
			mUpdatePreviewWindowSize();
		}
		private void mUpdatePreviewWindowSize()
		{

			if (m_StCamera != null)
			{
				m_StCamera.PreviewWindowWidth = (uint)panelImage.Width;
				m_StCamera.PreviewWindowHeight = (uint)panelImage.Height;

				m_StCamera.GetPreviewDestSize();
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void TransferStatusChanged(bool isRunning, uint errorNo)
		{
			m_IsTransferRunning = isRunning;
			if (errorNo != 0)
			{
				m_StCamera_OnError(this, errorNo);
			}
		}

		private void VideoCaptuerStatusChanged(bool isRunning, uint errorNo)
		{
			m_IsVideoCaptuerRunning = isRunning;
			if (!isRunning)
			{
				MessageBox.Show("AVI file created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			videoCaptureToolStripMenuItem.Enabled = !isRunning;
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case (StCam.WM_STCAM_TRANSFER_START):
					TransferStatusChanged(true, (uint)m.LParam);
					break;

				case (StCam.WM_STCAM_TRANSFER_FINISH):
					TransferStatusChanged(false, (uint)m.LParam);
					break;

				case (StCam.WM_STCAM_AVI_FILE_START):
					VideoCaptuerStatusChanged(true, (uint)m.LParam);
					break;

				case (StCam.WM_STCAM_AVI_FILE_FINISH):
					VideoCaptuerStatusChanged(false, (uint)m.LParam);
					break;

				default:
					base.WndProc(ref m);
					break;
			}
		}

		private void optionToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			startstopToolStripMenuItem.Text = m_IsTransferRunning?"&Freeze":"&Live video";
		}


		private void renameCameraToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (frmRenameCamera frm = new frmRenameCamera())
			{
				frm.CameraName = m_StCamera.CameraName;
				if (DialogResult.OK == frm.ShowDialog())
				{
					m_StCamera.CameraName = frm.CameraName;
					mUpdateTitleBar();
				}
			}
		}
		private void mUpdateTitleBar()
		{
			string strTitle = Application.ProductName;

			switch (IntPtr.Size)
			{
				case (4): strTitle += "(x86)"; break;
				case (8): strTitle += "(x64)"; break;
				default: strTitle += "(?)"; break;
			}

			strTitle += " [" + m_StCamera.CameraName + "] v" + Application.ProductVersion;
			Text = strTitle;
		}
		

		private void versionInformationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (frmVersionInfo frm = new frmVersionInfo())
			{
				frm.ShowDialog();
			}
		}

		private void adjustWindowSizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;

			int nOffsetX = 0;
			int nOffsetY = 0;

			int nAddWidth = 0;
			int nAddHeight = 0;

			if (menuStrip.Visible)
			{
				nOffsetY = menuStrip.Height;
			}

			if (statusStrip.Visible)
			{
				nAddHeight = statusStrip.Height;
			}

			m_StCamera.AdjustWindowSize(this, false, nOffsetX, nOffsetY, nAddWidth, nAddHeight);
		}

		private void previewSizeMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripDropDownItem tsddi = sender as ToolStripDropDownItem;
			int nShiftCount = int.Parse(tsddi.Tag.ToString());
			m_StCamera.PreviewSizeShiftCount = nShiftCount;
			adjustWindowSizeToolStripMenuItem.PerformClick();

		}

		private void frmMain_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void panelImage_MouseMove(object sender, MouseEventArgs e)
		{
			Point clientPt = panelImage.PointToClient(System.Windows.Forms.Cursor.Position);
			Point imagePt = m_StCamera.GetImagePos(clientPt);
			
			System.Drawing.Color color = m_StCamera.GetImageColor(imagePt);
			toolStripStatusLabelColor.Text = "(" + imagePt.X.ToString() + "," + imagePt.Y.ToString() + ")  R=" + color.R.ToString() + ", G=" + color.G.ToString() + ", B=" + color.B.ToString();
		}

		private void sizeToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			bool[] aCheck = new bool[]{false, false, false, false, false, false, false};
			int nIndex = 3 - m_StCamera.PreviewSizeShiftCount;

			if ((0 <= nIndex) && (nIndex < 7))
			{
				aCheck[nIndex] = true;
			}
			toolStripMenuItemx8.Checked = aCheck[0];
			toolStripMenuItemx4.Checked = aCheck[1];
			toolStripMenuItemx2.Checked = aCheck[2];
			toolStripMenuItemx1.Checked = aCheck[3];
			toolStripMenuItemxinv2.Checked = aCheck[4];
			toolStripMenuItemxinv4.Checked = aCheck[5];
			toolStripMenuItemxinv8.Checked = aCheck[6];
		}
		private void m_StCamera_OnRawCallback(System.IntPtr pbyteBuffer, uint dwBufferSize, uint dwWidth, uint dwHeight, uint dwFrameNo, ushort wColorArray, uint dwTransferBitsPerPixel)
		{

		}

		private void m_StCamera_OnPreviewBitmap(System.IntPtr pbyteBitmap, uint dwBufferSize, uint dwWidth, uint dwHeight, uint dwFrameNo, uint dwPreviewPixelFormat)
		{

		}
		private void m_StCamera_OnPreviewGDI(System.IntPtr hDC, uint dwWidth, uint dwHeight, uint dwFrameNo)
		{
			Font font = this.Font;
			using(Graphics grfx = Graphics.FromHdc(hDC))
			using(Pen pen = new Pen(Color.Black, 1))
			using(SolidBrush brushW = new SolidBrush(Color.White))
			using(SolidBrush brushB = new SolidBrush(Color.Black))
			{
				bool[] isEnabled = m_aCallbackFunctions;
				int nWidth = (int)dwWidth;
				int nHeight = (int)dwHeight;

				//Grid
				if (isEnabled[0])
				{
					grfx.DrawLine(pen, nWidth / 4, 0, nWidth / 4, nHeight);
					grfx.DrawLine(pen, nWidth / 2, 0, nWidth / 2, nHeight);
					grfx.DrawLine(pen, nWidth * 3 / 4, 0, nWidth * 3 / 4, nHeight);

					grfx.DrawLine(pen, 0, nHeight / 3, nWidth, nHeight / 3);
					grfx.DrawLine(pen, 0, nHeight * 2 / 3, nWidth, nHeight * 2 / 3);
				}

				//Ellipse
				if (isEnabled[1])
				{
					int nRadiusStepX = 100;
					int nRadiusStepY = 100;

					int nCenterX = nWidth / 2;
					int nCenterY = nHeight / 2;

					int nRadiusX = nRadiusStepX;
					int nRadiusY = nRadiusStepY;

					do
					{
						Rectangle rect = new Rectangle(nCenterX - nRadiusX, nCenterY - nRadiusY, nRadiusX * 2, nRadiusY * 2);
						if ((rect.Left < 0) && (rect.Top < 0) && (nWidth < rect.Right) && (nHeight < rect.Bottom))
						{
							break;
						}

						grfx.DrawEllipse(pen, rect);

						nRadiusX += nRadiusStepX;
						nRadiusY += nRadiusStepY;
					} while (true);
				}

				//FPS, Time
				if (isEnabled[2])
				{
					DateTime dt = DateTime.Now;
					double fps = m_StCamera.FPS;
					string str = "FPS:" + fps.ToString("0.0") + " / Frame Count:" + m_StCamera.RcvFrameCount.ToString() + "[" + m_StCamera.LastRcvFrameNo.ToString() + "]";
					SizeF sizefText = grfx.MeasureString(str, font);
					grfx.FillRectangle(brushW, nWidth - sizefText.Width, 0, nWidth - 1, sizefText.Height);
					grfx.DrawString(str, font, brushB, nWidth - sizefText.Width, 1);

					str = dt.ToString("G");
					sizefText = grfx.MeasureString(str, font);
					grfx.FillRectangle(brushW, nWidth - sizefText.Width, nHeight - sizefText.Height, sizefText.Width, sizefText.Height);
					grfx.DrawString(str, font, brushB, nWidth - sizefText.Width, nHeight - sizefText.Height + 1);
				}

				//Logo
				if (isEnabled[3])
				{
					Bitmap bmp = Properties.Resources.Logo;
					grfx.DrawImage(bmp, 0, 0);
					bmp.Dispose();
				}
				
				//Received, Dropped
				if (isEnabled[4])
				{
					string str = "Received:" + m_StCamera.RcvFrameCount.ToString() + " / Dropped:" + m_StCamera.DropCount.ToString();
					SizeF sizefText = grfx.MeasureString(str, font);
					grfx.FillRectangle(brushW, 0, nHeight - sizefText.Height, sizefText.Width, sizefText.Height);
					grfx.DrawString(str, font, brushB, 0, nHeight - sizefText.Height + 1);
				}
			}
			
		}

		private void callbackToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (frmCallback form = new frmCallback(m_aCallbackFunctions))
			{
				form.ShowDialog();
			}
		}

		private void snapShotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool result = m_StCamera.SnapShot();
		}

		void m_StCamera_OnSnapShot(object sender, SnapShotEventArgs e)
		{
			m_frmThumbnail.AddStillImage(new CStillImage(e.Bitmap, e.FrameNo));
			m_frmThumbnail.Show();
		}
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_frmThumbnail != null)
			{
				if (!m_frmThumbnail.AreAllImagesSaved)
				{
					if (DialogResult.Cancel == MessageBox.Show("There are images that are not saved. If you quit this application, these images are removed.", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
					{
						e.Cancel = true;
						return;
					}
				}

				m_frmThumbnail.CloseEnabled = true;
				m_frmThumbnail.Close();
			}

			if (m_StCamera != null)
			{
				m_StCamera.Dispose();
				m_StCamera = null;
			}
		}

		private void videoCaptureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName = null;
			do
			{
				using (SaveFileDialog fd = new SaveFileDialog())
				{
					fd.DefaultExt = ".avi";
					fd.Filter = "AVI files(*.avi)|*.avi|All files(*.*)|*.*";
					fd.InitialDirectory = CStCamera.VideoFilePath;
					if (DialogResult.OK == fd.ShowDialog(this))
					{
						fileName = fd.FileName;
					}
					else
					{
						break;
					}
				}

				using (frmAVISetting form = new frmAVISetting(m_StCamera))
				{
					if (DialogResult.OK != form.ShowDialog())
					{
						break;
					}
					System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
					CStCamera.VideoFilePath = fi.DirectoryName;
				}

				m_StCamera.SaveAVI(fileName);

			} while (false);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			StartPosition = FormStartPosition.Manual;
			Left = 0;
			Top = 0;

			m_frmThumbnail = new frmThumbnail(m_StCamera);
			m_frmThumbnail.FormClosed += new FormClosedEventHandler(m_frmThumbnail_FormClosed);
		}

		void m_frmThumbnail_FormClosed(object sender, FormClosedEventArgs e)
		{
			m_frmThumbnail = null;
		}


		private void showThumbnailToolStripMenuItem_Click(object sender, EventArgs e)
		{
			m_frmThumbnail.Show();
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			if ((!e.Alt) && (e.KeyCode == Keys.S))
			{
				m_StCamera.SnapShot();
			}
		}

		
	




	}
}