using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miotec.Vert3d.Faturamento
{
    public struct ColetasFeitas
    {
        public String Nome { get{ return nome;  } set{ Nome = nome;} }
        public bool StatusExame { get{return status;} set {StatusExame = status;} }
        public int DataColeta { get{return data;} set { DataColeta = data ;} }

        String nome;
        bool status;
        int data;

        public ColetasFeitas(String nome, bool status, int data)
        {
            this.status = status;
            this.data = data;
            this.nome = nome;
        }
    }
}
