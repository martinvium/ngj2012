using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TugOfBaby
{
    class FloatingScoreManager
    {
        List<GameObject> _gameObjects = new List<GameObject>();
        SpriteFont _font;

        public FloatingScoreManager(SpriteFont font)
        {
            _font = font;
        }

        public void Add(GameObject go)
        {
            _gameObjects.Add(go);
        }

        public void Update(GameTime gameTime)
        {
            foreach(GameObject go in _gameObjects) 
            {
                if(go.Label != null) 
                {
                    go.Label.ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                    if(go.Label.ElapsedTime > 1000) {
                        go.Label = null;
                    }
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (GameObject go in _gameObjects)
            {
                if (go.Label != null)
                {
                    Vector2 pos = new Vector2(go.Position.X, go.Position.Y - 100 - go.Label.ElapsedTime * .1f);
                    batch.DrawString(_font, go.Label.Text, pos, go.LabelColor);
                }
            }
        }
    }
}
