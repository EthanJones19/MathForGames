using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    class Dragon : Enemy
    {
        //Constructor
        public Dragon(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/dragon.png");

            _collisionRadius = _sprite.Height;
        }

        //Constructor
        public Dragon(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/dragon.png");
        }

        //Draws the Dragon sprites
        public override void Draw()
        {
            _sprite.Draw(_localTransform);
            base.Draw();
        }
    }
}
