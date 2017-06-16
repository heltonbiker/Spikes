namespace Pixelformat
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.icImagingControl1 = new TIS.Imaging.ICImagingControl();
            this.cmdY800 = new System.Windows.Forms.Button();
            this.cmdRGB8 = new System.Windows.Forms.Button();
            this.cmdRGB24 = new System.Windows.Forms.Button();
            this.cmdRGB32 = new System.Windows.Forms.Button();
            this.cmdYGB0 = new System.Windows.Forms.Button();
            this.cmdYGB1 = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // icImagingControl1
            // 
            this.icImagingControl1.BackColor = System.Drawing.Color.White;
            this.icImagingControl1.LiveDisplayPosition = new System.Drawing.Point(0, 0);
            this.icImagingControl1.Location = new System.Drawing.Point(8, 9);
            this.icImagingControl1.Name = "icImagingControl1";
            this.icImagingControl1.Size = new System.Drawing.Size(320, 224);
            this.icImagingControl1.TabIndex = 0;
            // 
            // cmdY800
            // 
            this.cmdY800.Location = new System.Drawing.Point(334, 9);
            this.cmdY800.Name = "cmdY800";
            this.cmdY800.Size = new System.Drawing.Size(75, 23);
            this.cmdY800.TabIndex = 1;
            this.cmdY800.Text = "Y800";
            this.cmdY800.UseVisualStyleBackColor = true;
            this.cmdY800.Click += new System.EventHandler(this.cmdY800_Click);
            // 
            // cmdRGB8
            // 
            this.cmdRGB8.Location = new System.Drawing.Point(334, 38);
            this.cmdRGB8.Name = "cmdRGB8";
            this.cmdRGB8.Size = new System.Drawing.Size(75, 23);
            this.cmdRGB8.TabIndex = 2;
            this.cmdRGB8.Text = "RGB8";
            this.cmdRGB8.UseVisualStyleBackColor = true;
            this.cmdRGB8.Click += new System.EventHandler(this.cmdRGB8_Click);
            // 
            // cmdRGB24
            // 
            this.cmdRGB24.Location = new System.Drawing.Point(334, 67);
            this.cmdRGB24.Name = "cmdRGB24";
            this.cmdRGB24.Size = new System.Drawing.Size(75, 23);
            this.cmdRGB24.TabIndex = 3;
            this.cmdRGB24.Text = "RGB24";
            this.cmdRGB24.UseVisualStyleBackColor = true;
            this.cmdRGB24.Click += new System.EventHandler(this.cmdRGB24_Click);
            // 
            // cmdRGB32
            // 
            this.cmdRGB32.Location = new System.Drawing.Point(334, 96);
            this.cmdRGB32.Name = "cmdRGB32";
            this.cmdRGB32.Size = new System.Drawing.Size(75, 23);
            this.cmdRGB32.TabIndex = 4;
            this.cmdRGB32.Text = "RGB32";
            this.cmdRGB32.UseVisualStyleBackColor = true;
            this.cmdRGB32.Click += new System.EventHandler(this.cmdRGB32_Click);
            // 
            // cmdYGB0
            // 
            this.cmdYGB0.Location = new System.Drawing.Point(334, 125);
            this.cmdYGB0.Name = "cmdYGB0";
            this.cmdYGB0.Size = new System.Drawing.Size(75, 23);
            this.cmdYGB0.TabIndex = 5;
            this.cmdYGB0.Text = "YGB0";
            this.cmdYGB0.UseVisualStyleBackColor = true;
            this.cmdYGB0.Click += new System.EventHandler(this.cmdYGB0_Click);
            // 
            // cmdYGB1
            // 
            this.cmdYGB1.Location = new System.Drawing.Point(334, 154);
            this.cmdYGB1.Name = "cmdYGB1";
            this.cmdYGB1.Size = new System.Drawing.Size(75, 23);
            this.cmdYGB1.TabIndex = 6;
            this.cmdYGB1.Text = "YGB1";
            this.cmdYGB1.UseVisualStyleBackColor = true;
            this.cmdYGB1.Click += new System.EventHandler(this.cmdYGB1_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Enabled = false;
            this.txtOutput.Location = new System.Drawing.Point(12, 245);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(315, 76);
            this.txtOutput.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 334);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.cmdYGB1);
            this.Controls.Add(this.cmdYGB0);
            this.Controls.Add(this.cmdRGB32);
            this.Controls.Add(this.cmdRGB24);
            this.Controls.Add(this.cmdRGB8);
            this.Controls.Add(this.cmdY800);
            this.Controls.Add(this.icImagingControl1);
            this.Name = "Form1";
            this.Text = "Accessing an Image Buffer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TIS.Imaging.ICImagingControl icImagingControl1;
        private System.Windows.Forms.Button cmdY800;
        private System.Windows.Forms.Button cmdRGB8;
        private System.Windows.Forms.Button cmdRGB24;
        private System.Windows.Forms.Button cmdRGB32;
        private System.Windows.Forms.Button cmdYGB0;
        private System.Windows.Forms.Button cmdYGB1;
        private System.Windows.Forms.TextBox txtOutput;
    }
}

