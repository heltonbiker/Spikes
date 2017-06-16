using System;
using System.Linq;
using System.Drawing;
using TIS.Imaging;
using TIS.Imaging.VCDHelpers;
using System.Windows;
using System.IO;
using System.Xml.Serialization;

namespace TesteCameraTIS
{
    /// <summary>
    /// Implementação de ICameraService utilizando a API (dll) "The Imaging Source".
    /// </summary>
    /// <remarks>
    /// Documentação em http://www.theimagingsource.com/en_US/products/oem-cameras/usb-ccd-color/
    /// </remarks>
    public class CameraServiceTIS
    {

        ICImagingControl _controle_camera;
        VCDSimpleProperty _propriedades_camera; 


        // CONSTRUTOR
        public CameraServiceTIS() {
            _controle_camera = new ICImagingControl("ISB3200208905");

            _controle_camera.Device = _controle_camera.Devices.First();

            _controle_camera.LiveCaptureContinuous = true;
            _controle_camera.LiveDisplay = false;
            _controle_camera.DeviceFrameRate = 10F; // Frame rate, o máximo no mac mini é 10F
            _controle_camera.ImageRingBufferSize = 1;


            // Desabilitando ganho e exposição automáticos
            VCDPropertyItem Gain = _controle_camera.VCDPropertyItems.FindItem(VCDIDs.VCDID_Gain);
            VCDSwitchProperty GainSwitch;
            GainSwitch = (VCDSwitchProperty)Gain.Elements.FindInterface(
                                                    TIS.Imaging.VCDIDs.VCDElement_Auto + ":" +
                                                    TIS.Imaging.VCDIDs.VCDInterface_Switch);
            GainSwitch.Switch = false;


            VCDPropertyItem Exposure = _controle_camera.VCDPropertyItems.FindItem(VCDIDs.VCDID_Gain);
            VCDSwitchProperty ExposureSwitch;
            ExposureSwitch = (VCDSwitchProperty)Exposure.Elements.FindInterface(
                                                    TIS.Imaging.VCDIDs.VCDElement_Auto + ":" +
                                                    TIS.Imaging.VCDIDs.VCDInterface_Switch);
            ExposureSwitch.Switch = false;


            _propriedades_camera = TIS.Imaging.VCDHelpers.VCDSimpleModule.GetSimplePropertyContainer(_controle_camera.VCDPropertyItems);

            
            // REFACTOR deve colocar isso em arquivo de configuração
            _propriedades_camera.RangeValue[VCDIDs.VCDID_Exposure] = -3;
            _propriedades_camera.RangeValue[VCDIDs.VCDID_Gain] = 300;
            _propriedades_camera.RangeValue[VCDIDs.VCDID_Gamma] = 60;


            _controle_camera.ImageAvailable += new EventHandler<ICImagingControl.ImageAvailableEventArgs>(NovoFrameEventHandler);
                    
        }



        public double Brilho {
            get { return Convert.ToDouble(_propriedades_camera.RangeValue[VCDIDs.VCDID_Gain]); }
            set {
                int valor = Convert.ToInt32(value);
                _propriedades_camera.RangeValue[VCDIDs.VCDID_Gain] = valor;
            }
        }

        public double Contraste {
            get { return Convert.ToDouble(_propriedades_camera.RangeValue[VCDIDs.VCDID_Gamma]); }
            set {
                int valor = Convert.ToInt32(value);
                _propriedades_camera.RangeValue[VCDIDs.VCDID_Gamma] = valor; 
            }
        }

        public double BrilhoMaximo { get { return _propriedades_camera.RangeMax(VCDIDs.VCDID_Gain); } }
        public double BrilhoMinimo { get { return _propriedades_camera.RangeMin(VCDIDs.VCDID_Gain); } }
        public double ContrasteMaximo { get { return _propriedades_camera.RangeMax(VCDIDs.VCDID_Gamma); } }
        public double ContrasteMinimo { get { return _propriedades_camera.RangeMin(VCDIDs.VCDID_Gamma); } }



        void NovoFrameEventHandler(object sender, ICImagingControl.ImageAvailableEventArgs e) {
            Bitmap bmp = e.ImageBuffer.Bitmap;
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            OnNovoFrame(this, new NovoFrameArgs(bmp));
        }



        public void Start() {
            _controle_camera.LiveStart();
        }


        public void Stop() {
            _controle_camera.LiveStop();
        }



        public bool isRunning() {
            return _controle_camera.LiveVideoRunning;
        }






        public event NovoFrameEventHandler NovoFrame;

        protected virtual void OnNovoFrame (object sender, NovoFrameArgs e) {
            if (NovoFrame != null) {
                NovoFrame(this, e);
            }
        }


    }


    public delegate void NovoFrameEventHandler(object sender, NovoFrameArgs e);



    public class NovoFrameArgs : EventArgs
    {

        public Bitmap Frame { get; private set; }

        public NovoFrameArgs(Bitmap fr)
        {
            Frame = fr;
        }

    }


}
