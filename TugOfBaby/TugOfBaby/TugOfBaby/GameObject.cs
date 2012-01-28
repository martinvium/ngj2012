using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TugOfBaby
{
    class GameObject
    {
        Sprite _sprite;

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }
    }
}
