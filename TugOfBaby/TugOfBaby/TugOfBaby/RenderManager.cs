﻿using System;
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
        public enum Texture
        {
            ANGEL,
            DEVIL,
            BABY,
            BUNNY,
            KNIFE,
            DRUGS,
            VEGETABLE,
            BIBLE
        }

        private Dictionary<Texture, Sprite> _sprites = new Dictionary<Texture, Sprite>();

        public RenderManager()
        {
            _sprites.Add(Texture.ANGEL, new Sprite("angel", new Vector2(-18, -18)));
            _sprites.Add(Texture.DEVIL, new Sprite("devil", new Vector2(-18, -18)));
            _sprites.Add(Texture.BABY, new Sprite("Child/child_face", new Vector2(-55, -55)));
            /*_textures.Add(Texture.BUNNY, content.Load<Texture2D>("bunny"));
            _textures.Add(Texture.KNIFE, content.Load<Texture2D>("knife"));
            _textures.Add(Texture.DRUGS, content.Load<Texture2D>("drugs"));
            _textures.Add(Texture.VEGETABLE, content.Load<Texture2D>("vegetable"));
            _textures.Add(Texture.BIBLE, content.Load<Texture2D>("bible"));*/
        }

        public Sprite GetSprite(Texture name)
        {
            Sprite tx;
            _sprites.TryGetValue(name, out tx);
            return tx;
        }

        public void LoadContent(ContentManager content, List<GameObject> all)
        {
            foreach(KeyValuePair<Texture, Sprite> kvp in _sprites) {
                kvp.Value.Texture = content.Load<Texture2D>(kvp.Value.Name);
            }

            foreach(GameObject gameObject in all) 
            {
                if(gameObject.Sprite != null && gameObject.Sprite.Texture == null) 
                {
                    gameObject.Sprite.Texture = content.Load<Texture2D>(gameObject.Sprite.Name);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, List<GameObject> all)
        {
            foreach (GameObject gameObject in all)
            {
                if (gameObject.Sprite != null)
                {
                    Vector2 pos = gameObject.Position + gameObject.Sprite.Origin;
                    spriteBatch.Draw(gameObject.Sprite.Texture, pos, Color.White);
                }
            }
        }
    }
}
