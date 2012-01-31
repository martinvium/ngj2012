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

        //public Vector2 babyDir = new Vector2();

        GameObject gameObject;
        GameObject devilObject;
        GameObject angelObject;
        GameInstance gameInstance;

        

        public Controls(GameInstance gameInstance)
            {
                this.gameInstance = gameInstance;
            }

        public GameObject Angel
        {
            get { return angelObject; }
            set { angelObject = value; }
        }

        public GameObject Baby
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        public GameObject Devil
        {
            get { return gameObject; }
            set { devilObject = value; }
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            GamePadState padState = GamePad.GetState(PlayerIndex.One);


            if (padState.ThumbSticks.Left.X > 0 || keyState.IsKeyDown(Keys.D))
            {
                angelObject.Body.ApplyLinearImpulse(new Vector2(VELOCITY, 0));
            }

            if (padState.ThumbSticks.Left.X < 0 || keyState.IsKeyDown(Keys.A))
            {
                angelObject.Body.ApplyLinearImpulse(new Vector2(-VELOCITY, 0));
            }

            if (padState.ThumbSticks.Left.Y > 0 || keyState.IsKeyDown(Keys.W))
            {
                angelObject.Body.ApplyLinearImpulse(new Vector2(0, -VELOCITY));
            }

            if (padState.ThumbSticks.Left.Y < 0 || keyState.IsKeyDown(Keys.S))
            {
                angelObject.Body.ApplyLinearImpulse(new Vector2(0, VELOCITY)); ;
            }

            if (padState.ThumbSticks.Right.X > 0 || keyState.IsKeyDown(Keys.Right))
            {
                devilObject.Body.ApplyLinearImpulse(new Vector2(VELOCITY, 0));
            }

            if (padState.ThumbSticks.Right.X < 0 || keyState.IsKeyDown(Keys.Left))
            {
                devilObject.Body.ApplyLinearImpulse(new Vector2(-VELOCITY, 0));
            }

            if (padState.ThumbSticks.Right.Y > 0 || keyState.IsKeyDown(Keys.Up))
            {
                devilObject.Body.ApplyLinearImpulse(new Vector2(0, -VELOCITY));
            }

            if (padState.ThumbSticks.Right.Y < 0 || keyState.IsKeyDown(Keys.Down))
            {
                devilObject.Body.ApplyLinearImpulse(new Vector2(0, VELOCITY));
            }

            if ((padState.Buttons.A == ButtonState.Pressed || keyState.IsKeyDown(Keys.Enter)) && gameInstance.State == GameState.ShowStats)
            {
                gameInstance.PreferredNextState = GameState.Playing;
                gameInstance.Restart = true;
            }

            if ((padState.Buttons.B == ButtonState.Pressed || keyState.IsKeyDown(Keys.Escape)) && gameInstance.State == GameState.ShowStats)
            {
                gameInstance.PreferredNextState = GameState.Menu;
                gameInstance.Restart = true;
            }

            if (padState.Buttons.X == ButtonState.Pressed)
            {
                GamePad.SetVibration(PlayerIndex.One, 1, 0);
            }

            if (padState.Buttons.Y == ButtonState.Pressed)
            {
                GamePad.SetVibration(PlayerIndex.One, 1, 1);
            }


            if (gameInstance.State == GameState.Playing && (padState.Buttons.Back == ButtonState.Pressed || keyState.IsKeyDown(Keys.Escape)))
                gameInstance.State = GameState.Menu;
        }



    }
}
