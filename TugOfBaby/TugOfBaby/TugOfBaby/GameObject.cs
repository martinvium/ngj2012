using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

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

        public Vector2 Position
        {
            get { return _body.Position * Game1.METER_IN_PIXEL; }
        }
    }
}
