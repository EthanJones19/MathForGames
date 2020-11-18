using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames3D
{
    enum Shape
    {
        SPHERE,
        CUBE
    }

    class Actor
    {
        protected char _icon = ' ';
        protected Matrix4 _globalTransform = new Matrix4();
        protected Matrix4 _localTransform = new Matrix4();
        protected Matrix4 _rotation = new Matrix4();
        protected Matrix4 _translation = new Matrix4();
        protected Matrix4 _scale = new Matrix4();
        protected Actor[] _children = new Actor[0];
        protected Vector3 _velocity;
        protected ConsoleColor _color;
        protected Color _rayColor;
        private float _collisionRadius;
        private float _radians;
        private float rotation;
        private Shape _shape;

        public bool Started { get; private set; }
        public Actor Parent { get; private set; }

        public Vector3 Forward
        {
            get
            {
                return new Vector3(_globalTransform.m13, _globalTransform.m23, _globalTransform.m33).Normalized;
            }

        }

        public Vector3 WorldPosition
        {
            get
            {
                return new Vector3(_globalTransform.m14, _globalTransform.m24, _globalTransform.m34);
            }
        }

        public Vector3 LocalPosition
        {
            get
            {
                return new Vector3(_localTransform.m14, _localTransform.m24, _localTransform.m34);
            }
            set
            {
                _translation.m14 = value.X;
                _translation.m24 = value.Y;
                _translation.m34 = value.Z;
            }
        }

        public Vector3 Velocity
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



        public Actor(float x, float y, float z, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            LocalPosition = new Vector3(x, y, z);
            _velocity = new Vector3();
            _color = color;
            _collisionRadius = collisionRadius;
        }



        public Actor(float x, float y, float z, Color rayColor, Shape shape, float collisionRadius, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x, y, z, collisionRadius, icon, color)
        {
            _rayColor = rayColor;
            _shape = shape;
        }

        public virtual void Start()
        {
            Started = true;
        }

        public void AddChild(Actor child)
        {

            Actor[] appendedArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
            {
                appendedArray[i] = _children[i];
            }

            child.Parent = this;


            appendedArray[_children.Length] = child;

            _children = appendedArray;
        }

        public bool RemoveChild(int index)
        {

            if (index < 0 || index >= _children.Length)
            {
                return false;
            }

            bool actorRemoved = false;


            Actor[] newArray = new Actor[_children.Length - 1];

            int j = 0;

            for (int i = 0; i < _children.Length; i++)
            {


                if (i != index)
                {
                    newArray[j] = _children[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                }
            }
            _children[index].Parent = null;

            _children = newArray;
            return actorRemoved;
        }

        public bool RemoveChild(Actor child)
        {

            if (child == null)
            {
                return false;
            }

            bool actorRemoved = false;

            Actor[] newArray = new Actor[_children.Length - 1];

            int j = 0;

            for (int i = 0; i < _children.Length; i++)
            {
                if (child != _children[i])
                {
                    newArray[j] = _children[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                }
            }
            child.Parent = null;

            _children = newArray;

            return actorRemoved;
        }

        public void SetScale(Vector3 scale)
        {
            _scale.m11 = scale.X;
            _scale.m22 = scale.Y;
            _scale.m33 = scale.Z;
        }

        public void Scale(Vector3 scale)
        {
            if (scale.X != 0)
                _scale.m11 *= scale.X;
            if (scale.Y != 0)
                _scale.m22 *= scale.Y;
            if (scale.Z != 0)
                _scale.m33 *= scale.Z;
        }


        public void SetRotationX(float radians)
        {
            _radians = radians;
            _rotation.m22 = (float)Math.Cos(_radians);
            _rotation.m32 = -(float)Math.Sin(_radians);
            _rotation.m23 = (float)Math.Sin(_radians);
            _rotation.m33 = (float)Math.Cos(_radians);
        }


        public void SetRotationY(float radians)
        {
            _radians = radians;
            _rotation.m11 = (float)Math.Cos(_radians);
            _rotation.m31 = (float)Math.Sin(_radians);
            _rotation.m13 = -(float)Math.Sin(_radians);
            _rotation.m33 = (float)Math.Cos(_radians);
        }


        public void SetRotationZ(float radians)
        {
            _radians = radians;
            _rotation.m11 = (float)Math.Cos(_radians);
            _rotation.m12 = (float)Math.Sin(_radians);
            _rotation.m21 = -(float)Math.Sin(_radians);
            _rotation.m22 = (float)Math.Cos(_radians);
        }


        public void Rotate(float radians)
        {
            _radians += radians;
            SetRotationY(_radians);
        }



        protected void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;


        }



        private void UpdateGlobalTransform()
        {
            if (Parent != null)
                _globalTransform = Parent._globalTransform * _localTransform;
            else
                _globalTransform = _localTransform;

            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].UpdateGlobalTransform();
            }
        }

        public bool CheckCollision(Actor other)
        {
            float distance = (other.WorldPosition - WorldPosition).Magnitude;
            return distance <= _collisionRadius + other._collisionRadius;
        }

        public virtual void OnCollision(Actor other)
        {

        }

        public virtual void Update(float deltaTime)
        {

            _localTransform = _translation * _rotation * _scale;

            UpdateGlobalTransform();


            LocalPosition += _velocity * deltaTime;


            SetRotationY(rotation += (float)(Math.PI / 2) * deltaTime);
        }

        private void DrawShape()
        {
            switch (_shape)
            {
                case Shape.SPHERE:
                    Raylib.DrawSphere(new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z), 5, _rayColor);
                    break;
                case Shape.CUBE:
                    Raylib.DrawCube(new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z), 5, 5, 5, _rayColor);
                    break;
            }
        }

        public virtual void Draw()
        {

            Raylib.DrawLine(
                (int)(WorldPosition.X * 32),
                (int)(WorldPosition.Y * 32),
                (int)((WorldPosition.X + Forward.X) * 32),
                (int)((WorldPosition.Y + Forward.Y) * 32),
                Color.WHITE
            );
            Console.ForegroundColor = _color;
            DrawShape();
        }

        public virtual void Debug()
        {
            if (Parent != null)
                Console.WriteLine("Velocity: " + Velocity.X + ", " + Velocity.Y);
        }

        public void Destroy()
        {

            if (Parent != null)
                Parent.RemoveChild(this);

            foreach (Actor child in _children)
                child.Destroy();

            End();
        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
