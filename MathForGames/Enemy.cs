using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Enemy : Actor
    {
        private Actor _target;
        private Color _alertColor;

        /*public Actor Target
        {

        }*/

        public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {

        }

        public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {

        }



        /*public bool GetTargetInSight(Actor actor)
        {
            if (Target == null)
                return false;
            Vector2 direction = Vector2.Normalize(Position - Targets.Position);

            if (Vector2.DotProduct(Forward, direction) == 1)
                return true;

            return false;
        }

        public override void Update(float deltaTime)
        {
            if(CheckTargetInSight())
            {
                _raycolor = Color.RED;
            }
            else
            {

            }


            base.Update(deltaTime);
        }*/


    }
}
