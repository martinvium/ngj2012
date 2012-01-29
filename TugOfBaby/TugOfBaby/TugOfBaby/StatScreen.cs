﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TugOfBaby
{
    class StatScreen
    {
        GraphicsDevice _graphics;
        Vector2 _angelPos = new Vector2(480, 200);
        Vector2 _devilPos = new Vector2(800, 200);
        int devilPoints = 0;
        int angelPoints = 0;
        SpriteFont _font;
        Texture2D winningScreen;

        //todo->angel - ffcc00
        //todo->devil - fa3d27 

        public StatScreen(GraphicsDevice graphics,Texture2D winningScreen, ContentManager content)
        {
            _graphics = graphics;
            _font = content.Load<SpriteFont>("Courier New");
            this.winningScreen = winningScreen;
        }


        public void Draw(GameObject winner, SpriteBatch batch)
        {


            batch.Draw(winningScreen, Vector2.Zero, Color.White); 
           
           
            batch.DrawString(_font, "Points Collected " + angelPoints, new Vector2(_angelPos.X-50, _angelPos.Y + 100), Color.Black);

            if(devilPoints < winner.Statistics.PointsCollected)
                devilPoints++;

         



            //angel stats
            
            


        }
    }
}