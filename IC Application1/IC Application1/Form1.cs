using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IC_Application1
{
    public partial class Form1 : Form
    {
        private delegate void DeviceLostDelegate();
        private delegate void ShowBufferDelegate(TIS.Imaging.ImageBuffer buffer);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Try to load the previously used device. 
            try
            {
                icImagingControl1.LoadDeviceStateFromFile("device.xml", true);
                if (icImagingControl1.DeviceValid)
                {

                }
            }
            catch
            {
                // Either the xml file does not exist or the device
                // could not be loaded. In both cases we do nothing and proceed.
            }

            // Update the menu and toolbar controls.
            UpdateControls();




            icImagingControl1.LiveCaptureLastImage = false;

        }

        /// <summary>
        /// Stop the live video stream, if the application is closed.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (icImagingControl1.DeviceValid)
            {
                icImagingControl1.LiveStop();
            }
        }

        /// <summary>
        /// Show the device selection dialog of IC Imaging Control.
        /// </summary>
        private void OpenNewVideoCaptureDevice()
        {

            if (icImagingControl1.DeviceValid)
            {
                icImagingControl1.LiveStop();
            }
            else
            {
                icImagingControl1.Device = "";
            }
            UpdateControls(); // Disable the controls.
            icImagingControl1.ShowDeviceSettingsDialog();
            if (icImagingControl1.DeviceValid)
            {
                // Save the currently used device into a file in order to be able to open it
                // automatically at the next program start.
                icImagingControl1.SaveDeviceStateToFile("device.xml");
                // Enable the menu and toolbar controls.
                UpdateControls();

            }
        }

        /// <summary>
        /// Update the controls in the toolbar and the menu, according
        /// to the device state.
        /// </summary>
        /// <param name="bEnable"></param>
        private void UpdateControls()
        {
            menuItemImageSettings.Enabled = icImagingControl1.DeviceValid;

            if (icImagingControl1.DeviceValid)
            {
                menuItemLiveStart.Enabled = !icImagingControl1.LiveVideoRunning;
                menuItemDevice.Enabled = !icImagingControl1.LiveVideoRunning;
                menuItemLiveStop.Enabled = icImagingControl1.LiveVideoRunning;

            }
            else
            {
                menuItemLiveStart.Enabled = false;
                menuItemLiveStop.Enabled = false;
                menuItemDevice.Enabled = true;

            }
        }

        /// <summary>
        /// Start the live video and update the state of the start/stop button.
        /// </summary>
        private void StartLiveVideo()
        {
            icImagingControl1.LiveStart();
            UpdateControls();
        }

        /// <summary>
        /// Stop the live video and update the state of the start/stop button.
        /// </summary>
        private void StopLiveVideo()
        {
            icImagingControl1.LiveStop();
            UpdateControls();
        }

        /// <summary>
        /// Show the device's property dialog for exposure, brightness etc. The 
        /// changes that were made using the dialog are saved to the file 'device.xml'.
        /// </summary>
        private void ShowDeviceProperties()
        {
            if (icImagingControl1.DeviceValid == true)
            {
                icImagingControl1.ShowPropertyDialog();
                icImagingControl1.SaveDeviceStateToFile("device.xml");
            }
        }

        /// <summary>
        /// Messagehandler for the menu and the toolbar.
        /// </summary>

        private void menuItemDevice_Click(object sender, EventArgs e)
        {
            OpenNewVideoCaptureDevice();
        }

        private void menuItemImageSettings_Click(object sender, EventArgs e)
        {
            ShowDeviceProperties();
        }

        private void menuItemLiveStart_Click(object sender, EventArgs e)
        {
            StartLiveVideo();
        }

        private void menuItemLiveStop_Click(object sender, EventArgs e)
        {
            StopLiveVideo();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            if (icImagingControl1.DeviceValid)
            {
                icImagingControl1.LiveStop();
            }
            this.Close();
        }

        // Call the image processing method manually from the main menu.
        private void menuItemProcess_Click(object sender, EventArgs e)
        {
            ProcessImage();
        }









        /// <summary>
        /// Perfrom image processing. This method is called from the main menu,
        /// but can also be called from a timer or a button event.
        /// </summary>
        private void ProcessImage()
        {
            TIS.Imaging.ImageBuffer ImgBuffer;
            int x, y;
            int BytesPerLine;

            Cursor = Cursors.WaitCursor;

            icImagingControl1.MemorySnapImage();


            // Stop the live video, so the processed image can be displayed.
            StopLiveVideo();


            ImgBuffer = icImagingControl1.ImageActiveBuffer;
            // Calculate the number of bytes per line.  
            BytesPerLine = ImgBuffer.BitsPerPixel / 8 * ImgBuffer.PixelPerLine;

            // Start the image processing. It is contained in an "unsafe" section,
            // because a pointer to the image data is used.
            unsafe
            {
                // TODO: Insert your own image processing here.
                byte* pDatabyte = ImgBuffer.GetImageData();
                for (y = 0; y < ImgBuffer.Lines; y++)
                {
                    for (x = 0; x < BytesPerLine; x++)
                    {
                        *pDatabyte = (byte)(255 - *pDatabyte);
                        pDatabyte++;
                    }
                }
            }


            // Display the processed image in the IC Imaging Control window.
            icImagingControl1.DisplayImageBuffer(ImgBuffer);


            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handle the DeviceLost event.
        /// </summary>
        private void DeviceLost()
        {
            MessageBox.Show("Device Lost!");
            UpdateControls();
        }

        private void icImagingControl1_DeviceLost(object sender, TIS.Imaging.ICImagingControl.DeviceLostEventArgs e)
        {
            BeginInvoke(new DeviceLostDelegate(ref DeviceLost));
        }


    }
}
