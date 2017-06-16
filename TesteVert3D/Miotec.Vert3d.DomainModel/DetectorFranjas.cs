using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;

namespace Miotec.Vert3d.DomainModel
{
    
    /// <summary>
    /// Extrai a lista ordenada de franjas a partir de uma imagem (foto do dorso com franjas projetadas)
    /// </summary>
    public class DetectorFranjas
    {

        // Valores de entrada

        /// <summary>
        /// Representação da imagem listrada conforme coletada diretamente pela câmera (é a "foto das franjas")
        /// </summary>
        System.Drawing.Bitmap _foto_franjas_original;

        /// <summary>
        /// Índice que determina o ponto no qual será traçada uma reta vertical. A varredura em busca de franjas
        /// será feita à direita e à esquerda dessa reta, ao longo dos picos de intensidade dos pixels.
        /// </summary>
        int indice_x_inicial;


        List<List<Point>> _lista_franjas;
        int _indice_franja_principal;
        List<List<PontoDetectado>> _pontos_detectados;


        // Valores de saída

        /// <summary>
        /// Lista de franjas ordenadas de cima para baixo na imagem,
        /// onde cada franja é uma lista de pontos ordenados da esquerda para a direita,
        /// onde cada ponto tem uma coordenada X e Y, com unidade em pixels da imagem,
        /// e origem das coordenadas no canto superior direito da imagem.
        /// </summary>
        public List<List<Point>> ListaFranjas {
            get { return _lista_franjas; }
        }

        /// <summary>
        /// Índice em que se encontra a franja principal, na ListaDeFranjas.
        /// </summary>
        public int IndiceFranjaPrincipal {
            get { return _indice_franja_principal; }
        }




        // CONSTRUTOR
        public DetectorFranjas(System.Drawing.Bitmap original, int indice) {
            this._foto_franjas_original = original;
            this.indice_x_inicial = indice;
        }



        /// <summary>
        /// Realiza a extração das franjas, de forma não-interativa.
        /// </summary>
        /// <remarks>
        /// É composta pelas seguintes etapas:
        /// <list type="number">
        /// <item><description>
        /// Transformação da imagem (Bitmap) em um array de doubles (double[,]) com os valores grayscale.
        /// </description></item>
        /// <item><description>
        /// "Borramento" da imagem com dois valores diferentes, com posterior subtração de uma imagem da outra.
        /// Esse procedimento é chamado "Threshold Adaptativo", ou "Binarização Adaptativa".
        /// </description></item>
        /// <item><description>
        /// Varredura de cada coluna, usando algoritmo de "zero-crossing", para identificar os pares de índices
        /// que delimitam cada franja.
        /// </description></item>
        /// <item><description>
        /// Localização do máximo local de cada intervalo ("fatia vertical") da franja,
        /// usando interpolação parabólica de três pontos.
        /// </description></item>
        /// <item><description>
        /// A partir da reta vertical central, para cada máximo encontrado nessa reta, faz uma varredura
        /// para a esquerda e para a direita, em busca de pontos próximos, até que alguma condição
        /// de interrupção seja encontrada. Condições de interrupção:
        /// ponto pouco intenso (PontoDetectado.W menor que algum limite),
        /// distância muito grande entre os dois últimos pontos,
        /// ângulo muito agudo entre os últimos três pontos,
        /// raio do círculo inscrito muito pequeno entre os últimos três pontos.
        /// </description></item>
        /// <item><description>
        /// Identificação da franja principal, mais intensa, tomando-se a franja que apresenta a maior
        /// "soma de diferenças" ao comparar sua própria intensidade com as das franjas imediatamente
        /// acima e abaixo.
        /// </description></item>
        /// </list>
        /// </remarks>
        public void Executar() {

            double [,] array_grayscale = TransformaImagemEmArray(_foto_franjas_original);

            double[,] blur = Gaussian.GaussianConvolution(array_grayscale, 4);
            double[,] fundo = Gaussian.VerticalGaussianConvolution(blur, 6);

            double[,] array_diferenca = UmArrayMenosOutro(blur, fundo);


            _pontos_detectados = EncontraMaximosVerticais(array_diferenca);
            
            PercorreFranjas(_pontos_detectados, indice_x_inicial,
                            out _lista_franjas, out _indice_franja_principal);

        }




