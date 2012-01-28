using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;
using FarseerPhysics.DebugViews;
using FarseerPhysics;
 

namespace TugOfBaby
{
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const int WIDTH = 1280;
        public const int HEIGHT = 720;
        public const float METER_IN_PIXEL = 64f;
        Background theBackground;
        bool up = true;
        int bar = 0;

        SpriteBatch spriteBatch;

        GraphicsDeviceManager _graphics;
        Vector2 _screenCenter;

        World _world = new World(new Vector2(0, 20));

        //Debug view
        bool _showDebug = false;
        DebugViewXNA _debugView;

        


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            theBackground = new Background();
            theBackground.LoadContent(this.Content);
            _screenCenter = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2f,
                                               _graphics.GraphicsDevice.Viewport.Height / 2f);

            _debugView = new DebugViewXNA(_world);
            _debugView.AppendFlags(DebugViewFlags.DebugPanel);
            _debugView.DefaultShapeColor = Color.Black;
            _debugView.SleepingShapeColor = Color.LightGray;
            _debugView.LoadContent(GraphicsDevice, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
            {
                _showDebug = true;
            }
            else
            {
                _showDebug = false;
            }
             

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // calculate the projection and view adjustments for the debug view
            Matrix projection = Matrix.CreateOrthographicOffCenter(0f, _graphics.GraphicsDevice.Viewport.Width / METER_IN_PIXEL,
                                                             _graphics.GraphicsDevice.Viewport.Height / METER_IN_PIXEL, 0f, 0f,
                                                             1f);
            Matrix view = Matrix.CreateTranslation(new Vector3((Vector2.Zero / METER_IN_PIXEL) - (_screenCenter / METER_IN_PIXEL), 0f)) * Matrix.CreateTranslation(new Vector3((_screenCenter / METER_IN_PIXEL), 0f));

            if (_showDebug)
                _debugView.RenderDebugData(ref projection, ref view);
            if(up == true)
                theBackground.Update(bar++);
            if(!up)
                theBackground.Update(bar--);

            if (bar == 100)
                up = false;
            else if(bar == -100)
                up = true;

            spriteBatch.Begin();
            theBackground.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}
