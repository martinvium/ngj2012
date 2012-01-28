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
        float _selectionOffset = 50f;
        bool _released = true;

        GamePadState _oldState;
       
        Texture2D _title;        
        Texture2D _selection;
        
        Vector2 _selectionPosition;
        Vector2 _titlePosition;
        
        Game1 _game;
  
        SpriteFont _font;
        public GameMenu(ContentManager content, Game1 game)
        {
            _font = content.Load<SpriteFont>("Courier New");
            _title = content.Load<Texture2D>("title");
            _titlePosition = new Vector2(300, 100);
            _selectionPosition = new Vector2(650-_selectionOffset, 250);
            _game = game;
        }


        public void Update(GamePadState padState)
        {
            if (_released && padState.ThumbSticks.Left.Y < -0.5f && _currentSelection < MAX_MENU_ITEMS)
            {
                _currentSelection++;
                UpdateSelectionImage(false);
                _released = false;                
            }
            if (_released && padState.ThumbSticks.Left.Y > 0.5f && _currentSelection > 0)
            {
                _currentSelection--;
                UpdateSelectionImage(true);
                _released = false;
            }
            
 

            if (_released && padState.Buttons.A == ButtonState.Pressed)
            {
                Console.WriteLine("Button pressed selection is ->" + _currentSelection);
                ExecuteSelection();
                _released = false;
            }
            if (padState.ThumbSticks.Left.Length() < 0.2f && padState.Buttons.A == ButtonState.Released)
            {
                _released = true;
            }

            _oldState = padState;
        }
        public void Draw(SpriteBatch batch) 
        {

            batch.Draw(_title, _titlePosition, Color.White);
            batch.DrawString(_font, "Play", new Vector2(650, 250), Color.Green);
            batch.DrawString(_font, "Scores", new Vector2(650, 300), Color.Green);
            batch.DrawString(_font, "Exit", new Vector2(650, 350), Color.Green);
            batch.DrawString(_font, "-->", _selectionPosition, Color.Tomato);
        }

        private void UpdateSelectionImage(bool up)
        {
            if (up)
            {
                _selectionPosition.Y = _selectionPosition.Y - 50f;
            }
            else
            {
                _selectionPosition.Y = _selectionPosition.Y + 50f;
            }
        }
        private void ExecuteSelection()
        {
            switch (_currentSelection)
            {  
                case 0:
                    _game.State = GameState.Playing;
                    break;
                case 1:
                    break;
                case 2:
                    _game.Exit();
                    break;
                default:
                    break;
            }
        }

    }
}
