using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace TransformAndReverse
{
    class Program
    {
        static void Main(string[] args)
        {
            var transform = new TransformGroup();

            var translate = new TranslateTransform(10, 20);
            transform.Children.Add(translate);

            var scale = new ScaleTransform(1,-1);
            transform.Children.Add(scale);

            var inverse = transform.Inverse;



            Point entrada = new Point(50,60);

            Point saida = transform.Transform(entrada);

            Point reentrada = inverse.Transform(saida);

            Console.WriteLine(entrada);
            Console.WriteLine(saida);
            Console.WriteLine(reentrada);

            
        }
    }
}
