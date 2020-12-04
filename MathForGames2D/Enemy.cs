using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    
        class Enemy : Actor
        {
            //Sets the variables
            private Actor _target;
            protected Sprite _sprite;
        
            //Constructor
            public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
                : base(x, y, icon, color)
            {

            }
            
            //Constructor
            public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
                : base(x, y, rayColor, icon, color)
            {

            }
            
            //Draws the Enemy
            public override void Draw()
            {
                _sprite.Draw(_localTransform);
                base.Draw();

            }

            //Adds collision to Enemy
            public override void OnCollision(Actor other)
            {
                LocalPosition = new Vector2(30, 10);
            }


        }
    
}
