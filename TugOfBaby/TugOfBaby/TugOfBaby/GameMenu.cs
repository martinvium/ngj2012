using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TugOfBaby
{
    class GameMenu
    {
        int _currentSelection;

  
        SpriteFont _font;
        public GameMenu(ContentManager content)
        {
            _font = content.Load<SpriteFont>("Courier New");
        }


        public void Update()
        {

        }
        public void Draw(SpriteBatch batch) 
        {
            batch.DrawString(_font, "Play", new Vector2(400, 200), Color.Green);
            batch.DrawString(_font, "Scores", new Vector2(400, 250), Color.Green);
            batch.DrawString(_font, "Exit", new Vector2(400, 300), Color.Green);
        }


        #region Properties
        public int CurrentSelection
        {
            get { return _currentSelection; }
            set { _currentSelection = value; }
        }
        #endregion
    }
}
