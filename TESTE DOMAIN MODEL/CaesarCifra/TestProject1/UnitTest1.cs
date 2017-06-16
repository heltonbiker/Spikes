using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using  CaesarCifra;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestaString()
        {
            Cifra cifra = new Cifra();
            String expected = "d";
            Assert.AreEqual("d", cifra.Criptografar("a"));
        }
    }
}
