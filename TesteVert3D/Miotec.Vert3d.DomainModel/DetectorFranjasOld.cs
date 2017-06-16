using System;
using System.Linq;
using System.Windows.Media.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Collections.Generic;
using System.Windows;
using System.Drawing.Imaging;
using System.IO;

namespace Miotec.Vert3d.DomainModel
{
    
    /// <summary>
    /// Utiliza processamento de imagem para detectar as franjas estereométricas em uma imagem.
    /// </summary>
    public class DetectorFranjasOld {

        /// <summary>
        /// Representação da imagem listrada conforme coletada diretamente pela câmera (é a "foto das franjas")
        /// </summary>
        public System.Drawing.Bitmap Original;

        /// <summary>
        /// Representação da imagem com franjas, já binarizada e condicionada previamente (é a "imagem em zebra")
        /// </summary>
        public System.Drawing.Bitmap Binarizada;

        private int _coordenada_x_central;

        
        int _indiceFranjaPrincipal;
        public int IndiceFranjaPrincipal { get { return _indiceFranjaPrincipal; } }

        List<List<Point>> _posicoesFranjas;
        public List<List<Point>> PosicoesFranjas { get { return _posicoesFranjas; } }


        public DetectorFranjasOld(System.Drawing.Bitmap original, System.Drawing.Bitmap binarizada, int coordenada_x_central) {
            this.Original = original;
            this.Binarizada = binarizada;
            this._coordenada_x_central = coordenada_x_central;
        }





        UnmanagedImage _binarizada;
        UnmanagedImage _blur_grayscale;

        Dictionary<int, UnmanagedImage> _imagens_franja;
        Dictionary<int, System.Drawing.Rectangle> _retangulos_franja;

        List<UnmanagedImage> _franjas_ordenadas;
        List<int> _indices_dos_blobs;

        //List<List<Point>> _franjas_suavizadas;
        //List<double> _alturas_franja;

        //int _indice_franja_principal;


        /// <summary>
        /// Função que coordena o cálculo das linhas médias das franjas a partir das imagens de entrada
        /// <see cref="_original"/> e <see cref="_binarizada"/>.
        /// </summary>
        /// <remarks>
        /// Identifica regiões compostas por pixels contíguos que definem uma franja na imagem.
        /// Em Processamento de Imagem, o procedimento equivalente é chamado <i>image labeling</i> ou <i>blob detection</i>.
        /// </remarks>
        public void Executar() {

            // primeiro gera uma UnmanagedImage a partir do BitmapSource "_binarizada"
            InicializaOriginal();
            InicializaBinarizada();


            Console.WriteLine("detectando blobs");
            DetectarBlobs();
            

            Console.WriteLine("ordenando franjas");
            OrdenarFranjas();


            //Console.WriteLine("varrendo linhas médias");
            //VarrerLinhasMedias();
            

            //Console.WriteLine("identificando franja principal");
            //IdentificarFranjaPrincipal();


            //this._posicoesFranjas = _franjas_suavizadas;
            //this._indiceFranjaPrincipal = _indice_franja_principal;

        }


        # region Rotinas de Processamento

        private void DetectarBlobs() {
            
            
            // Detecta blobs na imagem threshold e seleciona os que são franjas (são "cortados" pela linha vertical de referência)
            var blob_counter = new BlobCounter();
            blob_counter.ProcessImage(_binarizada);
            var blobs = blob_counter.GetObjects(_binarizada, true);
            
            // No dicionário "imagens_franja", cada par vai ser ( (índice do "blobs") , (imagem_franja) )
            _imagens_franja = new Dictionary<int, UnmanagedImage>();
            _retangulos_franja = new Dictionary<int, System.Drawing.Rectangle>();

            Blob blob;
            // para cada blob, vê se é franja, salvando (índice, imagem_franja) no dicionário
            for (int blobindex = 0; blobindex < blobs.Length; blobindex++) {
                blob = blobs[blobindex];
                if ( (blob.Rectangle.X < _coordenada_x_central) &
                     ( (blob.Rectangle.X + blob.Rectangle.Width) > _coordenada_x_central) ) {
                    blob_counter.ExtractBlobsImage(_binarizada, blob, true);  // o parâmetro true significa que extrai a imagem inteira
                    //UnmanagedImage imagem_franja = blob.Image;
                    _imagens_franja[blobindex] = blob.Image;
                    int margem_retangulo = 9;
                    _retangulos_franja[blobindex] = System.Drawing.Rectangle.Inflate(blob.Rectangle, margem_retangulo, margem_retangulo);
                }
            }

        }     



