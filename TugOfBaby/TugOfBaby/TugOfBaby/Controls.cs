using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TugOfBaby
{
    public class Controls
    {
        const float VELOCITY = 5.0f;

        Vector2 direction = new Vector2();

        public void Update()
        {

            GamePadState padState = GamePad.GetState(PlayerIndex.One);


            if(padState.ThumbSticks.Left.X > 0)
            {
                direction = new Vector2(VELOCITY, 0);
            }

            if (padState.ThumbSticks.Left.X < 0)
            {
                direction = new Vector2(-VELOCITY, 0);
            }

            if (padState.ThumbSticks.Left.Y > 0)
            {
                direction = new Vector2(0, VELOCITY);
            }

            if (padState.ThumbSticks.Left.Y < 0)
            {
                direction = new Vector2(0, -VELOCITY);
            }

            if (padState.ThumbSticks.Right.X > 0)
            {
                direction = new Vector2(VELOCITY, 0);
            }

            if (padState.ThumbSticks.Right.X < 0)
            {
                direction = new Vector2(-VELOCITY, 0);
            }

            if (padState.ThumbSticks.Right.Y > 0)
            {
                direction = new Vector2(0, VELOCITY);
            }

            if (padState.ThumbSticks.Right.Y < 0)
            {
                direction = new Vector2(0, -VELOCITY);
            }
        
        }



    }
}
