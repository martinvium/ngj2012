using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;

namespace TugOfBaby
{
    class GameObject
    {
        Sprite _sprite;
        Body _body;

        public Body Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }
    }
}
