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
        Animation _animation;

   

        public Sprite(string name)
        {
            _name = name;
        }

        public Sprite(string name, Vector2 origin)
        {
            _name = name;
            _origin = origin;
        }
        public Sprite(Texture2D texture, Vector2 origin)
        {
            _texture = texture;
            _origin = origin;         
        }
        public Sprite(Animation animation, Vector2 origin)
        {
            _animation = animation;
            _origin = origin;
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
    }
}
