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
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;
 

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

        GameState _state;


        GameMenu _menu;
        HeadsUpDisplay _hud;

        SpriteBatch spriteBatch;

        GraphicsDeviceManager _graphics;
        Vector2 _screenCenter;

        World _world = new World(new Vector2(0, 0));

        GameObject _baby;
        GameObject _devil;
        GameObject _angel;

        //Debug view
        bool _showDebug = false;
        DebugViewXNA _debugView;

        GameObjectManager _gameObjectManager;
        RenderManager _renderManager;
        Controls _controls;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;

            BodyFactory.CreateEdge(_world, new Vector2(0, 0) / METER_IN_PIXEL, new Vector2(0, WIDTH) / METER_IN_PIXEL);
            //left
            BodyFactory.CreateEdge(_world, new Vector2(0, 0) / METER_IN_PIXEL, new Vector2(WIDTH, 0) / METER_IN_PIXEL);
            //right
            BodyFactory.CreateEdge(_world, new Vector2(WIDTH, 0) / METER_IN_PIXEL, new Vector2(WIDTH, HEIGHT) / METER_IN_PIXEL);
            //bottom
            BodyFactory.CreateEdge(_world, new Vector2(0, HEIGHT) / METER_IN_PIXEL, new Vector2(WIDTH, HEIGHT) / METER_IN_PIXEL);

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
            _renderManager = new RenderManager();
            _gameObjectManager = new GameObjectManager(_world, _renderManager);
            
            CreateBaby();
            _controls = new Controls(this);
            _controls.Angel = _angel;
            _controls.Baby = _baby;
            _controls.Devil = _devil;

            _gameObjectManager.GetItem("knife");
            _gameObjectManager.GetItem("bunny");

            base.Initialize();
        }

        private void CreateBaby()
        {
            _baby = _gameObjectManager.GetBaby();
            _devil = _gameObjectManager.GetDevil();
          
            _angel = _gameObjectManager.GetAngel();

            RopeJoint jLeftArm = new RopeJoint(_devil.Body, _baby.Body, new Vector2(0f, 0f), new Vector2(-.01f, 0f));
            jLeftArm.MaxLength = 2f;
            _world.AddJoint(jLeftArm);

            RopeJoint jRightArm = new RopeJoint(_angel.Body, _baby.Body, new Vector2(0f, 0f), new Vector2(.01f, 0f));
            jRightArm.MaxLength = 2f;
            _world.AddJoint(jRightArm);
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
            _renderManager.LoadContent(Content, _gameObjectManager.GetAll());
            _state = GameState.Menu;
            _hud = new HeadsUpDisplay(Content);
            
            _menu = new GameMenu(Content, this);
            _screenCenter = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2f,
                                               _graphics.GraphicsDevice.Viewport.Height / 2f);

            //_ragdoll.LoadContent(Content);

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
            _controls.Update();

            if (_baby.HeldItem != null)
            {
                
                _hud.PushItem(_baby.HeldItem);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                _state = GameState.Playing;
            
            
            
            
            if(GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                _state = GameState.Menu;
            } 
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
            {
                _showDebug = true;
            }
            else
            {
                _showDebug = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) 
            {
                _hud.UpdateEvilOMeter(1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _hud.UpdateEvilOMeter(-1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                _hud.PushItem(_devil);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                _hud.PopItem();
            }
            if (_state == GameState.Menu)
            {
                _menu.Update(GamePad.GetState(PlayerIndex.One));
            }

            _world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (up == true)
                theBackground.Update(bar++);
            if (!up)
                theBackground.Update(bar--);

            if (bar == 100)
                up = false;
            else if (bar == -100)
                up = true;

        
            theBackground.Draw(spriteBatch);
            if (_state == GameState.Menu)
            {
                _menu.Draw(spriteBatch);
            }
            else
            {
                _renderManager.Draw(spriteBatch, _gameObjectManager.GetAll());
                _hud.Draw(spriteBatch, this.Window);
            }

            
            
            // TODO: Add your drawing code here
            // calculate the projection and view adjustments for the debug view
            Matrix projection = Matrix.CreateOrthographicOffCenter(0f, _graphics.GraphicsDevice.Viewport.Width / METER_IN_PIXEL,
                                                             _graphics.GraphicsDevice.Viewport.Height / METER_IN_PIXEL, 0f, 0f,
                                                             1f);
            Matrix view = Matrix.CreateTranslation(new Vector3((Vector2.Zero / METER_IN_PIXEL) - (_screenCenter / METER_IN_PIXEL), 0f)) * Matrix.CreateTranslation(new Vector3((_screenCenter / METER_IN_PIXEL), 0f));

            spriteBatch.End();

            if (_showDebug)
                _debugView.RenderDebugData(ref projection, ref view);
            
          
            
            base.Draw(gameTime);

        }
        #region Properties
        public GameState State
        {
            get { return _state; }
            set { _state = value; }
        }
        #endregion
    }
    public enum GameState
    {
        Menu,
        Playing
    }
}
