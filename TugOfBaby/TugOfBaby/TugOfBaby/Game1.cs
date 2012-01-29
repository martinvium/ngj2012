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

        bool _released = true;

        bool grimReaper = false;
        
        public const float REAPERVELOCITY = 2.0f;
        public Vector2 babyDir = new Vector2();

        public float threeSecondTimer;
        public float tenSecondTimer;

        bool almostDeadlock = false;

        GameState _state;


        GameMenu _menu;
        HeadsUpDisplay _hud;
        StatScreen _statScreen;

        SpriteBatch spriteBatch;

        GraphicsDeviceManager _graphics;
        Vector2 _screenCenter;

        World _world = new World(new Vector2(0, 0));

        //TimeStep 
        GameObject _baby;
        GameObject _devil;
        GameObject _angel;
        GameObject _reaper;
        GameObject _femaleBunny;

        //Debug view
        bool _showDebug = false;
        DebugViewXNA _debugView;

        EffectManager _effectManager;
        BloodManager _bloodManager;
        GameObjectManager _gameObjectManager;
        RenderManager _renderManager;
        Controls _controls;
        FloatingScoreManager _floatingScoreManager;

        RopeJoint jLeftArm;
        RopeJoint jRightArm;
        Ragdoll _ragdoll;
        SpriteFont _font;

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
            _renderManager = new RenderManager(GraphicsDevice);
            _effectManager = new EffectManager(Content);
            _gameObjectManager = new GameObjectManager(_world, _renderManager, _effectManager);

            base.Initialize();
        }

        private void CreateBaby()
        {
            _baby = _gameObjectManager.GetBaby();
            _devil = _gameObjectManager.GetDevil();
          
            _angel = _gameObjectManager.GetAngel();
            _reaper = _gameObjectManager.GetReaper();
          
            _reaper.Body.Position = new Vector2(100, 100);

            jLeftArm = new RopeJoint(_devil.Body, _baby.Body, new Vector2(0f, 0f), new Vector2(-.01f, 0f));
            jLeftArm.MaxLength = 2f;
            _world.AddJoint(jLeftArm);

            jRightArm = new RopeJoint(_angel.Body, _baby.Body, new Vector2(0f, 0f), new Vector2(.01f, 0f));
            jRightArm.MaxLength = 2f;
            _world.AddJoint(jRightArm);

            //_ragdoll = new Ragdoll(_world, _baby.Body, _renderManager);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("Courier New");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            theBackground = new Background();
            theBackground.LoadContent(this.Content);
            _renderManager.LoadContent(Content, _gameObjectManager.GetAll());
            _bloodManager = new BloodManager(Content);
            
            _state = GameState.Menu;
            _hud = new HeadsUpDisplay(Content);
            
           

            CreateBaby();
            
            _controls = new Controls(this);
            _controls.Angel = _angel;
            _controls.Baby = _baby;
            _controls.Devil = _devil;

            _floatingScoreManager = new FloatingScoreManager(_font);
            _floatingScoreManager.Add(_devil);
            _floatingScoreManager.Add(_angel);
            _menu = new GameMenu(Content, this, _devil, _angel);
            _gameObjectManager.SpawnItems();

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
            _gameObjectManager.Update();
            _effectManager.update(gameTime);

            if (_baby.HeldItem != null)
            {
                _hud.PushItem(_baby.HeldItem);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                _state = GameState.Playing;


            if (!_baby.Enabled)
                _state = GameState.ShowStats;
            //reaperUpdate(gameTime);

            //Console.WriteLine(Vector2.Zero);
            //Console.WriteLine(babyTempPos1);
            
            /*Console.WriteLine("Math: ");
            Console.WriteLine(_baby.Position.X - babyTempPos1.X);
            Console.WriteLine("Pos: ");
            Console.WriteLine(_baby.Position.X);
            Console.WriteLine("Prev pos: ");
            Console.WriteLine(babyTempPos1.X);
            */
            //Console.WriteLine((DateTime.Now.Second % 3)+1);
            
            if(GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                _state = GameState.Menu;
            } 
         
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) 
            {
                HeadsUpDisplay.HOW_EVIL += 1;
                _angel.Statistics.PointsCollected++;
               
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                HeadsUpDisplay.HOW_EVIL -= 1;
                _devil.Statistics.PointsCollected++;
                _bloodManager.addBlood(_devil.Body.Position * METER_IN_PIXEL, _angel.Body.Position * METER_IN_PIXEL);
               
            }
            if (_released && Keyboard.GetState().IsKeyDown(Keys.L))
            {
                _gameObjectManager.SpawnItems();
                _effectManager.AddAngelEffect(_devil.Body.Position * METER_IN_PIXEL);
                _released = false;
            }
            if (_released && Keyboard.GetState().IsKeyDown(Keys.K))
            {
                _gameObjectManager.DespawnItems();
                _effectManager.AddDemonEffect(_devil.Position * METER_IN_PIXEL);
                _released = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                _angel.Statistics.CollectItem(_angel, 50);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                _devil.Statistics.CollectItem(_devil, 50);
            }
            if (_state == GameState.Menu)
            {
                _menu.Update(GamePad.GetState(PlayerIndex.One), gameTime);
            }
            else if(_state == GameState.Playing)
            {
                _renderManager.Update(gameTime, _gameObjectManager.GetAll());
                _floatingScoreManager.Update(gameTime);
                _hud.Update(_devil, _angel);
                _world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);
                reaperUpdate(gameTime);
                if (HeadsUpDisplay.HOW_EVIL > 250 || HeadsUpDisplay.HOW_EVIL < 60)
                {
                    _gameObjectManager.StartBunnyBoss();
                }
            }

            if (HeadsUpDisplay.HOW_EVIL <= 0 || HeadsUpDisplay.HOW_EVIL >= 309)
            {
                _state = GameState.ShowStats;
            }



            if (Keyboard.GetState().IsKeyUp(Keys.L) && Keyboard.GetState().IsKeyUp(Keys.K) && Keyboard.GetState().IsKeyUp(Keys.LeftAlt) && Keyboard.GetState().IsKeyUp(Keys.Enter))
             {
                 _released = true;
             }
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

        
            
            if (_state == GameState.Menu)
            {
                _menu.Draw(spriteBatch);

            }
            else if(_state == GameState.Playing)
            {
                theBackground.Draw(spriteBatch);
                _bloodManager.Draw(spriteBatch);
                _renderManager.DrawLine(spriteBatch, 1f, Color.Black, jLeftArm.BodyA.Position * Game1.METER_IN_PIXEL, jLeftArm.BodyB.Position * Game1.METER_IN_PIXEL);
                _renderManager.DrawLine(spriteBatch, 1f, Color.Black, jRightArm.BodyA.Position * Game1.METER_IN_PIXEL, jRightArm.BodyB.Position * Game1.METER_IN_PIXEL);
                _renderManager.Draw(spriteBatch, _gameObjectManager.GetAll());
                _hud.Draw(spriteBatch, this.Window);
                _effectManager.draw(spriteBatch);
                _floatingScoreManager.Draw(spriteBatch);
            }
            else if (_state == GameState.ShowStats)
            {
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
                theBackground.Draw(spriteBatch);
                if(!_baby.Enabled)
                {
                    if (_statScreen == null)
                    {
                        _statScreen = new StatScreen(GraphicsDevice, Content.Load<Texture2D>("NobodyWin"), Content);
                    }
                    _statScreen.Draw(_devil, spriteBatch, false);
                }
                if(_devil.Statistics.PointsCollected > _angel.Statistics.PointsCollected)
                {
                    if (_statScreen == null)
                    {
                        _statScreen = new StatScreen(GraphicsDevice, Content.Load<Texture2D>("devilwin"), Content);
                    }
                    _statScreen.Draw(_devil, spriteBatch, false);
                }
                else if (_devil.Statistics.PointsCollected < _angel.Statistics.PointsCollected)
                {
                    if (_statScreen == null)
                    {
                        _statScreen = new StatScreen(GraphicsDevice, Content.Load<Texture2D>("angelwin"), Content);
                    }
                    _statScreen.Draw(_angel, spriteBatch, false);
                } 

                 
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

        public void reaperUpdate(GameTime _gameTime)
            {
                if (almostDeadlock)
                    GamePad.SetVibration(PlayerIndex.One, 0.15f, 0.15f);
                else if(!grimReaper)
                    GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);

                Console.WriteLine(_reaper.Position);
                if (!grimReaper){
                    _reaper.Enabled = false;
                _reaper.Position = new Vector2(0.15f, 0.09f);
                }

                if (grimReaper)
                {

                    if(!_reaper.Enabled)
                        _effectManager.AddDeathSpawnEffect(_reaper.Body.Position * Game1.METER_IN_PIXEL);
                    _reaper.Enabled = true;
                    
                    reaperMove();
                    tenSecondTimer += (float)_gameTime.ElapsedGameTime.TotalSeconds;
                }

                threeSecondTimer += (float)_gameTime.ElapsedGameTime.TotalSeconds;


                if (tenSecondTimer > 10)
                {
                    if (grimReaper)
                        _effectManager.AddDeathDespawnEffect(_reaper.Body.Position * Game1.METER_IN_PIXEL);
                    grimReaper = false;
                    tenSecondTimer = 0;
                }

                if (threeSecondTimer > 3 && !grimReaper)
                    deadLock();
            }

        public void reaperMove()
        {
            babyDir = new Vector2();

            babyDir = (_baby.Position - _reaper.Position);

            _reaper.Body.ApplyLinearImpulse(babyDir * 0.005f);

            //Console.WriteLine(Vector2.Distance(_baby.Position, _reaper.Position));
            
            if(Vector2.Distance(_baby.Position, _reaper.Position) < 300)
                GamePad.SetVibration(PlayerIndex.One, 0.25f, 0.25f);

            if (Vector2.Distance(_baby.Position, _reaper.Position) < 200)
                GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.5f);

            if (Vector2.Distance(_baby.Position, _reaper.Position) < 100)
                GamePad.SetVibration(PlayerIndex.One, 1, 1);
            
        }
        
        public void deadLock()
            {

                
          
                
            if (_baby.Body.LinearVelocity.X > -1.0f && _baby.Body.LinearVelocity.Y > -1.0f)
                    {
                        if (_baby.Body.LinearVelocity.X < 1.0f && _baby.Body.LinearVelocity.Y < 1.0f)
                            if (almostDeadlock)
                            {
                                Console.WriteLine("O HAI REAPER");
                                almostDeadlock = false;
                                grimReaper = true;
                            }
                            else
                            {
                                Console.WriteLine("Doorbell");
                                almostDeadlock = true;
                            }
                        else
                            almostDeadlock = false;
                    }
                else
                    {
                    almostDeadlock = false;
                    }

                threeSecondTimer = 0;

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
        Playing,
        ShowStats
    }
}
