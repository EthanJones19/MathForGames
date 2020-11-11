<<<<<<< HEAD:MathForGames/MathForGames/Player.cs
﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed = 1;
        private Sprite _sprite;


        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) 
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
        }

        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
        }

        public override void Update(float deltaTime)
        {
            int xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A)) +
                    Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));

            int yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W)) +
                    Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));

            Velocity = new Vector2(xDirection, yDirection);
            Velocity = Velocity.Normalized * Speed;

            base.Update(deltaTime);
        }

        public override void Draw()
        {
            _sprite.Draw(_localTransform);
            base.Draw();
        }





    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed = 1;
        private Sprite _sprite;


        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public Player(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White) 
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
        }

        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/player.png");
        }

        public override void Update(float deltaTime)
        {
            int xVelocity = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A)) +
                    Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));

            int yVelocity = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W)) +
                    Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));

            Velocity = new Vector2(xDirection, yDirection);
            Velocity = Velocity.Normalized * Speed;

            base.Update(deltaTime);
        }

        public override void Draw()
        {
            _sprite.Draw(_localTransform);
            base.Draw();
        }





    }
}
>>>>>>> master:MathForGames/Player.cs
