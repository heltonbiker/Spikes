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
	public partial class frmThumbnail : Form
	{
		public frmThumbnail(CStCamera stCamera)
		{
			InitializeComponent();
			m_StCamera = stCamera;
		}

		private CStCamera m_StCamera = null;
		private bool m_nCloseEnabled = false;
		public bool CloseEnabled
		{
			get { return (m_nCloseEnabled); }
			set { m_nCloseEnabled = value; }
		}


		public bool AreAllImagesSaved
		{
			get
			{
				return (thumbnailCtrl.AreAllImagesSaved);
			}
		}
		public void AddStillImage(CStillImage stillImage)
		{
			thumbnailCtrl.AddStillImage(stillImage);

		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			if ((e.CloseReason == CloseReason.UserClosing) && (!m_nCloseEnabled))
			{
				this.Hide();
				e.Cancel = true;
			}
			else
			{
				thumbnailCtrl.Clear();
			}
		}

		private void frmThumbnail_SizeChanged(object sender, EventArgs e)
		{
			thumbnailCtrl.UpdateThubnailCtrlSize();
		}

		private void frmThumbnail_KeyDown(object sender, KeyEventArgs e)
		{

			if ((!e.Alt) && (e.KeyCode == Keys.S))
			{
				m_StCamera.SnapShot();
			}
		}

		private void thumbnailCtrl_KeyDown(object sender, KeyEventArgs e)
		{
			if ((!e.Alt) && (e.KeyCode == Keys.S))
			{
				m_StCamera.SnapShot();
			}
		}






	}
}