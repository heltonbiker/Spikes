using Miotec.Vert3d.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Moq;


namespace TestesUnitariosDomainModel
{
    
    [TestClass()]
    public class DetectorFranjasTest
    {


        private TestContext testContextInstance;
        private String path;

        public String DirectoryPath
        {
            get { return "C:/Users/pc1/Dropbox/PequenosProjetos/TESTE DOMAIN MODEL/TesteDomainModel/IMAGENS_FRANJA/fringe4.png"; }
        }

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
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

        //[TestMethod()]
        //[DeploymentItem("Miotec.Vert3d.DomainModel.dll")]
        //public void RaioCirculoPassandoTresPontosTest()
        //{
        //    PrivateObject param0 = null; // TODO: Initialize to an appropriate value
        //    DetectorFranjas_Accessor target = new DetectorFranjas_Accessor(param0); // TODO: Initialize to an appropriate value
        //    double x1 = 0;
        //    double x2 = 1;
        //    double x3 = 2;
        //    double y1 = 10;
        //    double y2 = 8;
        //    double y3 = 5;

        //    double n1 = Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        //    double n2 = Math.Sqrt(Math.Pow((x2 - x3), 2) + Math.Pow((y2 - y3), 2));
        //    double n3 = Math.Sqrt(Math.Pow((x3 - x1), 2) + Math.Pow((y3 - y1), 2));
        //    double s = (n1 + n2 + n3) / 2;
        //    double A = Math.Sqrt(s * (s - n1) * (s - n2) * (s - n3));
        //    double R = n1 * n2 * n3 / (4 * A);
        //    double expected = 19.039432764659669; // TODO: Initialize to an appropriate value
        //    double actual;
        //    actual = R;
        //    Assert.AreEqual(expected, actual);
        //}


        //[TestMethod()]
        //[DeploymentItem("Miotec.Vert3d.DomainModel.dll")]
        //public void InterpolacaoParabolicaTest()
        //{
        //    PrivateObject param0 = null;
        //    DetectorFranjas_Accessor target = new DetectorFranjas_Accessor(param0);
        //    System.Windows.Point p1 = new System.Windows.Point(10, 15);
        //    System.Windows.Point p2 = new System.Windows.Point(5, 10);
        //    System.Windows.Point p3 = new System.Windows.Point(25, 5);
        //    System.Windows.Point expected = new System.Windows.Point(13.5, 16.020833333333332); 

        //    double denom = (p1.X - p2.X) * (p1.X - p3.X) * (p2.X - p3.X);
        //    double A = (p3.X * (p2.Y - p1.Y) + p2.X * (p1.Y - p3.Y) + p1.X * (p3.Y - p2.Y)) / denom;
        //    double B = (p3.X * p3.X * (p1.Y - p2.Y) + p2.X * p2.X * (p3.Y - p1.Y) + p1.X * p1.X * (p2.Y - p3.Y)) / denom;
        //    double C = (p2.X * p3.X * (p2.X - p3.X) * p1.Y + p3.X * p1.X * (p3.X - p1.X) * p2.Y + p1.X * p2.X * (p1.X - p2.X) * p3.Y) / denom;
            
            
        //    System.Windows.Point actual = new System.Windows.Point();
        //    actual.X = -B / (2 * A);
        //    actual.Y = C - B * B / (4 * A);
        //    Assert.AreEqual(expected, actual);
        //}


        /// <summary>
        ///A test for Executar
        ///</summary>
        [TestMethod()]
        public void ExecutarTest()
        {
            Bitmap original = new Bitmap(DirectoryPath);
            DetectorFranjas detect = new DetectorFranjas(original, original.Width / 2);

            detect.Executar();

            Assert.IsNotNull(detect.ListaFranjas);
           
        }
    }
}
    