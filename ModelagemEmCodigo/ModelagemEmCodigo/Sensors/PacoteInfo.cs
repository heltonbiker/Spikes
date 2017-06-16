using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelagemEmCodigo
{
    public class PacoteInfo
    {
        //public int TamanhoEmBytes { get; set; }

        public IEnumerable<CanalInfo> Canais { get; set; }
    }
}
