﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    class Actor
    {
        //Set of Variables
        protected char _icon = ' ';
        protected Matrix3 _localTransform;
        protected Vector2 _velocity;
        protected Matrix3 _globalTransform = new Matrix3();
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();
        protected ConsoleColor _color;
        protected Color _raycolor;
        protected Actor _parent;
        protected Actor[] _children = new Actor[0];
        protected float _rotationAngle;
        protected float _collisionRadius = 1;

        //Gets and Sets the start
        public bool Started { get; private set; }

        //Sets positions of global and local forward
        public Vector2 Forward
        {
            get
            {
                return new Vector2(_globalTransform.m11, _globalTransform.m21);
            }
            set
            {
                Vector2 lookPosition = LocalPosition + value.Normalized;
                LookAt(lookPosition);
            }
        }

        //Sets the collision radius
        public float CollisionRadius
        {
            get
            {
                return _collisionRadius;
            }

        }


        //Sets local position
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

        //Sets a world transform
        public Vector2 WorldTransform
        {
            get
            {
                return new Vector2(_globalTransform.m13, _globalTransform.m23);
            }

        }

        //Sets velocity
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

        //Sets Actor's values
        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _raycolor = Color.WHITE;
            _icon = icon;
            _localTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
        }

        //Sets Actor's values 
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, icon, color)
        {
            _localTransform = new Matrix3();
            _raycolor = rayColor;
        }

        //Adds a child 
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

        //Removes the child
        public bool RemoveChild(Actor child)
        {
            bool childRemoved = false;

            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length - 1];

            int j = 0;
            for (int i = 0; i < _children.Length; i++)
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



        //Sets translation
        public void SetTranslation(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
        }

        //Sets rotation
        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }

        //Gives rotation
        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }
        
        //Sets the dirction to look in a position
        public void LookAt(Vector2 position)
        {
            Vector2 direction = (position - LocalPosition).Normalized;

            float dotProd = Vector2.DotProduct(Forward, direction);
            if (Math.Abs(dotProd) > 1)
                return;
            float angle = (float)Math.Acos(dotProd);

            Vector2 perp = new Vector2(direction.Y, -direction.X);

            float perpDot = Vector2.DotProduct(perp, Forward);

            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }

        //Sets up collision
        public virtual void OnCollision(Actor other)
        {
            
        }

        //Sets the scale
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(new Vector2(x, y));
        }

        //Updates the transformation
        private void UpdateTransform()
        {
            _localTransform = _translation * _rotation * _scale;

            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Game.GetCurrentScene().World * _localTransform;


        }

        //Starts Actor
        public virtual void Start()
        {
            Started = true;
        }

        //Updates Actor
        public virtual void Update(float deltaTime)
        {
            UpdateTransform();
            LocalPosition += _velocity * deltaTime;
        }

        //Draws the Actor
        public virtual void Draw()
        {

            Console.ForegroundColor = _color;

            if (WorldTransform.X >= 0 && WorldTransform.X < Console.WindowWidth
                && WorldTransform.Y >= 0 && WorldTransform.Y < Console.WindowHeight)
            {
                Console.SetCursorPosition((int)WorldTransform.X, (int)WorldTransform.Y);
                Console.Write(_icon);
            }

            Console.ForegroundColor = Game.DefaultColor;
        }

        //Ends Actor
        public virtual void End()
        {
            Started = false;
        }
    }
}
