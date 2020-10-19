using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Raylib_cs;

namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;
        private static Scene[] _scenes;
        private static int _currentSceneIndex;

        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;

        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        public static Scene GetScene(int index)
        {
            return _scenes[index];
        }

        public static int AddScene(Scene scene)
        {
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            for(int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            int index = _scenes.Length;
            tempArray[index] = scene;
            _scenes = tempArray;

            return index;
        }

        public static bool RemoveScene(Scene scene)
        {
            if(scene == null)
            {
                return false;
            }

            bool sceneRemoved = false;

            Scene[] tempArray = new Scene[_scenes.Length - 1];

            int j = 0;
            for(int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[i] = _scenes[j];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            if (sceneRemoved)
                _scenes = tempArray;

            return sceneRemoved;
        }

        public static void SetCurrentScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return;

            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            _currentSceneIndex = index;

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

        public Game()
        {
            _scenes = new Scene[0];
        }

        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            //Creates a new window for raylib
            Raylib.InitWindow(1024, 760, "Math For Games");
            //Sets the framerate
            Raylib.SetTargetFPS(60);
            
            //Set up console window
            Console.CursorVisible = false;
            Console.Title = "Math For Games";

            //Create a new scene for our actors to exist in
            Scene scene = new Scene();

            //Creates two actors to add to our scene
            Actor actor = new Actor(0,0,Color.GREEN, '■', ConsoleColor.Green);
            actor.Velocity.X = 1;
            Player player = new Player(0,1, '@', ConsoleColor.Red);
            //Player player3 = new Player()
            Wall wall = new Wall(0, 2, '■', ConsoleColor.Yellow);
            Wall wall2 = new Wall(0, 3, '═', ConsoleColor.Yellow);
            Wall wall3 = new Wall(1, 1, '■', ConsoleColor.Blue);
            scene.AddActor(actor);
            scene.AddActor(player);
            scene.AddActor(wall);
            scene.AddActor(wall2);
            scene.AddActor(wall3);

            int startingSceneIndex = 0;

            startingSceneIndex = AddScene(scene);

            SetCurrentScene(startingSceneIndex);

        }


        //Called every frame.
        public void Update()
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update();
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLUE);
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }


        //Called when the game ends.
        public void End()
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while(!_gameOver &&  !Raylib.WindowShouldClose())
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
