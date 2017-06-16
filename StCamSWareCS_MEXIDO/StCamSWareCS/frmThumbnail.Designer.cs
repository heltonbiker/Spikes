namespace StCamSWareCS
{
	partial class frmThumbnail
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.thumbnailCtrl = new StCamSWareCS.ThumbnailCtrl();
			this.SuspendLayout();
			// 
			// thumbnailCtrl
			// 
			this.thumbnailCtrl.AutoScroll = true;
			this.thumbnailCtrl.BackColor = System.Drawing.Color.White;
			this.thumbnailCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.thumbnailCtrl.Location = new System.Drawing.Point(0, 0);
			this.thumbnailCtrl.Name = "thumbnailCtrl";
			this.thumbnailCtrl.Size = new System.Drawing.Size(592, 506);
			this.thumbnailCtrl.TabIndex = 0;
			this.thumbnailCtrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.thumbnailCtrl_KeyDown);
			// 
			// frmThumbnail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(592, 506);
			this.Controls.Add(this.thumbnailCtrl);
			this.Name = "frmThumbnail";
			this.Text = "Thumbnail";
			this.SizeChanged += new System.EventHandler(this.frmThumbnail_SizeChanged);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmThumbnail_KeyDown);
			this.ResumeLayout(false);

		}

		private ThumbnailCtrl thumbnailCtrl;
	}
}