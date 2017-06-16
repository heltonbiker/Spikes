namespace StCamSWareCS
{
	partial class frmMain
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
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startstopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemx8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemx4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemx2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemx1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemxinv2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemxinv4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemxinv8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.adjustWindowSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.callbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snapShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.videoCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showThumbnailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.versionInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panelImage = new System.Windows.Forms.Panel();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelColor = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.captureToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(980, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// optionToolStripMenuItem
			// 
			this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startstopToolStripMenuItem,
            this.renameCameraToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.sizeToolStripMenuItem,
            this.callbackToolStripMenuItem,
            this.statusbarToolStripMenuItem});
			this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
			this.optionToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.optionToolStripMenuItem.Text = "&Option";
			this.optionToolStripMenuItem.DropDownOpening += new System.EventHandler(this.optionToolStripMenuItem_DropDownOpening);
			// 
			// startstopToolStripMenuItem
			// 
			this.startstopToolStripMenuItem.Name = "startstopToolStripMenuItem";
			this.startstopToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.startstopToolStripMenuItem.Text = "&Freeze";
			this.startstopToolStripMenuItem.Click += new System.EventHandler(this.startstopToolStripMenuItem_Click);
			// 
			// renameCameraToolStripMenuItem
			// 
			this.renameCameraToolStripMenuItem.Name = "renameCameraToolStripMenuItem";
			this.renameCameraToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.renameCameraToolStripMenuItem.Text = "&Rename camera...";
			this.renameCameraToolStripMenuItem.Click += new System.EventHandler(this.renameCameraToolStripMenuItem_Click);
			// 
			// settingToolStripMenuItem
			// 
			this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
			this.settingToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.settingToolStripMenuItem.Text = "&Setting...";
			this.settingToolStripMenuItem.Click += new System.EventHandler(this.menuItemSetting_Click);
			// 
			// sizeToolStripMenuItem
			// 
			this.sizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemx8,
            this.toolStripMenuItemx4,
            this.toolStripMenuItemx2,
            this.toolStripMenuItemx1,
            this.toolStripMenuItemxinv2,
            this.toolStripMenuItemxinv4,
            this.toolStripMenuItemxinv8,
            this.toolStripMenuItem1,
            this.adjustWindowSizeToolStripMenuItem});
			this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
			this.sizeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.sizeToolStripMenuItem.Text = "Si&ze";
			this.sizeToolStripMenuItem.DropDownOpening += new System.EventHandler(this.sizeToolStripMenuItem_DropDownOpening);
			// 
			// toolStripMenuItemx8
			// 
			this.toolStripMenuItemx8.Name = "toolStripMenuItemx8";
			this.toolStripMenuItemx8.Size = new System.Drawing.Size(168, 22);
			this.toolStripMenuItemx8.Tag = "3";
			this.toolStripMenuItemx8.Text = "8";
			this.toolStripMenuItemx8.Click += new System.EventHandler(this.previewSizeMenuItem_Click);
			// 
			// toolStripMenuItemx4
			// 
			this.toolStripMenuItemx4.Name = "toolStripMenuItemx4";
			this.toolStripMenuItemx4.Size = new System.Drawing.Size(168, 22);
			this.toolStripMenuItemx4.Tag = "2";
			this.toolStripMenuItemx4.Text = "4";
			this.toolStripMenuItemx4.Click += new System.EventHandler(this.previewSizeMenuItem_Click);
			// 
			// toolStripMenuItemx2
			// 
			this.toolStripMenuItemx2.Name = "toolStripMenuItemx2";
			this.toolStripMenuItemx2.Size = new System.Drawing.Size(168, 22);
			this.toolStripMenuItemx2.Tag = "1";
			this.toolStripMenuItemx2.Text = "2";
			this.toolStripMenuItemx2.Click += new System.EventHandler(this.previewSizeMenuItem_Click);
			// 
			// toolStripMenuItemx1
			// 
			this.toolStripMenuItemx1.Name = "toolStripMenuItemx1";
			this.toolStripMenuItemx1.Size = new System.Drawing.Size(168, 22);
			this.toolStripMenuItemx1.Tag = "0";
			this.toolStripMenuItemx1.Text = "1";
			this.toolStripMenuItemx1.Click += new System.EventHandler(this.previewSizeMenuItem_Click);
			// 
			// toolStripMenuItemxinv2
			// 
			this.toolStripMenuItemxinv2.Name = "toolStripMenuItemxinv2";
			this.toolStripMenuItemxinv2.Size = new System.Drawing.Size(168, 22);
			this.toolStripMenuItemxinv2.Tag = "-1";
			this.toolStripMenuItemxinv2.Text = "1/2";
			this.toolStripMenuItemxinv2.Click += new System.EventHandler(this.previewSizeMenuItem_Click);
			// 
			// toolStripMenuItemxinv4
			// 
			this.toolStripMenuItemxinv4.Name = "toolStripMenuItemxinv4";
			this.toolStripMenuItemxinv4.Size = new System.Drawing.Size(168, 22);
			this.toolStripMenuItemxinv4.Tag = "-2";
			this.toolStripMenuItemxinv4.Text = "1/4";
			this.toolStripMenuItemxinv4.Click += new System.EventHandler(this.previewSizeMenuItem_Click);
			// 
			// toolStripMenuItemxinv8
			// 
			this.toolStripMenuItemxinv8.Name = "toolStripMenuItemxinv8";
			this.toolStripMenuItemxinv8.Size = new System.Drawing.Size(168, 22);
			this.toolStripMenuItemxinv8.Tag = "-3";
			this.toolStripMenuItemxinv8.Text = "1/8";
			this.toolStripMenuItemxinv8.Click += new System.EventHandler(this.previewSizeMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 6);
			// 
			// adjustWindowSizeToolStripMenuItem
			// 
			this.adjustWindowSizeToolStripMenuItem.Name = "adjustWindowSizeToolStripMenuItem";
			this.adjustWindowSizeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.adjustWindowSizeToolStripMenuItem.Text = "&Adjust window size";
			this.adjustWindowSizeToolStripMenuItem.Click += new System.EventHandler(this.adjustWindowSizeToolStripMenuItem_Click);
			// 
			// callbackToolStripMenuItem
			// 
			this.callbackToolStripMenuItem.Name = "callbackToolStripMenuItem";
			this.callbackToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.callbackToolStripMenuItem.Text = "&Callback...";
			this.callbackToolStripMenuItem.Click += new System.EventHandler(this.callbackToolStripMenuItem_Click);
			// 
			// statusbarToolStripMenuItem
			// 
			this.statusbarToolStripMenuItem.Name = "statusbarToolStripMenuItem";
			this.statusbarToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.statusbarToolStripMenuItem.Text = "Status &bar";
			this.statusbarToolStripMenuItem.Click += new System.EventHandler(this.statusbarToolStripMenuItem_Click);
			// 
			// captureToolStripMenuItem
			// 
			this.captureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snapShotToolStripMenuItem,
            this.videoCaptureToolStripMenuItem});
			this.captureToolStripMenuItem.Name = "captureToolStripMenuItem";
			this.captureToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
			this.captureToolStripMenuItem.Text = "&Capture";
			// 
			// snapShotToolStripMenuItem
			// 
			this.snapShotToolStripMenuItem.Name = "snapShotToolStripMenuItem";
			this.snapShotToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.snapShotToolStripMenuItem.Text = "&Snap shot";
			this.snapShotToolStripMenuItem.Click += new System.EventHandler(this.snapShotToolStripMenuItem_Click);
			// 
			// videoCaptureToolStripMenuItem
			// 
			this.videoCaptureToolStripMenuItem.Name = "videoCaptureToolStripMenuItem";
			this.videoCaptureToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.videoCaptureToolStripMenuItem.Text = "&Video capture...";
			this.videoCaptureToolStripMenuItem.Click += new System.EventHandler(this.videoCaptureToolStripMenuItem_Click);
			// 
			// windowToolStripMenuItem
			// 
			this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showThumbnailToolStripMenuItem});
			this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
			this.windowToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
			this.windowToolStripMenuItem.Text = "&Window";
			// 
			// showThumbnailToolStripMenuItem
			// 
			this.showThumbnailToolStripMenuItem.Name = "showThumbnailToolStripMenuItem";
			this.showThumbnailToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.showThumbnailToolStripMenuItem.Text = "&Show thumbnail";
			this.showThumbnailToolStripMenuItem.Click += new System.EventHandler(this.showThumbnailToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionInformationToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// versionInformationToolStripMenuItem
			// 
			this.versionInformationToolStripMenuItem.Name = "versionInformationToolStripMenuItem";
			this.versionInformationToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.versionInformationToolStripMenuItem.Text = "&Version information";
			this.versionInformationToolStripMenuItem.Click += new System.EventHandler(this.versionInformationToolStripMenuItem_Click);
			// 
			// panelImage
			// 
			this.panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelImage.Location = new System.Drawing.Point(0, 24);
			this.panelImage.Name = "panelImage";
			this.panelImage.Size = new System.Drawing.Size(980, 526);
			this.panelImage.TabIndex = 1;
			this.panelImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelImage_MouseMove);
			this.panelImage.Resize += new System.EventHandler(this.panelImage_Resize);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelColor});
			this.statusStrip.Location = new System.Drawing.Point(0, 550);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(980, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip1";
			this.statusStrip.Visible = false;
			// 
			// toolStripStatusLabelColor
			// 
			this.toolStripStatusLabelColor.Name = "toolStripStatusLabelColor";
			this.toolStripStatusLabelColor.Size = new System.Drawing.Size(0, 17);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(980, 572);
			this.Controls.Add(this.panelImage);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "frmMain";
			this.Text = "StCamSWareCS";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseMove);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
		private System.Windows.Forms.Panel panelImage;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem startstopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameCameraToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemx8;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemx4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemx2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemx1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemxinv2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemxinv4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemxinv8;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem adjustWindowSizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem callbackToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem statusbarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem snapShotToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem videoCaptureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showThumbnailToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem versionInformationToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelColor;
	}
}

