using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TugOfBaby
{
   public class BloodManager
    {
        Texture2D bloodSplat1;
        Texture2D bloodSplat2;
        Texture2D bloodSplat3;
        Texture2D bloodFlat1;
        Texture2D bloodFlatAni1;
        Texture2D bloodBall1;
        Texture2D bloodBall2;
        Texture2D bloodSplatAni1;
        Texture2D bloodSplatAni2;

        string[] wallSplatArray = new string[3] { "1", "2", "6" };

        Texture2D pwny_headTexture;
        Texture2D dino_headTexture;
        Texture2D bird_headTexture;
        Texture2D bear_headTexture;

        private List<Blood> bloodSplatter;

        private int maxBlood = 1000;
        private int maxGore = 30;

        private double bloodGameTime = 0.0f;

        ContentManager Content;

        public BloodManager(ContentManager _Content)
        {
            Content = _Content;

            bloodSplatter = new List<Blood>();

            bloodSplat1 = Content.Load<Texture2D>("blood/blood_Splat1");
            bloodSplat2 = Content.Load<Texture2D>("blood/blood_Splat2");
            bloodSplat3 = Content.Load<Texture2D>("blood/blood_Splat3");
            bloodSplatAni1 = Content.Load<Texture2D>("blood/blood_SplatAni1");
            bloodSplatAni2 = Content.Load<Texture2D>("blood/blood_SplatAni2");
            bloodFlat1 = Content.Load<Texture2D>("blood/blood_Flat");
            bloodFlatAni1 = Content.Load<Texture2D>("blood/blood_FlatAni1");
            bloodBall1 = Content.Load<Texture2D>("blood/blood_Ball1");
            bloodBall2 = Content.Load<Texture2D>("blood/blood_Ball2");

        }

        public void Update(GameTime _gameTime)
        {

            bloodGameTime = _gameTime.TotalGameTime.TotalSeconds;
        }

        private void addBlood(string type,Vector2 position, float rotation)
        {
            Blood newBlood = new Blood("1",Vector2.Zero,0.0f);

            if (type == "wall")
            {
                Random rand = new Random();
                int randomNumber = rand.Next(0,3);
                newBlood = new Blood(wallSplatArray[randomNumber], position, rotation);

            }

            if (!checkList(bloodSplatter.Count(), maxBlood))
                bloodSplatter.Add(newBlood);
            else
            {
                bloodSplatter.RemoveAt(0);
                bloodSplatter.Add(newBlood);
            }
        }

        public void addBlood(Vector2 position, Vector2 position2)
        {

                Random rand = new Random();

                Vector2 coordinate = position2 - position ;
                addBlood("wall", position + new Vector2(rand.Next(-50, 50) / 100.0f, rand.Next(-50, 50) / 100.0f) * 30, AngleBetween(coordinate, new Vector2(0,-1)));
                //addBlood("wallAnimation", position + new Vector2(rand.Next(-50, 50) / 100.0f, rand.Next(-50, 50) / 100.0f) * 50, 0);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // 1 BloodSplat1
            // 2 BloodSplat2
            // 3 BloogFlat
            // 4 BloodFlatRun1
            // 5 BloodSplatRun1
            // 6 BloodSplat3

            foreach (Blood blood in bloodSplatter)
            {
                if (blood.Type == "1")
                {
                    spriteBatch.Draw(bloodSplat1, blood.Position, null, Color.White, blood.Rotation, new Vector2(bloodSplat1.Width / 2f, bloodSplat1.Height / 2f), 1f, SpriteEffects.None, 0f);
                }

                else if (blood.Type == "2")
                {
                    spriteBatch.Draw(bloodSplat2, blood.Position, null, Color.White, blood.Rotation, new Vector2(bloodSplat2.Width / 2f, bloodSplat2.Height / 2f), 1f, SpriteEffects.None, 0f);
                }

                else if (blood.Type == "3")
                {
                    spriteBatch.Draw(bloodFlat1, blood.Position+ new Vector2(0,1), null, Color.White, blood.Rotation, new Vector2(bloodFlat1.Width / 2f, bloodFlat1.Height / 2f), 1f, SpriteEffects.None, 0f);
                }
                else if (blood.Type == "6")
                {
                    spriteBatch.Draw(bloodSplat3, blood.Position, null, Color.White, blood.Rotation, new Vector2(bloodSplat3.Width / 2f, bloodSplat3.Height / 2f), 1f, SpriteEffects.None, 0f);
                }
            }

       

        }

        private bool checkList(int listCount, int listMax)
        {

            if (listCount >= listMax)
            {
                return true;
            }

            else

                return false;

        }

        public float AngleBetween(Vector2 a, Vector2 b)
        {
            float dotProd = Vector2.Dot(a, b);
            float lenProd = a.Length() * b.Length();
            float cos = dotProd / lenProd;

            a = new Vector2(a.Y, -a.X);
            dotProd = Vector2.Dot(a, b);
            lenProd = a.Length() * b.Length();
            float sin = dotProd / lenProd;

            return (float)Math.Atan2(sin, cos);
        }
    }
}
