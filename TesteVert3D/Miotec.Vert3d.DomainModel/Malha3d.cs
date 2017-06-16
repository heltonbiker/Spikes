using System;



namespace Miotec.Vert3d.DomainModel
{
    public class Malha3d
    {

        # region Campos

        Double [,] _matrizRelevo;
        Double _espacamento;
        Double _xmin;
        Double _ymin;
        //Int32 _linhas;
        //Int32 _colunas;
        
        # endregion



        # region Construtor

        public Malha3d (Double [,] matriz, Double espacamento, Double xmin, Double ymin) {
            this._matrizRelevo = matriz;
            this._espacamento = espacamento;
            this._xmin = xmin;
            this._ymin = ymin;
        }

        # endregion



        # region Propriedades

        public Double [,] MatrizRelevo {
            get { return _matrizRelevo; }
            set { _matrizRelevo = value; }
        }
        
        public Double [,] MatrizCurvatura {
            get { return this._getCurvatura(); }
        }

        public Double Espacamento {
            get { return _espacamento; }
        }

        public Double Xmin {
            get { return _xmin; }
            private set { _xmin = value; }
        }

        public Double Ymin {
            get { return _ymin; }
            private set { _ymin = value; }
        }



        # endregion



        # region Métodos

        private double[,] _getCurvatura()
        {
            throw new NotImplementedException();
        }

        # endregion

    }
}
