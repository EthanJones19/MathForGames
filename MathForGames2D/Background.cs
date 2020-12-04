using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    class Background : Actor
    {
        //Sets Variables
        private Sprite _sprite;

        //Constructor
        public Background(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images/background.png");
        }

        //Constructor
        public Background(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("Images/background.png");
        }

        //Draws the Background sprites
        public override void Draw()
        {
            _sprite.Draw(_localTransform);
            base.Draw();
        }
    }
}
