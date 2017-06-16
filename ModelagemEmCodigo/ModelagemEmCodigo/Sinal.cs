using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelagemEmCodigo
{
    public class Sinal
    {

        List<short> _valores;

        public readonly CanalInfo canalInfo;


        public IEnumerable<double> Valores
        {
            get
            {
                var A = canalInfo.A;
                var B = canalInfo.B;
                return _valores.Select(x => A*x + B);
            }
        }

    }
}
