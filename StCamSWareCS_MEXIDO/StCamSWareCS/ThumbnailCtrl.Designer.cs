namespace StCamSWareCS
{
	partial class ThumbnailCtrl
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
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snapShotNoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x14ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x18ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.snapShotNoToolStripMenuItem,
            this.sizeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.deleteAllToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.ShowImageMargin = false;
			this.contextMenuStrip.Size = new System.Drawing.Size(128, 170);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// showToolStripMenuItem
			// 
			this.showToolStripMenuItem.Name = "showToolStripMenuItem";
			this.showToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.showToolStripMenuItem.Text = "Show";
			this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
			// 
			// snapShotNoToolStripMenuItem
			// 
			this.snapShotNoToolStripMenuItem.Name = "snapShotNoToolStripMenuItem";
			this.snapShotNoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.snapShotNoToolStripMenuItem.Text = "Snap shot No.";
			this.snapShotNoToolStripMenuItem.Click += new System.EventHandler(this.snapShotNoToolStripMenuItem_Click);
			// 
			// sizeToolStripMenuItem
			// 
			this.sizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x8ToolStripMenuItem,
            this.x4ToolStripMenuItem,
            this.x2ToolStripMenuItem,
            this.x1ToolStripMenuItem,
            this.x12ToolStripMenuItem,
            this.x14ToolStripMenuItem,
            this.x18ToolStripMenuItem});
			this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
			this.sizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.sizeToolStripMenuItem.Text = "Size";
			// 
			// x8ToolStripMenuItem
			// 
			this.x8ToolStripMenuItem.Name = "x8ToolStripMenuItem";
			this.x8ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x8ToolStripMenuItem.Tag = "3";
			this.x8ToolStripMenuItem.Text = "x8";
			this.x8ToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
			// 
			// x4ToolStripMenuItem
			// 
			this.x4ToolStripMenuItem.Name = "x4ToolStripMenuItem";
			this.x4ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x4ToolStripMenuItem.Tag = "2";
			this.x4ToolStripMenuItem.Text = "x4";
			this.x4ToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
			// 
			// x2ToolStripMenuItem
			// 
			this.x2ToolStripMenuItem.Name = "x2ToolStripMenuItem";
			this.x2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x2ToolStripMenuItem.Tag = "1";
			this.x2ToolStripMenuItem.Text = "x2";
			this.x2ToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
			// 
			// x1ToolStripMenuItem
			// 
			this.x1ToolStripMenuItem.Name = "x1ToolStripMenuItem";
			this.x1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x1ToolStripMenuItem.Tag = "0";
			this.x1ToolStripMenuItem.Text = "x1";
			this.x1ToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
			// 
			// x12ToolStripMenuItem
			// 
			this.x12ToolStripMenuItem.Name = "x12ToolStripMenuItem";
			this.x12ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x12ToolStripMenuItem.Tag = "-1";
			this.x12ToolStripMenuItem.Text = "x1/2";
			this.x12ToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
			// 
			// x14ToolStripMenuItem
			// 
			this.x14ToolStripMenuItem.Name = "x14ToolStripMenuItem";
			this.x14ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x14ToolStripMenuItem.Tag = "-2";
			this.x14ToolStripMenuItem.Text = "x1/4";
			this.x14ToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
			// 
			// x18ToolStripMenuItem
			// 
			this.x18ToolStripMenuItem.Name = "x18ToolStripMenuItem";
			this.x18ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x18ToolStripMenuItem.Tag = "-3";
			this.x18ToolStripMenuItem.Text = "x1/8";
			this.x18ToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// deleteAllToolStripMenuItem
			// 
			this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
			this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.deleteAllToolStripMenuItem.Text = "Delete all";
			this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.deleteAllToolStripMenuItem_Click);
			// 
			// ThumbnailCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ContextMenuStrip = this.contextMenuStrip;
			this.Name = "ThumbnailCtrl";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ThumbnailCtrl_Paint);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ThumbnailCtrl_MouseDoubleClick);
			this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ThumbnailCtrl_Scroll);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ThumbnailCtrl_MouseClick);
			this.SizeChanged += new System.EventHandler(this.ThumbnailCtrl_SizeChanged);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem snapShotNoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x8ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x4ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x12ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x14ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x18ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;

	}
}
