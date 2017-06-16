using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AveragingBuffer
{
    class Program {

        static void Main(string[] args) {
        
            var buffer = new AveragingBuffer(300);
            var rand = new Random();
            double av;

            foreach (int n in Enumerable.Range(0, 20000)) {
                double val = rand.NextDouble();
                buffer.Add(val);
                av = buffer.Average;
                Console.WriteLine(av);
            }

        }
    }
}
