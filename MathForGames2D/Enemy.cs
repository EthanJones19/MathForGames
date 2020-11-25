using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    
    
        class Enemy : Actor
        {
            private Actor _target;
            private Color _alertColor;
            private Sprite _sprite;


            public Actor Target
            {
                get { return _target; }
                set { _target = value; }
            }

            public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
                : base(x, y, icon, color)
            {
                _sprite = new Sprite("Images/enemy.png");
            }

            public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
                : base(x, y, rayColor, icon, color)
            {
                _sprite = new Sprite("Images/enemy.png");
                _alertColor = Color.RED;
            }



            public bool CheckTargetInSight(float maxAngle, float maxDistance)
            {
                if (Target == null)
                    return false;

                Vector2 direction = Vector2.Normalize(Target.LocalPosition - LocalPosition);

                float distance = direction.Magnitude;

                float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));

                if (angle <= maxAngle && distance <= maxDistance)
                    return true;

                return false;
            }

            public override void Update(float deltaTime)
            {
                if (CheckTargetInSight(1.5f, 5))
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
                _sprite.Draw(_localTransform);
                base.Draw();
            }



        }
    
}