        // Métodos principais

        /// <summary>
        /// Transforma uma imagem do tipo System.Drawing.Bitmap em um array do tipo Double[,]
        /// </summary>
        private double[,] TransformaImagemEmArray(System.Drawing.Bitmap imagem) {
            // Transforma a imagem de entrada em um array de doubles
            // com os valores grayscale da imagem

            BitmapData bitmap_data = imagem.LockBits(new System.Drawing.Rectangle(0,0,_foto_franjas_original.Width,_foto_franjas_original.Height),
                                                     ImageLockMode.ReadOnly,
                                                     _foto_franjas_original.PixelFormat);

            int pixelsize = System.Drawing.Image.GetPixelFormatSize(bitmap_data.PixelFormat)/8;

            IntPtr pointer = bitmap_data.Scan0;
            int nbytes = bitmap_data.Height * bitmap_data.Stride;
            byte[] imagebytes = new byte[nbytes];
            System.Runtime.InteropServices.Marshal.Copy(pointer, imagebytes, 0, nbytes);

            double red;
            double green;
            double blue;
            double gray;

            var _grayscale_array = new Double[bitmap_data.Height, bitmap_data.Width];

            if (pixelsize >= 3 ) {
                for (int I = 0; I < bitmap_data.Height; I++) {
                    for (int J = 0; J < bitmap_data.Width; J++ ) {
                        int position = (I * bitmap_data.Stride) + (J * pixelsize);
                        blue = imagebytes[position];
                        green = imagebytes[position + 1];
                        red = imagebytes[position + 2];
                        gray = 0.299 * red + 0.587 * green + 0.114 * blue;
                        _grayscale_array[I,J] = gray;
                    }
                }
            }

            _foto_franjas_original.UnlockBits(bitmap_data);

            return _grayscale_array;
        }

        /// <summary>
        /// Efetua a diferença, elemento a elemento, entre os valores de dois arrays com o mesmo tamanho
        /// </summary>
        private double[,] UmArrayMenosOutro(double[,] um_array, double[,] menos_outro) {

            if ( um_array.GetLength(0) != menos_outro.GetLength(0) ||
                 um_array.GetLength(1) != menos_outro.GetLength(1) )
                throw new ArgumentException(
                    String.Format("Os arrays não têm as mesmas dimensões. um_array = ({0}x{1}). menos_outro({2}x{3})",
                                                          um_array.GetLength(0), um_array.GetLength(1),
                                                          menos_outro.GetLength(0), menos_outro.GetLength(1))
                );

            int height = um_array.GetLength(0);
            int width = um_array.GetLength(1);

            double[,] final = new double[height, width];

            for (int I = 0; I < height; I++) {
                for (int J = 0; J < width; J++ ) {
                    final[I,J] = um_array[I,J] - menos_outro[I,J];
                }
            }
            
            return final;

        }


        private enum MetodoCalculoCentroFranja {
            InterpolacaoParabolica,
            CentroDeMassa
        }


