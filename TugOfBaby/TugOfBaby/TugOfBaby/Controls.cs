using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TugOfBaby
{
    class Controls
    {
        const float VELOCITY = 3.0f;

        GameObject gameObject;
        Game1 game;

        

        public Controls(Game1 game)
            {
                this.game = game;
            }

        public GameObject Angel
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        public GameObject Baby
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        public GameObject Devil
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            GamePadState padState = GamePad.GetState(PlayerIndex.One);


            if(padState.ThumbSticks.Left.X > 0)
            {
                gameObject.Body.ApplyLinearImpulse(new Vector2(VELOCITY, 0));
            }

            if (padState.ThumbSticks.Left.X < 0)
            {
                gameObject.Body.ApplyLinearImpulse(new Vector2(-VELOCITY, 0));
            }

            if (padState.ThumbSticks.Left.Y > 0)
            {
                gameObject.Body.ApplyLinearImpulse(new Vector2(0, -VELOCITY));
            }

            if (padState.ThumbSticks.Left.Y < 0)
            {
                gameObject.Body.ApplyLinearImpulse(new Vector2(0, VELOCITY)); ;
            }

            if (padState.ThumbSticks.Right.X > 0)
            {
                gameObject.Body.ApplyLinearImpulse(new Vector2(VELOCITY, 0));
            }

            if (padState.ThumbSticks.Right.X < 0)
            {
                gameObject.Body.ApplyLinearImpulse(new Vector2(-VELOCITY, 0));
            }

            if (padState.ThumbSticks.Right.Y > 0)
            {
                gameObject.Body.ApplyLinearImpulse(new Vector2(0, -VELOCITY));
            }

            if (padState.ThumbSticks.Right.Y < 0)
            {
                gameObject.Body.ApplyLinearImpulse(new Vector2(0, VELOCITY));
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            
        
        }



    }
}
