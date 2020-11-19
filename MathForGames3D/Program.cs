using System;
using MathLibrary;

namespace MathForGames3D
{
    class Program
    {
        static void Main(string[] args)
        {

            Matrix3 test = new Matrix3();


            test.m13 = 2;
            test.m23 = 3;

            test = new Matrix3(1, 0, 3, 0, 1, 15, 0, 0, 1) * test;


            Console.WriteLine(test);
            return;
            Game game = new Game();

            game.Run();
        }
    }
}
