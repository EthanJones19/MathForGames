﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
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
            _sprite = new Sprite("Images/player1.png");
        }

        public Player(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/player1.png");
        }

        public override void Update(float deltaTime)
        {
            float xDirection = 0;
            float yDirection = 0;
            bool xMovement = false;
            bool yMovement = false;


            if(Game.GetKeyDown((int)KeyboardKey.KEY_A))
            {
                xDirection -= Speed;
                xMovement = true;
            }
            if(Game.GetKeyDown((int)KeyboardKey.KEY_D))
            {
                xDirection += Speed;
                xMovement = true;

            }

            if (Game.GetKeyDown((int)KeyboardKey.KEY_W))
            {
                yDirection -= Speed;
                yMovement = true;

            }
            if (Game.GetKeyDown((int)KeyboardKey.KEY_S))
            {
                yDirection += Speed;
                yMovement = true;

            }

            

            Vector2 movementDirection = new Vector2(xDirection, yDirection);
            Velocity += movementDirection.Normalized * Speed;


            if(! xMovement && ! yMovement)
            {
                _velocity = new Vector2(0, 0);
            }

            base.Update(deltaTime);
        }

        public override void OnCollision(Actor other)
        {
            
        }

        public override void Draw()
        {
            _sprite.Draw(_localTransform);
            base.Draw();
        }

    }
}