        /// <summary>
        /// Ordena verticalmente as franjas detectadas na imagem.
        /// </summary>
        /// <remarks>
        /// Tipicamente, os procedimentos de detecção de blobs não fornecem resultados com uma ordem definida.
        /// Este procedimento serve para garantir que as franjas estejam estruturadas na sequência correta.
        /// </remarks>
        private void OrdenarFranjas() {

            // Vai varrendo a linha central da imagem de cima para baixo, em busca de blobs...
            _franjas_ordenadas = new List<UnmanagedImage>();
            _indices_dos_blobs = new List<int>();
            int counter = 0;
            UnmanagedImage _imagem_uma_franja;
            UnmanagedImage _franja_dilatada;
            for (int y = 0; y < _binarizada.Height; y++) {
                var pixel = _binarizada.GetPixel(_coordenada_x_central, y);
                var pixelvalue = (pixel.GetBrightness()*255);  // pega o componente R mas poderia ser G ou B, porque são iguais (grayscale)
                // Quando encontra um pixel branco...
                if (pixelvalue > 128) {
                    // procura em todas as imagens_franja pra ver "em qual delas o pixel está"...
                    foreach (int k in _imagens_franja.Keys) {
                        _imagem_uma_franja = _imagens_franja[k];
                        // quando encontra...
                        if (_imagem_uma_franja.GetPixel(_coordenada_x_central, y).R > 128) {
                            // adiciona o índice na lista (vai ser usado para referenciar em "imagens_franja")
                            if (!(_indices_dos_blobs.Contains(k))) {
                                
                                /// REFACTOR!!!!! INEFICIENTE!!!!!
                                /// ISSO AQUI FICARÁ MUITO MELHOR SE AO INVÉS DE USAR "ExtractBlobImage" COM O TAMANHO INTEIRO,
                                /// EXTRAIR SOMENTE A ÁREA COMPREENDIDA NO RETÂNGULO, E CONSIDERAR O OFFSET NOS CÁLCULOS POSTERIORES
                                _franja_dilatada = dilate_image(_imagem_uma_franja);
                                _indices_dos_blobs.Add(k);
                                _franjas_ordenadas.Add(_franja_dilatada);
                                
                                _franja_dilatada.ToManagedImage().Save(string.Format("../../franjas_ordenadas/ord{0:D3}.png", counter));

                            }
                            counter++;
                        }                            
                    }
                }
            }
        }


        /*


        /// <summary>
        /// Detecta a sequência de pontos que define a linha média de cada franja identificada em <see cref="DetectarFranjas"/>.
        /// </summary>
        private void VarrerLinhasMedias() {

            _franjas_suavizadas = new List<List<Point>>();
            _alturas_franja = new List<double>();
            
            // para cada franja...
            for (int bi = 0; bi < _franjas_ordenadas.Count; bi++) {
                var franja = _franjas_ordenadas[bi];

                var mascara_franja = new ApplyMask(franja);
                System.Drawing.Bitmap franja_grayscale = mascara_franja.Apply(_blur_grayscale);

                franja_grayscale.Save(string.Format("../../franjas_gray/franjagray{0:D3}.png",bi));

                var franja_suavizada = new List<Point>();
                var rect = _retangulos_franja[_indices_dos_blobs[bi]];
                int número_de_colunas = 0;
                double column_height_sum = 0;
                bool column_found = false;
                // para cada coluna vertical da imagem da franja...
                int margem = 7;
                for (int col = rect.Left+margem; col < (rect.Left + rect.Width - margem); col++) {
                    int column_height = 0;
                    // selecionar a fatia vertical com índice col e fazer média aritmética ponderada
                    // para calcular o centro de massa, salvando o resultado como (x=col; y=média)
                    var column_selection_area = new System.Drawing.Rectangle(col, 0, 1, franja_grayscale.Height);
                    var crop_filter = new Crop(column_selection_area);
                    System.Drawing.Bitmap selected_column = crop_filter.Apply(franja_grayscale);

                    //selected_column.ToManagedImage().Save(string.Format("../../col{0:D3}-{1:D3}.png",bi,col));

                    // calcula o centro de massa da coluna (usando média aritmética ponderada),
                    // e adiciona o resultado à lista "franja_codificada"
                    double val_sum = 0;
                    double pos_sum = 0;
                    // para cada pixel (de cima para baixo), ao longo da coluna selecionada...
                    for (int row = rect.Top; row < (rect.Top + rect.Height); row++) {
                        double pix_val = selected_column.GetPixel(0, row).R;
                        if (pix_val > 0) {
                            val_sum += pix_val;
                            pos_sum += pix_val * row;
                            column_height += 1;
                            if (!column_found) {
                                column_found = true;
                                número_de_colunas += 1;
                            }
                        }
                    } // termina processamento da coluna
                    if (val_sum > 0) {
                        //Console.WriteLine(String.Format("{0}, {1}", col, pos_mean));
                        franja_suavizada.Add(new Point(col, (pos_sum/val_sum)));
                        column_height_sum += column_height;
                    }
                } // termina processamento da franja

                double fringe_height_mean = column_height_sum/número_de_colunas;
                
                _franjas_suavizadas.Add(franja_suavizada);
                _alturas_franja.Add(fringe_height_mean);
            }
        }


        /// <summary>
        /// Determina qual das franjas é a franja principal (mais grossa).
        /// </summary>
        /// <remarks>
        /// A técnica de luz estruturada com franjas requer que se saiba o ângulo original de cada franja projetada.
        /// Esta rotina tem a função de identificar a chamada Franja Principal,
        /// que se situa no meio da projeção e tem um ângulo de projeção conhecido.
        /// </remarks>
        private void IdentificarFranjaPrincipal() {
            
            var alturas_filtradas = new List<double>();
            for (int i = 1; i < (_alturas_franja.Count - 2); i++) {
                alturas_filtradas.Add(2 * _alturas_franja[i] - _alturas_franja[i-1] - _alturas_franja[i+1]);
            }
            _indice_franja_principal = alturas_filtradas.IndexOf(alturas_filtradas.Max());
        }

        */


