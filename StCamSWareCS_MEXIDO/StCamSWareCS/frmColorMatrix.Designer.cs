namespace StCamSWareCS
{
	partial class frmColorMatrix
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
			this.btnOK = new System.Windows.Forms.Button();
			this.settingNumericUpDown17 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.settingNumericUpDown1 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.settingNumericUpDown2 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.settingNumericUpDown3 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.settingNumericUpDown4 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.settingNumericUpDown5 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.settingNumericUpDown6 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.settingNumericUpDown7 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.settingNumericUpDown8 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.settingNumericUpDown9 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.settingNumericUpDown10 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.settingNumericUpDown11 = new StCamSWareCS.SettingCtrl.SettingNumericUpDown();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnGray = new System.Windows.Forms.Button();
			this.btnReverse = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown17)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown11)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(499, 98);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// settingNumericUpDown17
			// 
			this.settingNumericUpDown17.Location = new System.Drawing.Point(52, 14);
			this.settingNumericUpDown17.Name = "settingNumericUpDown17";
			this.settingNumericUpDown17.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_00;
			this.settingNumericUpDown17.SettingMax = 100;
			this.settingNumericUpDown17.SettingMin = 0;
			this.settingNumericUpDown17.SettingValue = 0;
			this.settingNumericUpDown17.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown17.TabIndex = 26;
			this.settingNumericUpDown17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 12);
			this.label1.TabIndex = 27;
			this.label1.Text = "R\' = (";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(121, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 12);
			this.label2.TabIndex = 28;
			this.label2.Text = "x R +";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown1
			// 
			this.settingNumericUpDown1.Location = new System.Drawing.Point(160, 14);
			this.settingNumericUpDown1.Name = "settingNumericUpDown1";
			this.settingNumericUpDown1.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_01;
			this.settingNumericUpDown1.SettingMax = 100;
			this.settingNumericUpDown1.SettingMin = 0;
			this.settingNumericUpDown1.SettingValue = 0;
			this.settingNumericUpDown1.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown1.TabIndex = 29;
			this.settingNumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(229, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(33, 12);
			this.label3.TabIndex = 30;
			this.label3.Text = "x G +";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(337, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(33, 12);
			this.label4.TabIndex = 32;
			this.label4.Text = "x B +";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown2
			// 
			this.settingNumericUpDown2.Location = new System.Drawing.Point(268, 14);
			this.settingNumericUpDown2.Name = "settingNumericUpDown2";
			this.settingNumericUpDown2.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_02;
			this.settingNumericUpDown2.SettingMax = 100;
			this.settingNumericUpDown2.SettingMin = 0;
			this.settingNumericUpDown2.SettingValue = 0;
			this.settingNumericUpDown2.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown2.TabIndex = 31;
			this.settingNumericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(445, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 34;
			this.label5.Text = ") / 100";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown3
			// 
			this.settingNumericUpDown3.Location = new System.Drawing.Point(376, 14);
			this.settingNumericUpDown3.Name = "settingNumericUpDown3";
			this.settingNumericUpDown3.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_03;
			this.settingNumericUpDown3.SettingMax = 100;
			this.settingNumericUpDown3.SettingMin = 0;
			this.settingNumericUpDown3.SettingValue = 0;
			this.settingNumericUpDown3.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown3.TabIndex = 33;
			this.settingNumericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(445, 45);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 12);
			this.label6.TabIndex = 43;
			this.label6.Text = ") / 100";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown4
			// 
			this.settingNumericUpDown4.Location = new System.Drawing.Point(376, 43);
			this.settingNumericUpDown4.Name = "settingNumericUpDown4";
			this.settingNumericUpDown4.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_13;
			this.settingNumericUpDown4.SettingMax = 100;
			this.settingNumericUpDown4.SettingMin = 0;
			this.settingNumericUpDown4.SettingValue = 0;
			this.settingNumericUpDown4.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown4.TabIndex = 42;
			this.settingNumericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(337, 45);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(33, 12);
			this.label7.TabIndex = 41;
			this.label7.Text = "x B +";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown5
			// 
			this.settingNumericUpDown5.Location = new System.Drawing.Point(268, 43);
			this.settingNumericUpDown5.Name = "settingNumericUpDown5";
			this.settingNumericUpDown5.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_12;
			this.settingNumericUpDown5.SettingMax = 100;
			this.settingNumericUpDown5.SettingMin = 0;
			this.settingNumericUpDown5.SettingValue = 0;
			this.settingNumericUpDown5.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown5.TabIndex = 40;
			this.settingNumericUpDown5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(229, 45);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(33, 12);
			this.label8.TabIndex = 39;
			this.label8.Text = "x G +";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown6
			// 
			this.settingNumericUpDown6.Location = new System.Drawing.Point(160, 43);
			this.settingNumericUpDown6.Name = "settingNumericUpDown6";
			this.settingNumericUpDown6.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_11;
			this.settingNumericUpDown6.SettingMax = 100;
			this.settingNumericUpDown6.SettingMin = 0;
			this.settingNumericUpDown6.SettingValue = 0;
			this.settingNumericUpDown6.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown6.TabIndex = 38;
			this.settingNumericUpDown6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(121, 45);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(33, 12);
			this.label9.TabIndex = 37;
			this.label9.Text = "x R +";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(13, 45);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(33, 12);
			this.label10.TabIndex = 36;
			this.label10.Text = "G\' = (";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown7
			// 
			this.settingNumericUpDown7.Location = new System.Drawing.Point(52, 43);
			this.settingNumericUpDown7.Name = "settingNumericUpDown7";
			this.settingNumericUpDown7.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_10;
			this.settingNumericUpDown7.SettingMax = 100;
			this.settingNumericUpDown7.SettingMin = 0;
			this.settingNumericUpDown7.SettingValue = 0;
			this.settingNumericUpDown7.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown7.TabIndex = 35;
			this.settingNumericUpDown7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(445, 74);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(41, 12);
			this.label11.TabIndex = 52;
			this.label11.Text = ") / 100";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown8
			// 
			this.settingNumericUpDown8.Location = new System.Drawing.Point(376, 72);
			this.settingNumericUpDown8.Name = "settingNumericUpDown8";
			this.settingNumericUpDown8.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_23;
			this.settingNumericUpDown8.SettingMax = 100;
			this.settingNumericUpDown8.SettingMin = 0;
			this.settingNumericUpDown8.SettingValue = 0;
			this.settingNumericUpDown8.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown8.TabIndex = 51;
			this.settingNumericUpDown8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(337, 74);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(33, 12);
			this.label12.TabIndex = 50;
			this.label12.Text = "x B +";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown9
			// 
			this.settingNumericUpDown9.Location = new System.Drawing.Point(268, 72);
			this.settingNumericUpDown9.Name = "settingNumericUpDown9";
			this.settingNumericUpDown9.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_22;
			this.settingNumericUpDown9.SettingMax = 100;
			this.settingNumericUpDown9.SettingMin = 0;
			this.settingNumericUpDown9.SettingValue = 0;
			this.settingNumericUpDown9.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown9.TabIndex = 49;
			this.settingNumericUpDown9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(229, 74);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(33, 12);
			this.label13.TabIndex = 48;
			this.label13.Text = "x G +";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown10
			// 
			this.settingNumericUpDown10.Location = new System.Drawing.Point(160, 72);
			this.settingNumericUpDown10.Name = "settingNumericUpDown10";
			this.settingNumericUpDown10.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_21;
			this.settingNumericUpDown10.SettingMax = 100;
			this.settingNumericUpDown10.SettingMin = 0;
			this.settingNumericUpDown10.SettingValue = 0;
			this.settingNumericUpDown10.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown10.TabIndex = 47;
			this.settingNumericUpDown10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(121, 74);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(33, 12);
			this.label14.TabIndex = 46;
			this.label14.Text = "x R +";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(13, 74);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(33, 12);
			this.label15.TabIndex = 45;
			this.label15.Text = "B\' = (";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// settingNumericUpDown11
			// 
			this.settingNumericUpDown11.Location = new System.Drawing.Point(52, 72);
			this.settingNumericUpDown11.Name = "settingNumericUpDown11";
			this.settingNumericUpDown11.SettingID = StCamSWareCS.SettingCtrl.SettingIDs.COLOR_MATRIX_20;
			this.settingNumericUpDown11.SettingMax = 100;
			this.settingNumericUpDown11.SettingMin = 0;
			this.settingNumericUpDown11.SettingValue = 0;
			this.settingNumericUpDown11.Size = new System.Drawing.Size(63, 19);
			this.settingNumericUpDown11.TabIndex = 44;
			this.settingNumericUpDown11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(499, 11);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(75, 23);
			this.btnReset.TabIndex = 53;
			this.btnReset.Tag = "0";
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnConst_Click);
			// 
			// btnGray
			// 
			this.btnGray.Location = new System.Drawing.Point(499, 40);
			this.btnGray.Name = "btnGray";
			this.btnGray.Size = new System.Drawing.Size(75, 23);
			this.btnGray.TabIndex = 54;
			this.btnGray.Tag = "1";
			this.btnGray.Text = "Gray";
			this.btnGray.UseVisualStyleBackColor = true;
			this.btnGray.Click += new System.EventHandler(this.btnConst_Click);
			// 
			// btnReverse
			// 
			this.btnReverse.Location = new System.Drawing.Point(499, 69);
			this.btnReverse.Name = "btnReverse";
			this.btnReverse.Size = new System.Drawing.Size(75, 23);
			this.btnReverse.TabIndex = 55;
			this.btnReverse.Tag = "2";
			this.btnReverse.Text = "Reverse";
			this.btnReverse.UseVisualStyleBackColor = true;
			this.btnReverse.Click += new System.EventHandler(this.btnConst_Click);
			// 
			// frmColorMatrix
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.ClientSize = new System.Drawing.Size(583, 127);
			this.Controls.Add(this.btnReverse);
			this.Controls.Add(this.btnGray);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.settingNumericUpDown8);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.settingNumericUpDown9);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.settingNumericUpDown10);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.settingNumericUpDown11);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.settingNumericUpDown4);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.settingNumericUpDown5);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.settingNumericUpDown6);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.settingNumericUpDown7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.settingNumericUpDown3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.settingNumericUpDown2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.settingNumericUpDown1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.settingNumericUpDown17);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmColorMatrix";
			this.Text = "Color Matrix";
			this.Load += new System.EventHandler(this.frmColorMatrix_Load);
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown17)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingNumericUpDown11)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.Button btnOK;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown17;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown2;
		private System.Windows.Forms.Label label5;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown3;
		private System.Windows.Forms.Label label6;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown4;
		private System.Windows.Forms.Label label7;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown5;
		private System.Windows.Forms.Label label8;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown7;
		private System.Windows.Forms.Label label11;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown8;
		private System.Windows.Forms.Label label12;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown9;
		private System.Windows.Forms.Label label13;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown10;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private StCamSWareCS.SettingCtrl.SettingNumericUpDown settingNumericUpDown11;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnGray;
		private System.Windows.Forms.Button btnReverse;


	}
}
