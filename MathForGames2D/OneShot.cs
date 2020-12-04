using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    class OneShot : Actor
    {
        //Sets the variables
        private Sprite _sprite;
        private float _speed = 1;

        //Constructor
        public OneShot(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/OneShot.png");
            
        }

        //Constructor
        public OneShot(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/OneShot.png");
        }

        //Draws OneShots sprite
        public override void Draw()
        {
            _sprite.Draw(_globalTransform);
            base.Draw();
        }








    }
}