        # endregion



        # region Rotinas Auxiliares

        private void InicializaOriginal() {

            //var originalbmp = UnmanagedImage.FromManagedImage(Original);

            var franjas_rgb = AForge.Imaging.Image.FromFile("../../original.png");

            var _grayscale_filter = new Grayscale(0.2125, 0.7154, 0.0721);
            var _franjas_grayscale_8bpp = _grayscale_filter.Apply(franjas_rgb);
            var _franjas_grayscale_16bpp = AForge.Imaging.Image.Convert8bppTo16bpp(_franjas_grayscale_8bpp);
            
            var blur_filter = new Blur();
            blur_filter.ApplyInPlace(_franjas_grayscale_16bpp);
            blur_filter.ApplyInPlace(_franjas_grayscale_16bpp);

            _franjas_grayscale_8bpp = AForge.Imaging.Image.Convert16bppTo8bpp(_franjas_grayscale_16bpp);


            //_franjas_grayscale_8bpp.Save("temp.png");
            //ms.Seek(0, SeekOrigin.Begin);

            _blur_grayscale = UnmanagedImage.FromManagedImage(_franjas_grayscale_8bpp);
            //bmp.BeginInit();
            //bmp.StreamSource = ms;
            //bmp.CacheOption = BitmapCacheOption.OnLoad;
            //bmp.EndInit();

            //_blur_grayscale = UnmanagedImage.FromManagedImage(bmp);



            //_blur_grayscale = UnmanagedImage.FromManagedImage(_franjas_grayscale_8bpp);
            //UnmanagedImage blurgray = _franjas_grayscale_8bpp.

            

        }

        private void InicializaBinarizada() {

            _binarizada = UnmanagedImage.FromManagedImage(Binarizada);

        }

        /*
        //System.Drawing.Bitmap BitmapFromBitmapSource (BitmapSource source, PixelFormat pixelformat) {
        //    var bmp = new System.Drawing.Bitmap(
        //                    source.PixelWidth,
        //                    source.PixelHeight,
        //                    pixelformat);

        //    BitmapData data = bmp.LockBits(
        //                        new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
        //                        ImageLockMode.WriteOnly,
        //                        pixelformat);

        //    source.CopyPixels(
        //        Int32Rect.Empty,
        //        data.Scan0,
        //        data.Height * data.Stride,
        //        data.Stride);
        //    bmp.UnlockBits(data);

        //    return bmp;
        //}


        //private UnmanagedImage ConvertToBlurredGrayscale (BitmapSource source) {
        //    var bmp = BitmapFromBitmapSource(source, PixelFormat.Format8bppIndexed);

        //    var _grayscale_filter = new Grayscale(0.2125, 0.7154, 0.0721);
        //    var _franjas_grayscale_8bpp = _grayscale_filter.Apply(bmp);
        //    var _franjas_grayscale_16bpp = AForge.Imaging.Image.Convert8bppTo16bpp(_franjas_grayscale_8bpp);
            
        //    var blur_filter = new Blur();
        //    blur_filter.ApplyInPlace(_franjas_grayscale_16bpp);
        //    blur_filter.ApplyInPlace(_franjas_grayscale_16bpp);
        //    _franjas_grayscale_8bpp = AForge.Imaging.Image.Convert16bppTo8bpp(_franjas_grayscale_16bpp);
            
        //    return UnmanagedImage.FromManagedImage(_franjas_grayscale_8bpp);            
        //}
        */

        private UnmanagedImage dilate_image (UnmanagedImage input_image) {
            UnmanagedImage dilated_image;
            bool custom_dilation = true;
            if (custom_dilation) {
                var custom_dilatation_filter = new Dilatation(new short[,] {{ -1,  1,  1,  1, -1 },
                                                                            { -1,  1,  1,  1, -1 },
                                                                            { -1,  1,  1,  1, -1 },
                                                                            { -1,  1,  1,  1, -1 },
                                                                            { -1,  1,  1,  1, -1 }});

                Console.WriteLine(input_image.PixelFormat);

                dilated_image = custom_dilatation_filter.Apply(input_image);
            }
            else {
                var dilation_filter = new Dilatation();           
                dilated_image = dilation_filter.Apply(input_image);
            }
            return dilated_image;
        }  // END dilate_image()


        # endregion


    }
}
