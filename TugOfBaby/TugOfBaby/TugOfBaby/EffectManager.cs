using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TugOfBaby
{
    class EffectManager
    {
        Texture2D effect1;
        Texture2D effect2;
        Texture2D effect3;
        Texture2D effect4;
        Texture2D effect5;

        SoundEffect sound1;
        SoundEffect sound2;
        SoundEffect sound3;
        SoundEffect sound4;

        ContentManager Content;

        List<Animation> effectList;
        List<Vector2> positionList;
        List<float> rotationList;

        List<Animation> removeAniList;
        List<Vector2> removePosList;
        List<float> removeRotList;

        public EffectManager(ContentManager _Content)
        {
            Content = _Content;

            effect1 = Content.Load<Texture2D>("effect/effect1");
            effect2 = Content.Load<Texture2D>("effect/effect2");
            effect3 = Content.Load<Texture2D>("effect/effect3");
            effect4 = Content.Load<Texture2D>("effect/effect4");
            effect5 = Content.Load<Texture2D>("effect/effect5");

            sound1 = Content.Load<SoundEffect>("effect/angel");
            sound2 = Content.Load<SoundEffect>("effect/demon");
            sound3 = Content.Load<SoundEffect>("effect/death");
            sound4 = Content.Load<SoundEffect>("effect/object");

            effectList = new List<Animation>();
            positionList = new List<Vector2>();
            rotationList = new List<float>();

            removeAniList = new List<Animation>();
            removePosList = new List<Vector2>();
            removeRotList = new List<float>();
        }

        public void AddAngelEffect(Vector2 position){
            Animation myAnimation;
            Animation myAnimation2;

            myAnimation = new Animation(effect3, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            sound1.Play();

            myAnimation.FrameTime = 1 / 30.0f;

            myAnimation2 = new Animation(effect3, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation);
            effectList.Add(myAnimation2);


            positionList.Add(position);
            positionList.Add(position);

            Random rand = new Random();

            rotationList.Add(rand.Next()*10);
            rotationList.Add(rand.Next() * 10);

        }

        public void AddDemonEffect(Vector2 position)
        {
            sound2.Play();

            Animation myAnimation1;
            Animation myAnimation2;

            myAnimation1 = new Animation(effect4, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation1.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation1);

            myAnimation2 = new Animation(effect4, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation2.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation2);


            positionList.Add(position);
            positionList.Add(position);

            Random rand = new Random();

            rotationList.Add(rand.Next()*10);
            rotationList.Add(rand.Next() * 10);
        }

        public void AddSpawnEffect(Vector2 position)
        {
            Animation myAnimation1;
            Animation myAnimation2;

            sound4.Play();

            myAnimation1 = new Animation(effect5, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation1.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation1);
            myAnimation2 = new Animation(effect5, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation2.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation2);

            positionList.Add(position);
            positionList.Add(position);

            Random rand = new Random();

            rotationList.Add(rand.Next() * 10);
            rotationList.Add(rand.Next() * 10);
        }

        public void AddDeathSpawnEffect(Vector2 position)
        {
            Animation myAnimation1;
            Animation myAnimation2;
            Animation myAnimation3;

            sound3.Play();

            myAnimation1 = new Animation(effect5, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation1.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation1);


            myAnimation2 = new Animation(effect2, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation2.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation2);

            myAnimation3 = new Animation(effect2, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation3.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation3);

            positionList.Add(position);
            positionList.Add(position);
            positionList.Add(position);

            Random rand = new Random();

            rotationList.Add(rand.Next() * 10);
            rotationList.Add(rand.Next() * 10);
            rotationList.Add(rand.Next() * 10);
        }

        public void AddDeathDespawnEffect(Vector2 position)
        {
            Animation myAnimation1;
            Animation myAnimation2;
            Animation myAnimation3;



            myAnimation1 = new Animation(effect5, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation1.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation1);


            myAnimation2 = new Animation(effect2, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation2.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation2);

            myAnimation3 = new Animation(effect2, new Rectangle(0, 0, 200, 227), 20, 1.0f, 10, 2);

            myAnimation3.FrameTime = 1 / 30.0f;

            effectList.Add(myAnimation3);

            positionList.Add(position);
            positionList.Add(position);
            positionList.Add(position);

            Random rand = new Random();

            rotationList.Add(rand.Next() * 10);
            rotationList.Add(rand.Next() * 10);
            rotationList.Add(rand.Next() * 10);
        }


        public void update(GameTime gametime)
        {
            if (effectList.Count > 0)
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    effectList[i].Update(gametime);

                    if (effectList[i].Frame > 19)
                    {
                        removeAniList.Add(effectList[i]);
                        removePosList.Add(positionList[i]);
                        removeRotList.Add(rotationList[i]);
                    }
                }
            }

            if (removeAniList.Count > 0)
            {
                for (int i = 0; i < removeAniList.Count; i++)
                {
                    effectList.Remove(removeAniList[i]);
                    rotationList.Remove(removeRotList[i]);
                    positionList.Remove(removePosList[i]);
                }
            }

            removeAniList = new List<Animation>();
            removePosList = new List<Vector2>();
            removeRotList = new List<float>();
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (effectList.Count > 0)
            {

                for (int i = 0; i < effectList.Count; i++)
                {
                    effectList[i].Draw(spriteBatch, positionList[i], rotationList[i], false);
                }
            }
        }
    }


}
