using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace MinimalUpdateableDrawing
{
    class ProjectionPattern : INotifyPropertyChanged {
        
        double _min_position = 0;
        double _max_position = 1;

        public double MinPosition {
            get { return _min_position; }
            set {
                _min_position = value;
                OnPropertyChanged("MinPosition");
                OnPropertyChanged("ImageSource");
            }
        }

        public double MaxPosition {
            get { return _max_position; }
            set {
                _max_position = value;
                OnPropertyChanged("MaxPosition");
                OnPropertyChanged("ImageSource");
            }
        }

        public BitmapImage ImageSource {
            get {
                int h = 600;
                int w = 800;
                int espessura_principal_pix = 6;
                int espessura_adicional_pix = 3;
                int intervalo_pix = 11;

                double posicao_superior_norm = MaxPosition;
                double posicao_inferior_norm = MinPosition;

                double posicao_principal_norm = (posicao_superior_norm + posicao_inferior_norm) / 2;
                double posicao_principal_float = h * posicao_principal_norm;

                double meia_franja = espessura_principal_pix * 0.5;
                double meia_franjinha = espessura_adicional_pix * 0.5;

                double posicao_principal_pix = h - Math.Round(posicao_principal_float);
                if (meia_franja % 1 == 0.5) posicao_principal_pix += 0.5;

                double deslocamento_inicial = meia_franja + intervalo_pix + meia_franjinha;
                double deslocamento_adicional = espessura_adicional_pix + intervalo_pix;

                // formulinha para número de franjas: até funciona, mas o cálculo não está definindo bem o fenômeno
                int num_franjas = (int)Math.Round( (((posicao_superior_norm - posicao_inferior_norm) * h * 0.5) - 2 * espessura_principal_pix) / deslocamento_adicional);

                double posicao_principal_final = posicao_principal_pix / h;




                using (var bmp = new Bitmap(w, h)) {
                    using (var g = Graphics.FromImage(bmp)) {

                        g.Clear(Color.Black);
                        g.TranslateTransform((float)0, (float)posicao_principal_pix);
                        
                        // desenha franja principal
                        g.FillRectangle(Brushes.White,
                                        0, (float)-meia_franja,
                                        (float)w, (float)espessura_principal_pix);

                        // desenha outras franjas
                        var dirs = new int[] {-1,1};
                        foreach (int dir in dirs) {
                            for (var i = 0; i < num_franjas; i++) {
                                var posicao = (deslocamento_inicial + i * deslocamento_adicional) * dir;
                                g.FillRectangle(Brushes.White,
                                                0, (float)(posicao - meia_franjinha),
                                                (float)w, (float)espessura_adicional_pix);
                            }
                        }
                    }

                    using(MemoryStream ms = new MemoryStream()) {
                        bmp.Save(ms, ImageFormat.Bmp);
                        ms.Position = 0;
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = ms;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        return bitmapImage;
                    }
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property_name) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }

    }
}
