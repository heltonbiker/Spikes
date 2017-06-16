using System;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using TIS.Imaging;

namespace IntegracaoColetaVVM.Model
{
    
    /// <summary>
    /// Classe que representa e encapsula um hardware câmera.
    /// </summary>
    /// <remarks>
    /// O usuário desta classe não deve ter conhecimento da localização ou do tipo de câmera.
    /// Esta mesma classe (ou classes com a mesma interface) deve se comunicar potencialmente com mais de um tipo de câmera.
    /// </remarks>
    public class CameraService {

        VideoCaptureDevice _video_source;




        /// <summary>
        /// Inicia a captura de imagens da câmera.
        /// </summary>
        public void Start() {

            
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0) {
               _video_source = new VideoCaptureDevice(videoDevices[0].MonikerString);
               _video_source.NewFrame += new NewFrameEventHandler(NewFrameHandler);
               _video_source.Start();
               
               // FileVideoSource fileVideo = new FileVideoSource(videoDevices[0].MonikerString);
               // AsyncVideoSource asyncVideoSource = new AsyncVideoSource(fileVideo);
               // asyncVideoSource.NewFrame += asyncVideoSource_NewFrame;
               // asyncVideoSource.Start();

            }
        }

        /// <summary>
        /// Interrompe a captura de imagens da câmera.
        /// </summary>
        public void Stop() {
            if (_video_source != null) {
                _video_source.Stop();
            }
        }
        
        /// <summary>
        /// Informa se a câmera está funcionando ou não.
        /// </summary>
        public bool isRunning() {
            if (_video_source != null) {
                return _video_source.IsRunning;
            }
            return false;
        }

        /// <summary>
        /// Evento disparado repetidamente durante a operação Start,
        /// a cada vez que a câmera envia um novo frame.
        /// </summary>
        public event NovoFrameEventHandler NovoFrame;

        protected void OnNovoFrame(NovoFrameArgs e) {
            if (NovoFrame != null)
                NovoFrame(this, e);
        }

        private void NewFrameHandler(object sender, NewFrameEventArgs eventArgs) {

            var bmp = (Bitmap)eventArgs.Frame.Clone();
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            OnNovoFrame(new NovoFrameArgs(bmp));
            //var _ = this.
        }

    }



}
