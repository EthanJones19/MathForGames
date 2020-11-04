using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        public Actor Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("Images")
        }

        public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _alertColor = Color.RED;
        }



        public bool CheckTargetInSight(float maxAngle, float maxDistance)
        {
            if (Target == null)
                return false;
            //Find the vector representing the distance between the actor and its target
            Vector2 direction = Vector2.Normalize(Target.LocalPosition - LocalPosition);
            //Get the magnitude of the distance vector
            float distance = direction.Magnitude;
            //Use the inverse cosine to find the angle of the dot product in radians
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));

            if (angle <= maxAngle && distance <= maxDistance )
                return true;

            return false;
        }

        public override void Update(float deltaTime)
        {
            if(CheckTargetInSight(1.5f, 5))
            {
                _raycolor = Color.RED;
            }
            else
            {
                _raycolor = Color.BLUE;
            }
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            base.Draw();
        }



    }
}
