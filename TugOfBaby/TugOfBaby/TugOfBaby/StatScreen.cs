using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TugOfBaby
{
    class StatScreen
    {
        GraphicsDevice _graphics;
        Vector2 _angelPos = new Vector2(485, 490);
        bool _released = true;
        int devilPoints = 0;
        int angelPoints = 0;
        SpriteFont _font;
        Texture2D winningScreen;

        //todo->angel - ffcc00
        //todo->devil - fa3d27 

        public StatScreen(GraphicsDevice graphics,Texture2D winningScreen, ContentManager content)
        {
            _graphics = graphics;
            _font = content.Load<SpriteFont>("Goudy Stout Regular");
            
            this.winningScreen = winningScreen;
        }

        public void Update(GameInstance game)
        {
            if (_released && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) // play again
            {
                _released = false;
                game.State = GameState.Playing;
            }
            if (_released && GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) //return to menu
            {
                _released = false;
                game.State = GameState.Menu;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released && GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Released)
            {
                _released = true;
            }
        }

        public void Draw(GameObject winner, SpriteBatch batch, bool lose)
        {
            
            batch.Draw(winningScreen, Vector2.Zero, Color.White);
            if (winner != null)
            {
                if (!lose)
                {

                    //batch.DrawString(_font, " " + angelPoints, new Vector2(_angelPos.X, _angelPos.Y), Color.Yellow);

                    if (devilPoints < winner.Statistics.PointsCollected)
                        devilPoints++;
                }
                
            }

        }
        public void ChangeImage(Texture2D newImage)
        {
            winningScreen = newImage;
        }
    }
}
