using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    class Game
    {
        //Sets the variables
        private static bool _gameOver = false;
        private static Scene[] _scenes;
        private static int _currentSceneIndex;
        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;

        //Gets current scene's index
        public static int CurrentSceneIndex
        {
            get
            {
                return _currentSceneIndex;
            }
        }

        //Sets up a game over
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        //Gets the scene of the index
        public static Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return new Scene();

            return _scenes[index];
        }
        
        //Gets the current scene
        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }

        //Adds a scene
        public static int AddScene(Scene scene)
        {
            if (scene == null)
                return -1;

            Scene[] tempArray = new Scene[_scenes.Length + 1];

            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            int index = _scenes.Length;
            tempArray[index] = scene;
            _scenes = tempArray;

            return index;
        }


        //Gets the key if down
        public static bool GetKeyDown(int key)
        {
            return Raylib.IsKeyDown((KeyboardKey)key);
        }

        //Gets the key if key been pressed
        public static bool GetKeyPressed(int key)
        {
            return Raylib.IsKeyPressed((KeyboardKey)key);
        }

        //Removes a Scene
        public static bool RemoveScene(Scene scene)
        {
            if (scene == null)
            {
                return false;
            }

            bool sceneRemoved = false;

            Scene[] tempArray = new Scene[_scenes.Length - 1];

            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
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

        //Sets a current scene index
        public static void SetCurrentScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return;

            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            _currentSceneIndex = index;

        }

        
        //Game will open up in scenes
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
            Scene scene1 = new Scene();

            //Creates two actors to add to our scene
            Actor actor = new Actor(0, 0, Color.GREEN, '■', ConsoleColor.Green);
            actor.Velocity.X = 1;
            Player player = new Player(0, 1, Color.RED, '@', ConsoleColor.Red);
            Background background = new Background(0, 0, Color.GREEN, '9', ConsoleColor.Green);
            Cripple cripple = new Cripple(0, 1, Color.GREEN, '8', ConsoleColor.Green);
            Dragon dragon = new Dragon(5, 0, Color.GREEN, '7', ConsoleColor.Green);
            OneShot oneshot = new OneShot(0, 0, Color.BLUE, '5', ConsoleColor.Green);

            actor.Velocity.X = 1;
            player.SetTranslation(new Vector2(10, 10));
            background.SetTranslation(new Vector2(10, 10));
            cripple.SetTranslation(new Vector2(29, 11));
            dragon.SetTranslation(new Vector2(30, 10));
            oneshot.SetTranslation(new Vector2(14, 14));
            player.SetScale(1, 1);
            background.SetScale(50, 30);
            cripple.SetScale(2, 2);
            dragon.SetScale(5, 5);
            oneshot.SetScale(1, 1);


            scene1.AddActor(background);
            scene1.AddActor(player);
            scene1.AddActor(cripple);
            scene1.AddActor(dragon);
            scene1.AddActor(oneshot);

            
            player.AddChild(oneshot);

            player.Speed = 0.1f;

            int startingSceneIndex = 0;
            startingSceneIndex = AddScene(scene1);

            SetCurrentScene(startingSceneIndex);

        }



        //Called every frame.
        public void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLACK);
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }


        //Called when the game ends.
        public void End()
        {
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();
        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while (!_gameOver && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }

            End();
        }
    }
}
