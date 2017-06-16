using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            Plateau plateau = new Plateau(5, 5);
            Rover rover = new Rover(1, 2, "N", plateau);
            Rover rover2 = new Rover(3, 3, "E", plateau);
            rover.ReadCommands("LMLMLMLMM");
            rover2.ReadCommands("MMRMMRMRRM");
            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Plateau plateau1 = new Plateau(30000, 30000);
                    Rover rover1 = new Rover(i, j, "N", plateau1);
                    rover1.ReadCommands("LMLMLM");
                    Console.WriteLine(rover1.toString());
                }
                
            }

            Console.WriteLine(rover.toString());
            Console.WriteLine(rover2.toString());
        }
    }
}
