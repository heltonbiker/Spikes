using ClassLibraryParaTestar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjetoExercicioTestes
{
    
    
    /// <summary>
    ///This is a test class for StringCalculatorTest and is intended
    ///to contain all StringCalculatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringCalculatorTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        [DeploymentItem("ClassLibraryParaTestar.dll")]
        public void TestaValorNulo()
        {
            int resultado = 0;

            StringCalculator calculator = new StringCalculator();
            Assert.AreEqual(resultado, calculator.Add(""));
            Assert.AreEqual(resultado, calculator.Add(null));
        }

        [TestMethod()]
        [DeploymentItem("ClassLibraryParaTestar.dll")]
        public void TestaSomaDoisValores()
        {
            int resultado = 3;
            StringCalculator calculator = new StringCalculator();

            Assert.AreEqual(resultado,calculator.Add("1,2"));
        }

        [TestMethod()]
        [DeploymentItem("ClassLibraryParaTestar.dll")]
        public void TestaSomaComMultiplosValores()
        {

            StringCalculator calculator = new StringCalculator();

            Assert.AreEqual(12, calculator.Add("1,2,3,1,2,3"));
            Assert.AreEqual(32, calculator.Add("1,1,1,1,1,1,2,3,3,3,3,3,3,2,1,2,1"));
            Assert.AreEqual(69, calculator.Add("3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2"));
        }


        [TestMethod()]
        [DeploymentItem("ClassLibraryParaTestar.dll")]
        public void TestaSeAceitaNovasLinhas()
        {
            int resultado = 12;
            StringCalculator calculator = new StringCalculator();

            Assert.AreEqual(resultado, calculator.Add("1\n2\n3\n1\n2\n3"));

        }

        [TestMethod()]
        [DeploymentItem("ClassLibraryParaTestar.dll")]
        [ExpectedException(typeof(ArgumentoInvalidoException), "Tipo de entrada inválida.")]
        public void TestIfMethodThrowsAnExceptionWhenExpected()
        {
            StringCalculator calculator = new StringCalculator();
            calculator.Add("1,2,3,4,5;;2asb");          
        }





    }
}
