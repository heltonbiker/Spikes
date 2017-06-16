using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TIS.Imaging;

namespace Creating_an_Overlay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private System.Windows.Forms.Form m_sinkDisplay;
		private FrameHandlerSink m_sink;

        private void Form1_Load(object sender, EventArgs e)
        {
            icImagingControl1.OverlayBitmapPosition = TIS.Imaging.PathPositions.Device;
			m_sink = new FrameHandlerSink(false, new FrameType("RGB32"));
			icImagingControl1.Sink = m_sink;

            chkPPDevice.Checked = true;
            chkPPSink.Checked = false;
            chkPPDisplay.Checked = false;

            btnBestFit.Checked = true;

            icImagingControl1.LiveCaptureContinuous = true;
        }

        /// <summary>
        /// cmdDevice_Click
        ///
        /// Show the device selection dialog. This dialog is imported from
        /// the "Making Device Settings" directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		        private void cmdDevice_Click(object sender, EventArgs e)
        {
            cmdStartStop.Text = "Start";

            bool wasLive = icImagingControl1.LiveVideoRunning;
            if (wasLive)
                icImagingControl1.LiveStop();

            icImagingControl1.ShowDeviceSettingsDialog();

            if (icImagingControl1.DeviceValid)
            {
                if (wasLive)
                {
                    icImagingControl1.LiveStart();
                    cmdStartStop.Text = "Stop";
                }

                cmdStartStop.Enabled = true;
                cmdSettings.Enabled = true;
            }
        }
		
        /// <summary>
        /// cmdSettings_Click
		/// 
        /// Show the property dialog to adjust the camera and video properties.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		        private void cmdSettings_Click(object sender, EventArgs e)
        {
            if (icImagingControl1.DeviceValid)
            {
                icImagingControl1.ShowPropertyDialog();
            }
        }
		
        /// <summary>
        /// cmdStartStop_Click
		/// 
        /// Start and stop the live video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		        private void cmdStartStop_Click(object sender, EventArgs e)
        {
            if (icImagingControl1.LiveVideoRunning)
            {
                icImagingControl1.LiveStop();
                cmdStartStop.Text = "Start";
            }
            else
            {
				if( icImagingControl1.DeviceValid )
				{
					icImagingControl1.LiveStart();
					cmdStartStop.Text = "Stop";
				}
            }		
        }
		
        /// <summary>
        /// SetupOverlay		
		/// Enables an OverlayBitmap object, sets a dropOutColor (magenta) and fills it with
        /// that color
        /// </summary>
        /// <param name="ob"></param>
		        private void SetupOverlay( TIS.Imaging.OverlayBitmap ob )
		{
			// Enable the overlay bitmap for drawing.
			ob.Enable = true;

			// Set magenta as dropout color.
			ob.DropOutColor = Color.Magenta;

			// Fill the overlay bitmap with the dropout color.
			ob.Fill( ob.DropOutColor );
    
			// Print text in red.
			ob.FontTransparent = true;
			ob.DrawText( Color.Red, 10, 10, "IC Imaging Control 3.0" );
		}
		
        /// <summary>
        /// After the filtergraph has bee prepared, text can be printed
        /// on the overlay bitmap.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		        private void icImagingControl1_LivePrepared(object sender, EventArgs e)
        {
            if (icImagingControl1.DeviceValid)
            {
				if( (icImagingControl1.OverlayBitmapPosition & PathPositions.Device) != 0 )
				{
					SetupOverlay(icImagingControl1.OverlayBitmapAtPath[PathPositions.Device]);

					// Display a coordinate system on the device overlay
					DrawCoordinatesystem(icImagingControl1.OverlayBitmapAtPath[PathPositions.Device]);

					// Draw overlay info box
					DrawOverlayInfo(icImagingControl1.OverlayBitmapAtPath[PathPositions.Device]);
				}

				if ((icImagingControl1.OverlayBitmapPosition & PathPositions.Sink) != 0)
				{
					SetupOverlay(icImagingControl1.OverlayBitmapAtPath[PathPositions.Sink]);

					// Draw overlay info box
					DrawOverlayInfo(icImagingControl1.OverlayBitmapAtPath[PathPositions.Sink]);
				}

				if ((icImagingControl1.OverlayBitmapPosition & PathPositions.Display) != 0)
				{
					SetupOverlay(icImagingControl1.OverlayBitmapAtPath[PathPositions.Display]);

					// Load a  bitmap file and display it on the display overlay
					ShowBitmap(icImagingControl1.OverlayBitmapAtPath[PathPositions.Display]);

					// Draw overlay info box
					DrawOverlayInfo(icImagingControl1.OverlayBitmapAtPath[PathPositions.Display]);
				}
			}
        }
		
        /// <summary>
        /// DrawOverlayInfo		 
        /// </summary>
        /// <param name="ob"></param>
        private void DrawOverlayInfo(OverlayBitmap ob)
        {
            switch (ob.PathPosition)
            {
                case PathPositions.Device:
                    ob.DrawFrameRect(Color.Red, 90, 90, 200, 130);
                    ob.DrawText(Color.Red, 100, 100, "Device Overlay");
                    break;
                case PathPositions.Sink:
                    ob.DrawFrameRect(Color.Red, 390, 90, 500, 130);
                    ob.DrawText(Color.Red, 400, 100, "Sink Overlay");
                    break;
                case PathPositions.Display:
                    ob.DrawFrameRect(Color.Red, 90, 290, 200, 330);
                    ob.DrawText(Color.Red, 100, 300, "Display Overlay");
                    break;
            }
        }

        /// <summary>
        /// DrawCoordinatesystem
        /// Draw a coordinate plane on the overlay.
        /// </summary>
        /// <param name="ob"></param>
		        private void DrawCoordinatesystem(TIS.Imaging.OverlayBitmap ob)
        {
            // Calculate the center of the video image.
            int Col = icImagingControl1.ImageWidth / 2;
            int Row = icImagingControl1.ImageHeight / 2;

            Font OldFont = ob.Font;
            ob.Font = new Font("Arial", 8);

            ob.DrawLine(Color.Red, Col, 0, Col, icImagingControl1.ImageHeight);
            ob.DrawLine(Color.Red, 0, Row, icImagingControl1.ImageWidth, Row);

            for (int i = 0; i < Row; i += 20)
            {
                ob.DrawLine(Color.Red, Col - 5, Row - i, Col + 5, Row - i);
                ob.DrawLine(Color.Red, Col - 5, Row + i, Col + 5, Row + i);
                if (i > 0)
                {
                    ob.DrawText(Color.Red, Col + 10, Row - i - 7, string.Format("{0}", i / 10));
                    ob.DrawText(Color.Red, Col + 10, Row + i - 7, string.Format("{0}", -i / 10));
                }
            }

            for (int i = 0; i < Col; i += 20)
            {
                ob.DrawLine(Color.Red, Col - i, Row - 5, Col - i, Row + 5);
                ob.DrawLine(Color.Red, Col + i, Row - 5, Col + i, Row + 5);
                if (i > 0)
                {
                    ob.DrawText(Color.Red, Col + i - 5, Row - 17, string.Format("{0}", i / 10));
                    ob.DrawText(Color.Red, Col - i - 10, Row - 17, string.Format("{0}", -i / 10));
                }
            }

            ob.Font = OldFont;
        }
		
        /// <summary>
        /// ImagingControl1_OverlayUpdate
		/// 
 		/// In the overlay update event, the frames are counted in
 		/// the variable FrameCount. FrameCount is used to draw a
		/// rising triangle to show the frame count. If FrameCount
		/// is greater than 25, the drawn triangle is deleted by
        /// drawing a solid rectangle with the dropout color over it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		        private void icImagingControl1_OverlayUpdate(object sender, TIS.Imaging.ICImagingControl.OverlayUpdateEventArgs e)
        {
            TIS.Imaging.OverlayBitmap ob = e.overlay;

			int lineIndex = m_sink.FrameCount % 25;
			if (lineIndex == 0)
            {
                ob.DrawSolidRect(ob.DropOutColor, 10, icImagingControl1.ImageHeight - 70, 62,
                                 icImagingControl1.ImageHeight - 9);
            }

			ob.DrawLine(Color.Yellow, lineIndex * 2 + 10,
                                            icImagingControl1.ImageHeight - 10,
											lineIndex * 2 + 10,
                                            icImagingControl1.ImageHeight - 10 - lineIndex);

            // Print the current frame number:
            // Set the background color to the current dropout color
            // and make the font opaque.
            ob.FontBackColor = ob.DropOutColor;
            ob.FontTransparent = false;

            // Save the current font.
            Font OldFont = ob.Font;

            // Draw the text.
            ob.Font = new Font("Arial", 8);
            ob.DrawText(Color.Yellow, 70, icImagingControl1.ImageHeight - 19,
                        m_sink.FrameCount.ToString());

            // Restore the previously used font.
            ob.Font = OldFont;
        }
		
        /// <summary>
        /// Timer1_Tick
		
		/// In the timer event, the current time is drawn on the live
        /// video stream.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		        private void timer1_Tick(object sender, EventArgs e)
        {
            TIS.Imaging.OverlayBitmap ob = icImagingControl1.OverlayBitmapAtPath[TIS.Imaging.PathPositions.Device];

            if (icImagingControl1.LiveVideoRunning)
            {
                // Save the current font.
                Font OldFont = ob.Font;

                ob.Font = new Font("Arial", 14, FontStyle.Bold);

                // Draw the time in the lower left corner of the video window.
                int Col = icImagingControl1.ImageWidth - 81;
                int Row = icImagingControl1.ImageHeight - 20;

                // Setup the background color and drawing mode.
                ob.FontTransparent = false;
                ob.FontBackColor = Color.Black;

                // Draw the current time with white color.
                ob.DrawText(Color.White, Col, Row, DateTime.Now.ToString("T"));

                // Restore the previously used font.
                ob.Font = OldFont;
            }
        }
		
        /// <summary>
        /// ShowBitmap
        /// This sub demonstrates how to use OverlayBitmap.GetGraphics to draw a bitmap
        /// from a file on the live video.
        /// The bitmap will be drawn with transparency on the live video because
        /// it's background color is magenta (load the image "Hardware.BMP"
        /// with "Paint.exe" to verify this). Magenta is the currently set
        /// dropout color. 
        /// </summary>
        /// <param name="ob"></param>
		        private void ShowBitmap(TIS.Imaging.OverlayBitmap ob)
        {
            try
            {
                // Load the sample bitmap from the project file's directory.
                Image bmp = Bitmap.FromFile(Application.StartupPath + @"\..\..\hardware.bmp");

                // Calculate the column to display the bitmap in the
                // upper right corner of Imaging Control.
                int col = icImagingControl1.ImageWidth - 5 - bmp.Width;

                // Retrieve the Graphics object of the OverlayBitmap.
                Graphics g = ob.GetGraphics();

                // Draw the image
                g.DrawImage(bmp, col, 5);

                // Release the Graphics after drawing is finished.
                ob.ReleaseGraphics(g);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("File not found: " + Ex.Message);
            }
        }
				
		/// <summary>
        /// EnableOverlayBitmapAtPath
        /// 
        /// Enables or disables the overlay bitmap for a specified path position.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="enabled"></param>
		        private void EnableOverlayBitmapAtPath(PathPositions pos, bool enabled)
        {
            bool wasLive = icImagingControl1.LiveVideoRunning;
            if (wasLive)
                icImagingControl1.LiveStop();

            PathPositions oldPos = icImagingControl1.OverlayBitmapPosition;
            if (enabled)
                icImagingControl1.OverlayBitmapPosition = oldPos | pos;
            else
                icImagingControl1.OverlayBitmapPosition = oldPos & ~pos;

            if (wasLive)
                icImagingControl1.LiveStart();
        }
		
        private void chkPPDevice_CheckedChanged(object sender, EventArgs e)
        {
            EnableOverlayBitmapAtPath(PathPositions.Device, chkPPDevice.Checked);
        }

        private void chkPPSink_CheckedChanged(object sender, EventArgs e)
        {
            EnableOverlayBitmapAtPath(PathPositions.Sink, chkPPSink.Checked);

            if (chkPPSink.Checked)
            {
                if (m_sinkDisplay == null || m_sinkDisplay.IsDisposed)
                {
                    m_sinkDisplay = new Form();
                    m_sinkDisplay.Text = "Sink";
                }

                if (icImagingControl1.DeviceValid)
                {
                    Size d = m_sinkDisplay.Size - m_sinkDisplay.ClientSize;

                    m_sinkDisplay.Width = icImagingControl1.ImageWidth + d.Width;
                    m_sinkDisplay.Height = icImagingControl1.ImageHeight + d.Height;
                }

                m_sinkDisplay.Show();
            }
        }

        private void chkPPDisplay_CheckedChanged(object sender, EventArgs e)
        {
            EnableOverlayBitmapAtPath(PathPositions.Display, chkPPDisplay.Checked);
        }

        private void icImagingControl1_ImageAvailable(object sender, TIS.Imaging.ICImagingControl.ImageAvailableEventArgs e)
        {
            if (m_sinkDisplay != null && !m_sinkDisplay.IsDisposed)
            {
                m_sinkDisplay.BackgroundImage = e.ImageBuffer.Bitmap;
            }
        }

        /// <summary>
        /// SetOverlayColorMode
		///
        ///	Sets the color modes of all overlay bitmaps to <mode>
        /// </summary>
        /// <param name="mode"></param>
        private void setOverlayBitmapColorModes(OverlayColorModes mode)
        {
            bool wasLive = icImagingControl1.LiveVideoRunning;
            if (wasLive)
                icImagingControl1.LiveStop();

            icImagingControl1.OverlayBitmapAtPath[PathPositions.Device].ColorMode = mode;
            icImagingControl1.OverlayBitmapAtPath[PathPositions.Display].ColorMode = mode;
            icImagingControl1.OverlayBitmapAtPath[PathPositions.Sink].ColorMode = mode;

            if (wasLive)
                icImagingControl1.LiveStart();
        }

        private void btnColor_CheckedChanged(object sender, EventArgs e)
        {
            if (btnColor.Checked)
                setOverlayBitmapColorModes(OverlayColorModes.Color);
        }

        private void btnGrayscale_CheckedChanged(object sender, EventArgs e)
        {
            if (btnGrayscale.Checked)
                setOverlayBitmapColorModes(OverlayColorModes.Grayscale);
        }

        private void btnBestFit_CheckedChanged(object sender, EventArgs e)
        {
            if (btnBestFit.Checked)
                setOverlayBitmapColorModes(OverlayColorModes.BestFit);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_sinkDisplay != null && !m_sinkDisplay.IsDisposed)
            {
                m_sinkDisplay.Close();
            }	
        }





    }
	}