        /// <summary>
        /// Detecta os pontos médios de cada seção vertical de uma franja, ao longo das colunas de pixels da imagem.
        /// </summary>
        /// <param name="array_com_franjas">Array do tipo Double[,] contendo a imagem do dorso com franjas projetadas</param>
        /// <returns>Lista de colunas correspondentes às colunas de pixels da imagem, onde cada coluna
        /// contém os pontos detectados que são os máximos locais.</returns>
        private List<List<PontoDetectado>> EncontraMaximosVerticais(double[,] array_com_franjas) {
            
            var colunasDePontosDetectados = new List<List<PontoDetectado>>();

            var altura = array_com_franjas.GetLength(0);
            var largura = array_com_franjas.GetLength(1);
            
            // neste método, "x" é sempre inteiro e portanto igual a "j",
            // enquanto "y" é Double e portanto é análogo mais não igual a "i"
            for (var x = 0; x < largura; x++) {

                // encontra zero-crossings varrendo coluna de baixo pra cima,
                // encontrando os pares de índices dessa coluna que
                // definem intervalos positivos
                var pares_de_indices = new List<List<int>>();
                var par_de_indices = new List<int>();
                for (int y = 1; y < altura; y++) {
                    double p1 = array_com_franjas[y-1,x];
                    double p2 = array_com_franjas[y,x];
                    if ((p1 * p2) < 0) {
                        if (p1 < p2) {
                            par_de_indices.Add(y);
                        }
                        else if (par_de_indices.Count() == 1) {
                            par_de_indices.Add(y-1);
                            pares_de_indices.Add(par_de_indices);
                            par_de_indices = new List<int>();
                        }
                    }                  
                }

                // depois de encontrar os índices que delimitam intervalos positivos,
                // fazer o cálculo do "valor central" com um de vários métodos disponíveis
                var coluna_com_centros_das_franjas = new List<PontoDetectado>();

                foreach (var par in pares_de_indices) {

                    int start = par[0];
                    int stop = par[1];
                    PontoDetectado centro_vertical = new PontoDetectado();

                    var metodo = MetodoCalculoCentroFranja.InterpolacaoParabolica;

                    switch (metodo) {
                        case MetodoCalculoCentroFranja.InterpolacaoParabolica:
                            centro_vertical = CentroViaInterpolacaoParabolica(array_com_franjas, x, start, stop);
                            break;
                        case MetodoCalculoCentroFranja.CentroDeMassa:
                            centro_vertical = CentroViaCentroDeMassa(array_com_franjas, x, start, stop);
                            break;
                    }

                    coluna_com_centros_das_franjas.Add(centro_vertical);
                }
                colunasDePontosDetectados.Add(coluna_com_centros_das_franjas);
            }
            return colunasDePontosDetectados;
        }


        /// <summary>
        /// Usado quando for escolhido o método InterpolacaoParabolica, para detectar o centro
        /// (ponto "médio") vertical de uma coluna de pixels.
        /// </summary>
        /// <param name="array">Array de onde vai ser lido o ponto</param>
        /// <param name="x">Índice horizontal da coluna a ser selecionada</param>
        /// <param name="start">Índice superior da coluna a ser selecionada</param>
        /// <param name="stop">Índice inferior da coluna a ser selecionada</param>
        /// <returns>Ponto correspondente ao centro vertical da fatia da franja</returns>
        private PontoDetectado CentroViaInterpolacaoParabolica(double[,] array, int x, int start, int stop) {
            // tem que achar o índice do maior valor no intervalo
            int maiorindice = int.MinValue;
            double maiorvalor = double.MinValue;
            for (var i = start; i <= stop; i++) {
                if (array[i,x] > maiorvalor) {
                    maiorvalor = array[i,x];
                    maiorindice = i;
                }
            }

            var p1 = new Point (maiorindice-1, array[maiorindice-1,x]);
            var p2 = new Point (maiorindice, array[maiorindice,x]);
            var p3 = new Point (maiorindice+1, array[maiorindice+1,x]);

            Point pVertice = InterpolacaoParabolica(p1, p2, p3);

            return new PontoDetectado(x, pVertice.X, pVertice.Y);
        }

        /// <summary>
        /// Ver <see cref="CentroViaInterpolacaoParabolica"/>.
        /// </summary>
        private PontoDetectado CentroViaCentroDeMassa(double[,] array, int x, int start, int stop) {

            int nstart = start;
            int nstop = stop;

            //int expand = 1;
            //nstart = Math.Max(start - expand, 0);
            //nstop = Math.Min(stop + expand, array.GetLength(0)-1);

            // acha valores menores pra cima
            //while (array[nstart-1,x] < array[nstart,x] & nstart >                   1) nstart-- ;
            //while (array[nstop+1,x] < array[nstop,x]  & nstop  < array.GetLength(0)-2)  nstop++ ;

            // calcula o centro de massa da coluna (usando média aritmética ponderada)
            double pos_sum = 0;
            double val_sum = 0;
            // para cada pixel (de cima para baixo), ao longo da coluna selecionada...
            for (var i = nstart; i <= nstop; i++) {
                pos_sum += array[i,x] * i;
                val_sum += array[i,x];
            }
            return new PontoDetectado(x, pos_sum/val_sum, val_sum);
        }



