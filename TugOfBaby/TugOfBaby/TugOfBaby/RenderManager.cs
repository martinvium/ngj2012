using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TugOfBaby
{
    class RenderManager
    {
        private GameObjectManager _gameObjectManager;

        public RenderManager(GameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }

        public void LoadContent(ContentManager content)
        {
            foreach(GameObject gameObject in _gameObjectManager.GetAll()) 
            {
                if(gameObject.Sprite != null) 
                {
                    gameObject.Sprite.Texture = content.Load<Texture2D>(gameObject.Sprite.Name);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in _gameObjectManager.GetAll())
            {
                if (gameObject.Sprite != null)
                {
                    spriteBatch.Draw(gameObject.Sprite.Texture, gameObject.Position, Color.White);
                }
            }
        }
    }
}
