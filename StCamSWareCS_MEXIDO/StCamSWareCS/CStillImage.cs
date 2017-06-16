using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SensorTechnology;
namespace StCamSWareCS
{
	public class CStillImage : IDisposable	
	{
		public CStillImage(Bitmap bitmap, uint frameNo)
		{
			m_Bitmap = bitmap;
			m_dwFrameNo = frameNo;
			m_dwCounter = ++SnapShotCount;
		}
		public void Dispose()
		{
			if (m_frmSnapShot != null)
			{
				m_frmSnapShot.Close();
				m_frmSnapShot = null;
			}

			if (m_Bitmap != null)
			{
				m_Bitmap.Dispose();
				m_Bitmap = null;
			}
		}

		private Bitmap m_Bitmap = null;
		private uint m_dwFrameNo = 0;
		private uint m_dwCounter = 0;
		private frmSnapShot m_frmSnapShot = null;
		private bool m_IsSelected = false;
		private bool m_IsSaved = false;
		private string m_FileName = null;

		public Bitmap Bitmap
		{
			get { return (m_Bitmap); }
		}
		public uint FrameNo
		{
			get { return (m_dwFrameNo); }
		}
		public uint Counter
		{
			get { return (m_dwCounter); }
		}

		public int Width
		{
			get
			{
				int val = 0;
				if (m_Bitmap != null)
				{
					val = m_Bitmap.Width;
				}
				return (val);
			}
		}
		public int Height
		{
			get
			{
				int val = 0;
				if (m_Bitmap != null)
				{
					val = m_Bitmap.Height;
				}
				return (val);
			}
		}
		public void Show()
		{
			if(m_frmSnapShot == null)
			{
				m_frmSnapShot = new frmSnapShot(m_Bitmap, m_dwCounter);
				m_frmSnapShot.FormClosed += new System.Windows.Forms.FormClosedEventHandler(m_frmSnapShot_FormClosed);
				m_frmSnapShot.SaveImage += new EventHandler(m_frmSnapShot_SaveImage);
			}
			m_frmSnapShot.Show();
			m_frmSnapShot.Activate();
		}
		void  m_frmSnapShot_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			m_frmSnapShot = null;
		}
		void m_frmSnapShot_SaveImage(object sender, EventArgs e)
		{
			SaveImage();
		}
		public void SaveImage()
		{
			if (m_Bitmap != null)
			{
				using (SaveFileDialog fd = new SaveFileDialog())
				{
					fd.DefaultExt = "bmp";
					fd.Filter = "BMP files(*.bmp)|*.bmp|Jpeg files(*.jpg)|*.jpg|Tiff files(*.tif)|*.tif|PNG files(*.png)|*.png|All files(*.*)|*.*";
					fd.InitialDirectory = CStCamera.StillImageFilePath;

					if (DialogResult.OK == fd.ShowDialog())
					{
						System.IO.FileInfo fi = new System.IO.FileInfo(fd.FileName);
						string strExt = fi.Extension.ToLower();
						System.Drawing.Imaging.ImageFormat imageFormat = System.Drawing.Imaging.ImageFormat.Png;
						if (strExt.CompareTo(".bmp") == 0)
						{
							imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
						}
						else if (strExt.CompareTo(".jpg") == 0)
						{
							imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
						}
						else if (strExt.CompareTo(".tif") == 0)
						{
							imageFormat = System.Drawing.Imaging.ImageFormat.Tiff;
						}

						m_Bitmap.Save(fd.FileName, imageFormat);
						m_FileName = fd.FileName;
						m_IsSaved = true;
						CStCamera.StillImageFilePath = fi.DirectoryName;
						
					}
				}
			}


		}

		public bool IsSelected
		{
			get{return(m_IsSelected);}
			set{m_IsSelected = value;}
		}
		public bool IsSaved
		{
			get { return (m_IsSaved); }
			set { m_IsSaved = value; }
		}
		private static uint SnapShotCount = 0;
	}

}
