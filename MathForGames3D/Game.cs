using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;


namespace MathForGames3D
{
    class Game
    {
        private static bool _gameOver;
        private Camera3D _camera = new Camera3D();
        private static Scene[] _scenes;
        private static int _currentSceneIndex;
        public static bool GameOver
        {
            get
            {
                return _gameOver;
            }
            set
            {
                _gameOver = value;
            }
        }

        public Game()
        {
            _scenes = new Scene[0];
        }

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


        public static bool RemoveScene(Scene scene)
        {

            if (scene == null)
                return false;

            bool sceneRemoved = false;


            Scene[] tempArray = new Scene[_scenes.Length - 1];


            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
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

        private void Start()
        {
            Raylib.InitWindow(1024, 760, "Math For Games");
            Raylib.SetTargetFPS(60);
            _camera.position = new System.Numerics.Vector3(0.0f, 10.0f, 10.0f);
            _camera.target = new System.Numerics.Vector3(0.0f, 0.0f, 0.0f);
            _camera.up = new System.Numerics.Vector3(0.0f, 1.0f, 0.0f);
            _camera.fovy = 45.0f;
            _camera.type = CameraType.CAMERA_PERSPECTIVE;
            Actor actor = new Actor(0, 0, 0, Color.BLUE, Shape.SPHERE, 5);
            Actor actor1 = new Actor(3, 0, 0, Color.RED, Shape.CUBE, 5);
            Scene scene = new Scene();
            scene.AddActor(actor);
            scene.AddActor(actor1);
            SetCurrentScene(AddScene(scene));
        }

        private void Update(float deltaTime)
        {
            if (!_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].Start();

            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.RAYWHITE);
            _scenes[_currentSceneIndex].Draw();
            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        private void End()
        {
            _scenes[_currentSceneIndex].End();
        }

        public void Run()
        {
            Start();

            while (!_gameOver && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
            }

            End();
        }
    }
}