        /// <summary>
        /// Percorre horizontalmente os pontos detectados, para ambos os lados,
        /// tentando formar franjas a partir de pontos de vizinhança.
        /// </summary>
        private void PercorreFranjas(List<List<PontoDetectado>> colunasPixel, int indice,
                                     out List<List<Point>> posFranjas, out int indFranjaPrincipal) {
            
            // PARÂMETROS NUMÉRICOS DO ALGORITMO
            const double PESO_MINIMO = double.MinValue; //5;
            const double DISTANCIA_VERTICAL_MINIMA = 1;
            const double ANGULO_MAXIMO = 10;
            const double RAIO_MINIMO = 10;
            const int PONTOS_MINIMOS_POR_FRANJA = 60;
            
            
            posFranjas = new List<List<Point>>();

            // "pesos" é necessário para identificar a franja principal no final da varredura
            List<double> pesos = new List<double>();

            // Seleciona a coluna média a partir da qual as franjas vão se irradiar para cada lado
            var coluna_media = colunasPixel[indice];

            // Para cada ponto da "coluna média", vai varrer para ambos os lados procurando novos pontos
            foreach (var p in coluna_media) {

                // Cria uma nova franja candidata, contendo inicialmente o próprio ponto que deu origem
                var franja = new List<Point>() {new Point(p.X, p.Y)};
                double peso_acumulado = 0;

                // Cada direção é um offset de índice: "1" aumenta para a direita, "-1" aumenta para a esquerda
                foreach (int direction in new int[] {1, -1}) {
                    int j = p.X + direction;
                    double y = p.Y;

                    // O ponto de interrogação abaixo significa que os valores numéricos podem ser nulos
                    double? A = null;
                    double? B = null;
                    double? C = null;

                    bool iniciado = false;

                    // Enquando o índice "j" estiver dentro da largura do array...
                    while (0 < j && j < colunasPixel.Count - 1) {

                        // seleciona uma nova coluna ativa
                        var coluna_ativa = colunasPixel[j];

                        double vdist = double.MaxValue;

                        // descobre qual é o pixel vizinho que está verticalmente mais perto
                        var pontoSelecionado = new PontoDetectado();
                        foreach (var pt in coluna_ativa) {
                            var distanciaVertical = Math.Abs(pt.Y - y);
                            if (distanciaVertical < vdist) {
                                vdist = distanciaVertical;
                                pontoSelecionado = pt;
                            }
                        }


                        // Nas operações abaixo, os valores do novo ponto candidato
                        // são avaliados juntamente com os valores anteriores, em busca
                        // de critérios de inclusão do ponto, ou de interrupção do crescimento da franja
                        double novoY = pontoSelecionado.Y;
                        double peso = pontoSelecionado.W;
                        double angulo = 0;
                        double raioTresPontos = double.MaxValue;

                        // A cada iteração, "troca" os valores como se fosse uma fila:
                        // "A" é o mais novo (último), "B" é o penúltimo, "C" é o antepenúltimo
                        C = B;
                        B = A;
                        A = novoY;

                        // Somente faz a operação se A, B e C não forem nulos
                        // Como os tipos são nullable (double?), então tem que usar a propriedade double?.Value
                        if (C.HasValue && B.HasValue && A.HasValue) {
                            angulo = (180.0 / Math.PI) * ( Math.Atan(A.Value-B.Value) + Math.Atan(C.Value-B.Value) );
                            raioTresPontos = RaioCirculoPassandoTresPontos(A.Value, B.Value, C.Value);
                        }

                        if (iniciado && (
                                       (peso < PESO_MINIMO) ||
                                       (vdist > DISTANCIA_VERTICAL_MINIMA) ||
                                       (angulo > ANGULO_MAXIMO) || 
                                       (raioTresPontos < RAIO_MINIMO)
                        )) break;

                        
                        // O código abaixo, até o fim do "while",
                        // só é executado caso a franja não tenha sido interrompida
                        Point pontoResultado = new Point(pontoSelecionado.X, pontoSelecionado.Y);
                        
                        if (direction == 1)
                            franja.Add(pontoResultado);
                        else if (direction == -1)
                            franja.Insert(0, pontoResultado);

                        y = novoY;
                        j += direction;
                        iniciado = true;
                        peso_acumulado += peso;
                    }                    
                }

                if (franja.Count > PONTOS_MINIMOS_POR_FRANJA) {
                    posFranjas.Add(franja);
                    pesos.Add(peso_acumulado);
                }

            }

            // Agora detecta o índice da franja principal, pegando a franja com maior "soma de diferenças"
            // entre ela própria e as que estão imediatamente acima e abaixo.
            indFranjaPrincipal = 0;
            double maxpeso = double.MinValue;
            for (int ind = 1; ind < pesos.Count - 1; ind++) {
                double pesoAcumulado = 2 * pesos[ind] - pesos[ind - 1] - pesos[ind + 1];
                if (pesoAcumulado > maxpeso) {
                    maxpeso = pesoAcumulado;
                    indFranjaPrincipal = ind;
                }
            }
        }



