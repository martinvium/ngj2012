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
        Texture2D _blank;

        public RenderManager(GraphicsDevice graphicsDevice)
        {
            _blank = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _blank.SetData(new[] { Color.Black });

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

        public void DrawLine(SpriteBatch batch, float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            batch.Draw(_blank, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }
    }
}
