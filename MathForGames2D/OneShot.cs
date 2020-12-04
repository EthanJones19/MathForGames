using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    class OneShot : Actor
    {
        private Sprite _sprite;
        private float _speed = 1;

        public OneShot(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/OneShot.png");
            
        }

        public OneShot(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/OneShot.png");
        }


        public override void Draw()
        {
            _sprite.Draw(_localTransform);
            base.Draw();
        }



    }
}
