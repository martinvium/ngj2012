using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TugOfBaby
{
    class Sprite
    {
        string _name = "";       
        Texture2D _texture;
        Vector2 _origin = new Vector2(0, 0);
        Animation _backgroundAnimation;


        Animation _animation;
        bool flipped = false;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            _origin = GetOriginFromTexture(texture);
        }

        public Sprite(Texture2D texture, Vector2 origin)
        {
            _texture = texture;
            _origin = origin;         
        }

        public Sprite(Animation animation)
        {
            _animation = animation;
            _origin = GetOriginFromAnimation(animation);
        }

        public Sprite(Animation animation, Vector2 origin)
        {
            _animation = animation;
            _origin = origin;
        }
        public Sprite(Animation animation, Vector2 origin, Animation glow)
        {
            _animation = animation;
            _backgroundAnimation = glow;
            _origin = origin;
        }
        public Sprite(Texture2D texture, Vector2 origin, Animation glow)
        {
            _backgroundAnimation = glow;
            _texture = texture;
            _origin = origin;
        }

        public Sprite(Animation animation, Animation glow)
        {
            _animation = animation;
            _backgroundAnimation = glow;
            _origin = GetOriginFromAnimation(animation);
        }

        public Sprite(Texture2D texture, Animation glow)
        {
            _backgroundAnimation = glow;
            _texture = texture;
            _origin = GetOriginFromTexture(texture);
        }

        private Vector2 GetOriginFromTexture(Texture2D texture)
        {
            return new Vector2(-(texture.Width / 2), -(texture.Height / 2));
        }

        private Vector2 GetOriginFromAnimation(Animation animation)
        {
            return Vector2.Zero;
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        internal Animation Animation
        {
            get { return _animation; }
            set { _animation = value; }
        }
        public bool Flipped
        {
            get { return flipped; }
            set { flipped = value; }
        }
        internal Animation BackgroundAnimation
        {
            get { return _backgroundAnimation; }
            set { _backgroundAnimation = value; }
        }
    }
}
