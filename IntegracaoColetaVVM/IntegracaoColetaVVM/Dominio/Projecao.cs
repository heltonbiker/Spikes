using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace IntegracaoColetaVVM.Dominio
{
    public class Projecao
    {
        
        readonly int LARGURA_PROJECAO_PIXELS;
        readonly int ALTURA_PROJECAO_PIXELS;

        int ESPESSURA_FRANJA_PRINCIPAL_PIXELS = 6;
        int ESPESSURA_FRANJA_ADICIONAL_PIXELS = 3;
        int ESPESSURA_INTERVALO_PIXELS = 11;        
        

        /// <summary>
        /// Índice onde se encontra a franja principal, na lista retornada por <see cref="Posicoes"/>
        /// </summary>
        public int IndiceFranjaPrincipal {
            get { return _indice_franja_principal; }
        }
        int _indice_franja_principal;

        /// <summary>
        /// Retorna uma lista com as posições normalizadas das franjas, incluindo a franja principal;
        /// </summary>
        public List<Double> Posicoes {
            get { return _posicoes; }
        }
        List<Double> _posicoes;



        // CONSTRUTOR
        public Projecao(int largura, int altura) {
            LARGURA_PROJECAO_PIXELS = largura;
            ALTURA_PROJECAO_PIXELS = altura;
        }



        /// <summary>
        /// Gera uma imagem do padrão de projeção com listras
        /// </summary>
        public BitmapImage getImagem(double posicao_superior_norm, double posicao_inferior_norm) {

            // Calcula franjas
            double posicao_principal_norm = (posicao_superior_norm + posicao_inferior_norm) / 2;
            double posicao_principal_float = ALTURA_PROJECAO_PIXELS * posicao_principal_norm;

            double meia_franja = ESPESSURA_FRANJA_PRINCIPAL_PIXELS * 0.5;
            double meia_franjinha = ESPESSURA_FRANJA_ADICIONAL_PIXELS * 0.5;

            double posicao_principal_pix = ALTURA_PROJECAO_PIXELS - Math.Round(posicao_principal_float);
            if (meia_franja % 1 == 0.5) posicao_principal_pix += 0.5;

            double deslocamento_inicial = meia_franja + ESPESSURA_INTERVALO_PIXELS + meia_franjinha;
            double deslocamento_adicional = ESPESSURA_INTERVALO_PIXELS + ESPESSURA_FRANJA_ADICIONAL_PIXELS;

            // formulinha para número de franjas: até funciona, mas o cálculo não está definindo bem o fenômeno
            double numerador = (((posicao_superior_norm - posicao_inferior_norm) * ALTURA_PROJECAO_PIXELS * 0.5) - 2 * ESPESSURA_FRANJA_PRINCIPAL_PIXELS);
            double numero_franjas_double = numerador / deslocamento_adicional;
            int num_franjas = (int)Math.Round(numero_franjas_double);

            double posicao_principal_final = posicao_principal_pix / ALTURA_PROJECAO_PIXELS;


            using (var bmp = new Bitmap(LARGURA_PROJECAO_PIXELS, ALTURA_PROJECAO_PIXELS)) {
                // Desenha franjas
                using (var g = Graphics.FromImage(bmp)) {

                    g.Clear(Color.Black);
                    g.TranslateTransform((float)0, (float)posicao_principal_pix);
                        
                    // desenha franja principal
                    g.FillRectangle(Brushes.White,
                                    0, (float)-meia_franja,
                                    (float)LARGURA_PROJECAO_PIXELS, (float)ESPESSURA_FRANJA_PRINCIPAL_PIXELS);

                    // desenha outras franjas
                    var dirs = new int[] {-1,1};
                    foreach (int dir in dirs) {
                        for (var i = 0; i < num_franjas; i++) {
                            var posicao = (deslocamento_inicial + i * deslocamento_adicional) * dir;
                            g.FillRectangle(Brushes.White,
                                            0, (float)(posicao - meia_franjinha),
                                            (float)LARGURA_PROJECAO_PIXELS, (float)ESPESSURA_FRANJA_ADICIONAL_PIXELS);
                        }
                    }
                }


                // Cria imagem propriamente dita
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

                    return bitmapImage;
                }
            }  // end using bmp


        }
        
    }
}
