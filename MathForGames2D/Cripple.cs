using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    class Cripple : Actor
    {
        //Sets the variables
        private Sprite _sprite;


        //Constructor
        public Cripple(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/cripple.png");
        }

        //Constructor
        public Cripple(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/cripple.png");
        }

        //Draws the Cripple sprites
        public override void Draw()
        {
            _sprite.Draw(_localTransform);
            base.Draw();
        }
    }
}
