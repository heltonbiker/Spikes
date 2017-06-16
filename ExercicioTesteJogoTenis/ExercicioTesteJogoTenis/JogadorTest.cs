using JogoTenis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExercicioTesteJogoTenis
{
    
    
    /// <summary>
    ///This is a test class for JogadorTest and is intended
    ///to contain all JogadorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JogadorTest
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


        /// <summary>
        ///A test for MarcarPonto
        ///</summary>
        [TestMethod()]
        public void TestaSeUmJogadorMarcaPonto()
        {
            Jogador arthur = new Jogador("Arthur Krause");
            Jogador vinicius = new Jogador("Vinicius Seganfredo");
            
            String expected = "15";
            Assert.AreEqual(expected, vinicius.MarcarPonto(vinicius, arthur));
            
            expected = "30";
            Assert.AreEqual(expected, vinicius.MarcarPonto(vinicius, arthur));
            
            expected = "40";
            Assert.AreEqual(expected, vinicius.MarcarPonto(vinicius, arthur));
            
            expected = "Ganhou!!";
            Assert.AreEqual(expected, vinicius.MarcarPonto(vinicius, arthur));
        }
        [TestMethod()]
        public void TestaSeDoisJogadoresMarcamPotos()
        {
            Jogador arthur = new Jogador("Arthur Krause");
            Jogador vinicius = new Jogador("Vinicius Seganfredo");
            String expected = "Deuce";

            vinicius.MarcarPonto(vinicius,arthur);
            
            arthur.MarcarPonto(arthur, vinicius);
            
            vinicius.MarcarPonto(vinicius, arthur);
            
            arthur.MarcarPonto(arthur, vinicius);
            
            vinicius.MarcarPonto(vinicius, arthur);
            
            arthur.MarcarPonto(arthur, vinicius);
            
            Assert.AreEqual(expected, vinicius.ActualScore);
            Assert.AreEqual(expected, arthur.ActualScore);

            arthur.MarcarPonto(vinicius, arthur);

            expected = "Advantage Vinicius Seganfredo";
            Assert.AreEqual(expected, vinicius.ActualScore);

            expected = "Deuce";
            vinicius.MarcarPonto(arthur,vinicius);
            Assert.AreEqual(expected, arthur.ActualScore);
            
            
        }
        
    }
}
