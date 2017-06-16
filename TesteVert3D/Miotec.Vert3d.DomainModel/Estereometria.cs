using System.Drawing;
using System.Windows.Media.Media3D;
using System;
using Miotec.Vert3d.DomainModel;

namespace Miotec.Vert3d.DomainModel
{
    
    /// <summary>
    /// Coordena o processamento numérico de transformação de
    /// uma imagem bidimensional em uma nuvem de pontos,
    /// unindo as informações da imagem e dos parâmetros de calibração.
    /// Após o processamento, mantém os resultados internamente na forma de
    /// propriedades públicas (Franjas, Malha e Simetria).
    /// </summary>

    public class Estereometria
    {
        
        // Entradas
        readonly Bitmap _foto_franjas;
        readonly Projecao _projecao;
        readonly Calibracao _calibracao;
        Marcadores _marcadores;

        /// <summary>
        /// Objeto usado na tarefa de processar franjas
        /// </summary>
        internal EstruturaFranjas Franjas { get; private set; }

        /// <summary>
        /// Objeto usado para conter as informações da malha e suas propriedades intrínsecas.
        /// </summary>
        public Malha Malha { get; private set; }

        // REFACTOR: o objeto estereometria não precisa ter uma propriedade pública
        // do tipo "Simetria", mas sim uma do "tipo" "Linha de Simetria".
        /// <summary>
        /// Objeto usado para calcular e representar as propriedades de simetria da Malha.
        /// </summary>
        public ModeloSimetria Simetria { get; private set; }


        // CONSTRUTOR
        public Estereometria(Bitmap im, Projecao pr, Calibracao calib, Marcadores marc) {

            this._foto_franjas = im;
            this._projecao = pr;
            this._calibracao = calib;
            this._marcadores = marc;
        }




        /// <summary>
        /// Executa o processamento estereométrico.
        /// </summary>
        public void Executar() {
            Logger.Initialize("../../../Miotec.Vert3d.DomainModel/App.config");

            Logger.Log(LoggingLevel.Info, "Iniciando ProcessarFranjas");
            ProcessarFranjas();
            Logger.Log(LoggingLevel.Info, "Finalizando ProcessarFranjas");

            Logger.Log(LoggingLevel.Info, "Iniciando ProcessarMalha");
            ProcessarMalha();
            Logger.Log(LoggingLevel.Info, "Finalizando ProcessarFranjas");

            //ProcessarSimetria();

        }




        /// <summary>
        /// Utiliza um objeto <see cref="EstruturaFranjas"/> para criar a nuvem de pontos.
        /// </summary>
        private void ProcessarFranjas() { 
            Franjas = new EstruturaFranjas(_foto_franjas,
                                            _projecao.Angulos,
                                            _projecao.IndiceFranjaPrincipal,
                                            _calibracao, _marcadores);
            Logger.Log(LoggingLevel.Debug, "Iniciando Franjas.Executar");
            Franjas.Executar();
            Logger.Log(LoggingLevel.Debug, "Finalizando Franjas.Executar");

        }

        /// <summary>
        /// Utiliza um <see cref="Interpolador"/> para gerar uma <see cref="Malha"/> a partir da
        /// nuvem de pontos disponibilizada por <see cref="Franjas"/>.
        /// </summary>
        private void ProcessarMalha() {
 
            Point3DCollection _nuvem = Franjas.getNuvem();

            Malha = new Malha(_nuvem,
                               _marcadores);
            Logger.Log(LoggingLevel.Debug, "Iniciando Malha.Construir()");
            Malha.Construir();
            Logger.Log(LoggingLevel.Debug, "Finalizando Malha.Construir()");

        }

        /// <summary>
        /// Utiliza um objeto <see cref="Simetria"/> para
        /// detectar e representar as propriedades de simetria da <see cref="Malha"/>.
        /// </summary>
        private void ProcessarSimetria() {        
            Simetria = new ModeloSimetria(Malha);
            Simetria.Executar();
        }

    }
}