        // Métodos auxiliares

        /// <summary>
        /// Encontra as coordenadas do vértice da parábola que passa por três pontos,
        /// com a parábola estando na forma y = f(x) (x monotonicamente crescentes)
        /// </summary>
        /// <returns>Ponto de vértice</returns>
        private Point InterpolacaoParabolica(Point p1, Point p2, Point p3) {

            double denom = (p1.X - p2.X) * (p1.X - p3.X) * (p2.X - p3.X);
            double A = (p3.X * (p2.Y - p1.Y) + p2.X * (p1.Y - p3.Y) + p1.X * (p3.Y - p2.Y)) / denom;
            double B = (p3.X*p3.X * (p1.Y - p2.Y) + p2.X*p2.X * (p3.Y - p1.Y) + p1.X*p1.X * (p2.Y - p3.Y)) / denom;
            double C = (p2.X * p3.X * (p2.X - p3.X) * p1.Y + p3.X * p1.X * (p3.X - p1.X) * p2.Y + p1.X * p2.X * (p1.X - p2.X) * p3.Y) / denom;

            var Pv = new Point();

            Pv.X = -B / (2*A);
            Pv.Y = C - B*B / (4*A);

            return Pv;
        }

        /// <summary>
        /// Calcula o raio de um círculo que passa por três pontos.
        /// OBS: nesta função modificada, os parâmetros são apenas as coordenadas Y,
        /// pois a função fornece as coordenadas X constantes (0, 1 e 2, respectivamente).
        /// </summary>
        /// <returns>Raio, nas mesmas unidades de entrada.</returns>
        private double RaioCirculoPassandoTresPontos(double y1, double y2, double y3) {
            double x1 = 0;
            double x2 = 1;
            double x3 = 2;

            double n1 = Math.Sqrt(Math.Pow((x1-x2),2)+Math.Pow((y1-y2),2));
            double n2 = Math.Sqrt(Math.Pow((x2-x3),2)+Math.Pow((y2-y3),2));
            double n3 = Math.Sqrt(Math.Pow((x3-x1),2)+Math.Pow((y3-y1),2));
            double s = (n1+n2+n3)/2;
            double A = Math.Sqrt(s*(s-n1)*(s-n2)*(s-n3));
            double R = n1*n2*n3/(4*A);

            return R;
        }



        // Classes aninhadas

        /// <summary>
        /// Estrutura que representa as propriedades de um ponto detectado em uma <see cref="Franja"/>.
        /// Equivalente a um ponto 3D, mas tem "W" (weight = peso) no lugar de "Z",
        /// além do que a coordenada X é um inteiro (pois ela sempre será um índice de coluna de pixel)
        /// </summary>
        private struct PontoDetectado {

            public int X;
            public double Y, W;

            public PontoDetectado(int x, double y, double w) {
                this.X = x;
                this.Y = y;
                this.W = w;
            }

