using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;
        private Scene _scene;

        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;

        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        //Return whether or not the specified ConsoleKey is pressed
        //public static bool CheckKey(ConsoleKey key)
        //{   //If the user has pressed a key
            //if (Console.KeyAvailable)
            //{   //Checks to see if the key pressed matches the argument given
                //if (Console.ReadKey(true).Key == key)
                //{
                    //return true;
                //}
            //}
            //return false;
        //}

        public static ConsoleKey GetNextKey()
        {
            if(!Console.KeyAvailable)
            {
                return 0;
            }
            //Return the key that was pressed
            return Console.ReadKey(true).Key;
        }




        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            Console.CursorVisible = false;
            _scene = new Scene();
            Actor actor = new Actor(0,0, '■', ConsoleColor.Green);
            actor.Velocity.X = 1;
            Player player = new Player(0, 1, '@', ConsoleColor.Red);
            Player player2 = new Player(1, 0, '@', ConsoleColor.Green);
            //Player player3 = new Player()
            Wall wall = new Wall(0, 2, '■', ConsoleColor.Yellow);
            Wall wall2 = new Wall(0, 3, '═', ConsoleColor.Yellow);
            Wall wall3 = new Wall(1, 1, '■', ConsoleColor.Blue);
            _scene.AddActor(actor);
            _scene.AddActor(player);
            _scene.AddActor(wall);
            _scene.AddActor(wall2);
            _scene.AddActor(wall3);
            _scene.AddActor(player2);
        }


        //Called every frame.
        public void Update()
        {
            _scene.Update();
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Console.Clear();
            _scene.Draw();
        }


        //Called when the game ends.
        public void End()
        {

        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while(!_gameOver)
            {
                Update();
                Draw();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
                Thread.Sleep(150);
            }

            End();
        }
    }
}
