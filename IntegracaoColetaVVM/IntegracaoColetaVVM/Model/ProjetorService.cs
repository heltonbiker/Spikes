using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;
using IntegracaoColetaVVM.Dominio;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace IntegracaoColetaVVM.Model
{
    public class ProjetorService : INotifyPropertyChanged {

 	    Window janelaprojecao;
        Projecao _projecao;


        // CONSTRUTOR
        public ProjetorService() {
            _projecao = new Projecao(800,600);
        }

        public double MinValue { 
            get {return _minvalue; }
            set { 
                double resultado = value;
                if (resultado > 1) resultado = 1;
                if (resultado < 0) resultado = 0;
                _minvalue = resultado;
                RaisePropertyChanged("ImagemProjecao");
            }
        }
        double _minvalue = 0;

        public double MaxValue {
            get {return _maxvalue; }
            set { 
                double resultado = value;
                if (resultado > 1) resultado = 1;
                if (resultado < 0) resultado = 0;
                _maxvalue = resultado;
                RaisePropertyChanged("ImagemProjecao");
            }
        }
        double _maxvalue = 1;


        public BitmapImage ImagemProjecao {
            get {
                var resultado = new BitmapImage();
                
                switch (_projetada) {
                    case TipoDeImagem.Franjas:
                        resultado = _projecao.getImagem(MaxValue, MinValue);
                        break;
                    case TipoDeImagem.Branca:
                        using (var bmp = new Bitmap(800, 600)) {
                            using (var g = Graphics.FromImage(bmp)) {
                                g.Clear(Color.Linen);
                            }
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bmp.Save(ms, ImageFormat.Bmp);
                                ms.Position = 0;
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.StreamSource = ms;
                                bitmapImage.EndInit();

                                bitmapImage.Freeze();

                                resultado = bitmapImage;
                            }
                        }
                        break;
                }
                return resultado;
            }
        }
        TipoDeImagem _projetada = TipoDeImagem.Franjas;


        public void  Start() {
 	        janelaprojecao = new JanelaProjecao();
            janelaprojecao.DataContext = this;
            this.ProjetaFranja();
            janelaprojecao.Show();
        }

        public void  Stop() {
 	        if (janelaprojecao != null)
                janelaprojecao.Close();
        }

        public bool IsRunning() {
            if (janelaprojecao != null &&
                janelaprojecao.IsVisible)
                return true;
            else return false;
        }

        public void ProjetaFranja() {
            _projetada = TipoDeImagem.Franjas;
            RaisePropertyChanged("ImagemProjecao");
        }

        public void ProjetaBranca() {
            _projetada = TipoDeImagem.Branca;
            RaisePropertyChanged("ImagemProjecao");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        enum TipoDeImagem {
            Branca,
            Franjas
        }

    }
}
