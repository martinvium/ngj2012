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
            BABY_TORSO,
            CHILD_LEFTARM,
            CHILD_LEFTHAND,
            CHILD_RIGHTARM,
            CHILD_RIGHTHAND,
            BUNNY,
            BUNNY_GIRL,
            KNIFE,
            DRUGS,
            VEGETABLE,
            REAPER,
            BIBLE,
            GAME
        }

        private Dictionary<Texture, Sprite> _sprites = new Dictionary<Texture, Sprite>();
        Texture2D _blank;

        public RenderManager(GraphicsDevice graphicsDevice)
        {
            _blank = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _blank.SetData(new[] { Color.Black });

        }

        public Sprite GetSprite(Texture name)
        {
            Sprite tx;
            _sprites.TryGetValue(name, out tx);
            return tx;
        }

        public void LoadContent(ContentManager content, List<GameObject> all)
        {
            Animation redGlow = new Animation(content.Load<Texture2D>("animations/redglow"), new Rectangle(0, 0, 275, 275), 30, 0.5f, 10, 3);
            redGlow.FrameTime = 1 / 8f;

            Animation blueGlow = new Animation(content.Load<Texture2D>("animations/blueglow"), new Rectangle(0, 0, 275, 275), 30, 0.5f, 10, 3);
            redGlow.FrameTime = 1 / 8f;

            Animation animation = new Animation(content.Load<Texture2D>("Animations/angel"), new Rectangle(0,0,110,126), 50, 1.0f, 10,5);
            _sprites.Add(Texture.ANGEL, new Sprite(animation, new Vector2(-18, -18)));
            
            animation = new Animation(content.Load<Texture2D>("animations/demon"), new Rectangle(0, 0, 110, 126), 50, 1.0f, 10, 5);
            _sprites.Add(Texture.DEVIL, new Sprite(animation, new Vector2(-18, -18)));            

            animation = new Animation(content.Load<Texture2D>("animations/death"), new Rectangle(0, 0, 150, 180), 60, 1.0f, 10, 6);
            _sprites.Add(Texture.REAPER, new Sprite(animation, new Vector2(-18, -18)));

            animation = new Animation(content.Load<Texture2D>("animations/bunny2"), new Rectangle(0, 0, 90, 120), 30, 1.0f, 10, 3);
            _sprites.Add(Texture.BUNNY, new Sprite(animation, new Vector2(-18, -18)));

            animation = new Animation(content.Load<Texture2D>("animations/bunny1"), new Rectangle(0, 0, 90, 120), 30, 1.0f, 10, 3);
            _sprites.Add(Texture.BUNNY_GIRL, new Sprite(animation, new Vector2(-50, -50)));

            _sprites.Add(Texture.KNIFE, new Sprite(content.Load<Texture2D>("knife"), new Vector2(-40, -40), redGlow));

            _sprites.Add(Texture.BABY, new Sprite(content.Load<Texture2D>("Child/child_face"), new Vector2(-55, -55)));
            _sprites.Add(Texture.CHILD_LEFTARM, new Sprite(content.Load<Texture2D>("Child/child_leftarm"), new Vector2(0,0)));
            _sprites.Add(Texture.CHILD_LEFTHAND, new Sprite(content.Load<Texture2D>("Child/child_lefthand"), new Vector2(0, 0)));
            _sprites.Add(Texture.CHILD_RIGHTARM, new Sprite(content.Load<Texture2D>("Child/child_rightarm"), new Vector2(0, 0)));
            _sprites.Add(Texture.CHILD_RIGHTHAND, new Sprite(content.Load<Texture2D>("Child/child_righthand"), new Vector2(0, 0)));
             animation = new Animation(content.Load<Texture2D>("animations/bible"), new Rectangle(0, 0, 100, 108), 20, 1.0f, 2, 10);
            _sprites.Add(Texture.BIBLE, new Sprite(animation, new Vector2(-18, -18),blueGlow));


            _sprites.Add(Texture.VEGETABLE, new Sprite(content.Load<Texture2D>("vegi"), new Vector2(-18,-18),blueGlow));

            _sprites.Add(Texture.DRUGS, new Sprite(content.Load<Texture2D>("drugs"), new Vector2(-18, -18),redGlow));

            _sprites.Add(Texture.GAME, new Sprite(content.Load<Texture2D>("games"), new Vector2(-18, -18),redGlow));
        }

        public void Draw(SpriteBatch spriteBatch, List<GameObject> all)
        {
            foreach (GameObject gameObject in all)
            {
                if (gameObject.Sprite != null && gameObject.Enabled)
                {
                    Vector2 pos = gameObject.Position + gameObject.Sprite.Origin;
                    if (gameObject.Sprite.BackgroundAnimation != null)
                    {
                        gameObject.Sprite.BackgroundAnimation.Draw(spriteBatch, new Vector2(pos.X , pos.Y), 0f, false);
                    }
                    if (gameObject.Sprite.Animation == null)
                    {

                        spriteBatch.Draw(gameObject.Sprite.Texture, pos, Color.White);
                    }
                    else
                    {
                        gameObject.Sprite.Animation.Draw(spriteBatch, pos, 0f,gameObject.Sprite.Flipped);
                    }
                }
            }
        }
        public void Update(GameTime gametime, List<GameObject> all)
        {
            foreach (GameObject gameObject in all)
            {
                if (gameObject.Sprite != null)
                {
                    Vector2 pos = gameObject.Position + gameObject.Sprite.Origin;
                    if (gameObject.Sprite.BackgroundAnimation != null)
                    {
                        gameObject.Sprite.BackgroundAnimation.Update(gametime);
                    }
                    if (gameObject.Sprite.Animation != null)                   
                    {       
                        
                        gameObject.Sprite.Animation.Update(gametime);
                    }
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
