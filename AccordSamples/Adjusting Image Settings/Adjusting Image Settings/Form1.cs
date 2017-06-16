using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TIS.Imaging;
using TIS.Imaging.VCDHelpers;
namespace Adjusting_Image_Settings
{
    public partial class Form1 : Form
    {

				private VCDSimpleProperty VCDProp;
		
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form1_Load
        ///
        /// If no video capture device was selected in the properties window of IC
        /// Imaging Control, the device settings dialog of IC Imaging Control is shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		        private void Form1_Load(object sender, EventArgs e)
        {
            if (!icImagingControl1.DeviceValid)
            {
                icImagingControl1.ShowDeviceSettingsDialog();

                if (!icImagingControl1.DeviceValid)
                {
                    MessageBox.Show("No device was selected.", "Adjusting Image Setting",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }

            if (icImagingControl1.DeviceValid == true)
            {
                icImagingControl1.LiveStart();

                VCDProp = VCDSimpleModule.GetSimplePropertyContainer(icImagingControl1.VCDPropertyItems);

                //  Setup the range of the brightness slider.
                trackBar1.Minimum = VCDProp.RangeMin(VCDIDs.VCDID_Brightness);
                trackBar1.Maximum = VCDProp.RangeMax(VCDIDs.VCDID_Brightness);

                //  Set the slider to the current brightness value.
                trackBar1.Value = VCDProp.RangeValue[VCDIDs.VCDID_Brightness];
            }
        }
				        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            VCDProp.RangeValue[VCDIDs.VCDID_Brightness] = trackBar1.Value;
        }
		
    }
}