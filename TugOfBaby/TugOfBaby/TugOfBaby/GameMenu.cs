using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Timers;
using Microsoft.Xna.Framework.Input;

namespace TugOfBaby
{
    class GameMenu
    {
        private const int MAX_MENU_ITEMS = 3;
        int _currentSelection;
        Timer _selectionTimer;
        bool _canChangeSelection = true;
        GamePadState _oldState;
        bool _released = true;
  
        SpriteFont _font;
        public GameMenu(ContentManager content)
        {
            _font = content.Load<SpriteFont>("Courier New");
               
        }


        public void Update(GamePadState padState)
        {

            if (_released && padState.ThumbSticks.Left.Y > 0.5f && _currentSelection < MAX_MENU_ITEMS)
            {
                _currentSelection++;
                _released = false;                
            }
            if (_released && padState.ThumbSticks.Left.Y < -0.5f && _currentSelection > 0)
            {
                _currentSelection--;
                _released = false;
            }
            
            if (padState.ThumbSticks.Left.Length() < 0.2f)
            {
                _released = true;
            }
            _oldState = padState;
        }
        public void Draw(SpriteBatch batch) 
        {
            batch.DrawString(_font, "Play", new Vector2(400, 200), Color.Green);
            batch.DrawString(_font, "Scores", new Vector2(400, 250), Color.Green);
            batch.DrawString(_font, ""+ _currentSelection, new Vector2(400, 300), Color.Green);
        }


    }
}
