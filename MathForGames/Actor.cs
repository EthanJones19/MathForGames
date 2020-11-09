using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Actor
    {
        protected char _icon = ' ';
        protected Matrix3 _localTransform;
        protected Vector2 _velocity;
        protected Matrix3 _globalTransform;
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();
        protected ConsoleColor _color;
        protected Color _raycolor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];

        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get { return _facing; }
            set { _facing = value; }
        }

        public Vector2 WorldPostion
        {
            get
            {
                return new Vector2(_globalTransform.m13, _global)
            }
        }



        
        public Vector2 LocalPosition
        {
            get
            {
                return new Vector2(_localTransform.m13, _localTransform.m23);
            }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }

        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _raycolor = Color.WHITE;
            _icon = icon;
            _position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
            Forward = new Vector2(1, 0);
        }

        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White) 
            : this(x,y,icon,color)
        {
            _raycolor = rayColor;
        }

        public void AddChild(Actor child)
        {
            Actor[] tempArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
        }

        public bool RemoveChild(Actor child)
        {
            bool childRemoved = false;

            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length - 1];

            int j = 0;
            for (int i = 0; i <_children.Length; i++)
            {
                if (child != _children[i])
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                {
                    childRemoved = true;
                }

            }

            _children = tempArray;
            child._parent = null;
            return childRemoved;
        }




        public void SetTranslation(Vector2 position)
        {
            _translation.m13 = position.X;
            _translation.m23 = position.Y;
        }

        public void SetRotation(float radians)
        {
            _rotationAngle = radians;
            _rotation.m11 = (float)Math.Cos(radians);
            _rotation.m21 = -(float)Math.Sin(radians);
            _rotation.m12 = (float)Math.Sin(radians);
            _rotation.m22 = (float)Math.Cos(radians);
        }

        public void Rotate(float radians)
        {
            _rotationAngle += radians;
            SetRotation(_rotationAngle);
        }




        public void SetScale(float x, float y)
        {
            _scale.m11 = x;
            _scale.m22 = y;
        }

        private void UpdateTransform()
        {
            _localTransform = _translation * _rotation * _scale;
        }



        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;

            
        }

        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            UpdateFacing();
            UpdateTransform();
        }

        public virtual void Draw()
        {
            Raylib.DrawText(_icon.ToString(), (int)_position.X * 32, (int)_position.Y * 32, 32, _raycolor);
            Raylib.DrawLine(
                (int)(LocalPosition.X * 32),
                (int)(LocalPosition.Y * 32), 
                (int)((LocalPosition.X + Forward.X) * 32), 
                (int)((LocalPosition.Y + Forward.Y) * 32), 
                Color.WHITE
                );
            Console.ForegroundColor = _color;
            Console.SetCursorPosition((int)_position.X, (int)_position.Y);
            Console.Write(_icon);
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void End()
        {
            Started = false;
        }
    }
}
