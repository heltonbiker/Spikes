using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StCamSWareCS
{
	public partial class frmSnapShot : Form
	{
		public frmSnapShot(Bitmap bitmap, uint imageNo)
		{
			InitializeComponent();
			m_Bitmap = bitmap;
			ClientSize = new Size(m_Bitmap.Width, m_Bitmap.Height);
			MaximumSize = Size;

			pictureBox.Location = new Point(0, 0);
			pictureBox.Image = m_Bitmap;

			m_dwIMageNo = imageNo;

			Text = imageNo.ToString();
		}
		private Bitmap m_Bitmap = null;
		private uint m_dwIMageNo = 0;

		public EventHandler SaveImage = null;

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (SaveImage != null)
			{
				SaveImage(this, new EventArgs());
			}
		}

		private void frmSnapShot_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Apps)
			{
				contextMenuStrip.Show(this, this.PointToClient(MousePosition));
			}

		}

	}
}