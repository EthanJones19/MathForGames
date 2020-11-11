using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class Wall : Actor
    {
        public Wall(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
             : base(x, y, icon, color)
        {

        }
    }
}
