using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace TugOfBaby
{
    class FloatingScoreLabel
    {
        private string _text = "";
        private int _timeElapsed;

        public FloatingScoreLabel(string text)
        {
            _text = text;
        }

        public int ElapsedTime
        {
            get { return _timeElapsed; }
            set { _timeElapsed = value; }
        }

        public string Text
        {
            get { return _text; }
        }
    }
}
