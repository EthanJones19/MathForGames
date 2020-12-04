using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames2D
{
    class Sprite
    {
        //Sets a variable
        private Texture2D _texture;

        //Adds width to sprites
        public int Width
        {
            get
            {
                return _texture.width;
            }
            set
            {
                _texture.width = value;
            }
        }

        //Adds height to sprites
        public int Height
        {
            get
            {
                return _texture.height;
            }
            set
            {
                _texture.height = value;
            }
        }

        //Adds textures to sprites
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        //Adds a path 
        public Sprite(string path)
        {
            _texture = Raylib.LoadTexture(path);
        }

        //Draws the sprites
        public void Draw(Matrix3 transform)
        {

            float xMagnitude = (float)Math.Round(new Vector2(transform.m11, transform.m21).Magnitude);
            float yMagnitude = (float)Math.Round(new Vector2(transform.m12, transform.m22).Magnitude);
            Width = (int)xMagnitude;
            Height = (int)yMagnitude;


            System.Numerics.Vector2 pos = new System.Numerics.Vector2(transform.m13, transform.m23);
            System.Numerics.Vector2 forward = new System.Numerics.Vector2(transform.m11, transform.m21);
            System.Numerics.Vector2 up = new System.Numerics.Vector2(transform.m12, transform.m22);
            pos -= (forward / forward.Length()) * Width / 2;
            pos -= (up / up.Length()) * Height / 2;


            float rotation = (float)Math.Atan2(transform.m21, transform.m11);

            //Draw the sprite
            Raylib.DrawTextureEx(_texture, pos * 32,
                (float)(rotation * 180.0f / Math.PI), 32, Color.WHITE);
        }
    }
}
