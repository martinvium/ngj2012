using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TugOfBaby
{
    class Animation
    {
        public Texture2D Texture;

        public List<Rectangle> frames = new List<Rectangle>();
        public int frameWidth = 0;
        public int frameHeight = 0;
        private int currentFrame;
        private float frameTime = 0.0333f;
        private float timeForCurrentFrame = 0.0f;
        private float scale;
        private int numberOfFrames;
        private int loopStart;
        private int loopStop;
        public int drawWidth = 0;
        public bool looping = false;


        private Color tintColor = Color.White;

        //How to use in Game1.cs :

        // Animation myAnimation(AnimationTexture, new Rectangle (startX, startY, frameWidth, frameHeight, numberOfFrames, scale);
        // myAnimation.Update(gameTime);
        // myAnimvation.Draw(spriteBatch,position,rotation);

        public Animation(Texture2D texture, Rectangle initialFrame, int totalNumberOfFrames, float _scale, int rows, int collums)
        {
            Texture = texture;
            scale = _scale;
            numberOfFrames = totalNumberOfFrames;

            frames.Add(initialFrame);
            frameWidth = initialFrame.Width;
            frameHeight = initialFrame.Height;

            drawWidth = frameWidth;

            //for (int i = 1; i < numberOfFrames; i++)
            //{
            //    AddFrame(new Rectangle(initialFrame.X + (initialFrame.Width * i), initialFrame.Y, initialFrame.Width, initialFrame.Height));
            //}
            for (int i = 0; i < collums; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    AddFrame(new Rectangle(initialFrame.X + (initialFrame.Width * j), initialFrame.Y + (initialFrame.Height * i), initialFrame.Width, initialFrame.Height));
                }
            }

            loopStart = 0;
            loopStop = totalNumberOfFrames;
        }

        public Animation(Texture2D texture, Rectangle initialFrame, int totalNumberOfFrames, float _scale, int rows, int collums, int start, int stop)
        {
            looping = true;
            Texture = texture;
            scale = _scale;
            numberOfFrames = totalNumberOfFrames;

            frames.Add(initialFrame);
            frameWidth = initialFrame.Width;
            frameHeight = initialFrame.Height;

            drawWidth = frameWidth;

            for (int i = 0; i < collums; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    AddFrame(new Rectangle(initialFrame.X + (initialFrame.Width * j), initialFrame.Y + (initialFrame.Height * i), initialFrame.Width, initialFrame.Height));
                }
            }

            loopStart = start;
            loopStop = stop;
        }

        public Color TintColor
        {
            get { return tintColor; }
            set { tintColor = value; }
        }

        public int Frame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = (int)MathHelper.Clamp(value, 0,
                frames.Count - 1);
            }
        }

        public int TotalFrames
        {
            get { return numberOfFrames - 1; }
        }

        public int LoopStart
        {
            get { return loopStart - 1; }
        }

        public int LoopStop
        {
            get { return loopStop - 1; }
        }

        public float FrameTime
        {
            get { return frameTime; }
            set { frameTime = MathHelper.Max(0, value); }
        }

        public Rectangle Source
        {
            get { return frames[currentFrame]; }
        }

        private void AddFrame(Rectangle frameRectangle)
        {
            frames.Add(frameRectangle);
        }

        public virtual void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            timeForCurrentFrame += elapsed;

            if (timeForCurrentFrame >= FrameTime)
            {
                currentFrame = (currentFrame + 1) % (frames.Count);
                timeForCurrentFrame = 0.0f;
            }

        }

        //public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation)
        //{
        //    spriteBatch.Draw(Texture, position, new Rectangle(frames[currentFrame].X, frames[currentFrame].Y, drawWidth, frames[currentFrame].Height), Color.White, rotation, new Vector2(frameWidth / 2, frameHeight / 2), scale, SpriteEffects.None, 0.0f);
        //}

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, bool flip)
        {
            if (flip)
                spriteBatch.Draw(Texture, position, new Rectangle(frames[currentFrame].X, frames[currentFrame].Y, drawWidth, frames[currentFrame].Height), Color.White, rotation, new Vector2(frameWidth / 2, frameHeight / 2), scale, SpriteEffects.FlipHorizontally, 0.0f);
            else
                spriteBatch.Draw(Texture, position, new Rectangle(frames[currentFrame].X, frames[currentFrame].Y, drawWidth, frames[currentFrame].Height), Color.White, rotation, new Vector2(frameWidth / 2, frameHeight / 2), scale, SpriteEffects.None, 0.0f);
        }

        //public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, Vector2 rotationPoint, float scaleLoad) 
        //{

        //    spriteBatch.Draw(Texture, position, new Rectangle(frames[currentFrame].X, frames[currentFrame].Y, drawWidth, frames[currentFrame].Height), Color.White, rotation, rotationPoint, scaleLoad, SpriteEffects.None, 0.0f);

        //}

    }
}
