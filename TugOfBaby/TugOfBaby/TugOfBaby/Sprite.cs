﻿using System;
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
        Vector2 _origin;

        public Sprite(string name)
        {
            _name = name;
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
    }
}