            public override string ToString()
            {
                return string.Format("{0}, {1}, {2}", this.X, this.Y,  this.W);
            }
        }

        /// <summary>
        /// Faz operações de "Gaussian Blur" em arrays de doubles.
        /// (fonte: http://haishibai.blogspot.com.br/2009/09/image-processing-c-tutorial-4-gaussian.html)
        /// </summary>
        class Gaussian {

            /// <summary>
            /// Faz convolução gaussiana bidimensional.
            /// </summary>
            /// <param name="matrix">Matriz onde aplica a convolução.</param>
            /// <param name="deviation">Amplitude do desvio padrão ("sigma") do kernel gaussiano.</param>
            public static double[,] GaussianConvolution(double[,] matrix, double deviation)
            {
                double[,] kernel = CalculateNormalized1DSampleKernel(deviation);
                double[,] res1 = new double[matrix.GetLength(0), matrix.GetLength(1)];
                double[,] res2 = new double[matrix.GetLength(0), matrix.GetLength(1)];
                //x-direction
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        res1[i, j] = processPoint(matrix, i, j, kernel, 0);
                }
                //y-direction
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        res2[i, j] = processPoint(res1, i, j, kernel, 1);
                }
                return res2;
            }

            /// <summary>
            /// Faz convolução gaussiana somente ao longo da dimensão vertical.
            /// </summary>
            /// <remarks>
            /// Como no Vert3D as franjas são horizontais, uma das convoluções funciona de forma
            /// mais correta quando aplicada somente ao longo da vertical (para suavizar as franjas
            /// sem "levar informação" para os lados, o que contaminaria a detecção.
            /// </remarks>
            public static double[,] VerticalGaussianConvolution(double[,] matrix, double deviation) {
                double[,] kernel = CalculateNormalized1DSampleKernel(deviation);
                double[,] res = new double[matrix.GetLength(0), matrix.GetLength(1)];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        res[i, j] = processPoint(matrix, i, j, kernel, 0);
                }
            
                return res;
            }

            private static double processPoint(double[,] matrix, int x, int y, double[,] kernel, int direction)
            {
                double res = 0;
                int half = kernel.GetLength(0) / 2;
                for (int i = 0; i < kernel.GetLength(0); i++)
                {
                    int cox = direction == 0 ? x + i - half : x;
                    int coy = direction == 1 ? y + i - half : y;
                    if (cox >= 0 && cox < matrix.GetLength(0) && coy >= 0 && coy < matrix.GetLength(1))
                    {
                        res += matrix[cox, coy] * kernel[i, 0];
                    }
                }
                return res;
            }
   
            private static double[,] CalculateNormalized1DSampleKernel(double deviation) {
                return NormalizeMatrix(Calculate1DSampleKernel(deviation));
            }

            private static double[,] Calculate1DSampleKernel(double deviation) {
                int size = (int)Math.Ceiling(deviation * 3) * 2 + 1;
                return Calculate1DSampleKernel(deviation, size);
            }

            private static double[,] Calculate1DSampleKernel(double deviation, int size)
            {
                double[,] ret = new double[size, 1];
                double sum = 0;
                int half = size / 2;
                for (int i = 0; i < size; i++)
                {
                        ret[i, 0] = 1 / (Math.Sqrt(2 * Math.PI) * deviation) * Math.Exp(-(i - half) * (i - half) / (2 * deviation * deviation));
                        sum += ret[i, 0];
                }
                return ret;
            }
   
            private static double[,] NormalizeMatrix(double[,] matrix)
            {
                double[,] ret = new double[matrix.GetLength(0), matrix.GetLength(1)];
                double sum = 0;
                for (int i = 0; i < ret.GetLength(0); i++)
                {
                    for (int j = 0; j < ret.GetLength(1); j++)
                        sum += matrix[i,j];
                }
                if (sum != 0)
                {
                    for (int i = 0; i < ret.GetLength(0); i++)
                    {
                        for (int j = 0; j < ret.GetLength(1); j++)
                            ret[i, j] = matrix[i,j] / sum;
                    }
                }
                return ret;
            } 
    
        }


    }

}


