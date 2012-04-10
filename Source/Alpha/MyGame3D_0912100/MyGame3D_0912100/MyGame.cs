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

namespace MyGame3D_0912100
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MyGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Camera parameter
        /// </summary>
        private Vector3 CAMERAPOSITION = new Vector3(0, 30, 40); //vị trí cam
        private Vector3 CAMERATARGET = new Vector3(0, 30, 0); //nhìn tới điểm đó
        private Vector3 CAMERAUPVECTOR = Vector3.Up;
        private float NEARPLANEDISTANCE = 10;
        private float FARPLANEDISTANCE = 1000;
        private float FIELDOFVIEW = MathHelper.PiOver4;
        private float ASPECTRATIO = 1;


        /// <summary>
        /// Rotation parameter
        /// </summary>
        //private float STARTRADIAN = 0;
        //private float VELOCITY = 1;
        
        

        /// <summary>
        /// Game invisible entity
        /// </summary>

        private enum GAME_STATE {MAIN_MENU, CHOOSE_STAGE_INFO, PLAYING, OPTION, TRAILER};
        private GAME_STATE _GameState;
        private Camera _camera;

        //private BasicEffect _basicEffect;
        private SkinnedEffect _SkinnedEffect;

        private VideoPlayer _VideoPlayer;
        private Video _TrailerVideo;
        private bool _TRAILERISPLAYING = false;

        /// <summary>
        /// Game visible entity
        /// </summary>

        private MainMenu mainMenu = null;
        private Stage stage;

        private VideoFrame _VideoFrame = new VideoFrame();

        

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
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
            graphics.PreferredBackBufferWidth = 620;
            graphics.PreferredBackBufferHeight = 450;
            this.ASPECTRATIO = this.Window.ClientBounds.Width / this.Window.ClientBounds.Height;

            this._GameState = GAME_STATE.TRAILER;

            this.mainMenu.NewGame += new EventHandler(mainMenu_NewGame);
            this.mainMenu.Option += new EventHandler(mainMenu_Option);

            //Video
            this._VideoPlayer = new VideoPlayer();
            this._TrailerVideo = Content.Load<Video>("TrailerVideo\\Trailer");


            graphics.ApplyChanges();
        }

        void mainMenu_Option(object sender, EventArgs e)
        {
            //new cai menu
            this._GameState = GAME_STATE.OPTION;
        }

        void mainMenu_NewGame(object sender, EventArgs e)
        {
            stage = new Stage(Content, new GogetaSSJ4(Content, new Vector3(0, 0, 0)), new GokuSSJ2(Content, new Vector3(0, 10, 0)), null);
            this._GameState = GAME_STATE.MAIN_MENU;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            mainMenu = new MainMenu(Content, ".\\Menu.xml");

            this._camera = new PerspectiveCamera(
                this.CAMERAPOSITION,
                this.CAMERATARGET,
                this.CAMERAUPVECTOR,
                this.NEARPLANEDISTANCE,
                this.FARPLANEDISTANCE,
                this.FIELDOFVIEW,
                this.ASPECTRATIO);


            this._SkinnedEffect = new SkinnedEffect(GraphicsDevice);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState kbState = Keyboard.GetState();
            MouseState mouState = Mouse.GetState();

            switch(this._GameState)
            {
                case GAME_STATE.TRAILER:
                    {
                        if(!this._TRAILERISPLAYING)
                        {
                            if(this._VideoPlayer.State == MediaState.Stopped)
                            {
                                this._VideoPlayer.IsLooped = false;
                                this._VideoPlayer.Play(this._TrailerVideo);
                                this._TRAILERISPLAYING = true;
                            }
                        }
                        else
                        {
                            if(this._VideoPlayer.State == MediaState.Stopped)
                            {
                                this._GameState = GAME_STATE.MAIN_MENU;
                                Update(gameTime);
                            }
                        }
                        break;
                    }

                case GAME_STATE.MAIN_MENU:
                    {
                        mainMenu.Update(gameTime, kbState, mouState);
                        break;
                    }

                case GAME_STATE.PLAYING:
                    {
                        stage.Update(gameTime, kbState, mouState);
                        break;
                    }

                default:
                    break;
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

            // TODO: Add your drawing code here


            switch (this._GameState)
            {
                case GAME_STATE.TRAILER:
                    {
                        if(this._VideoPlayer.State != MediaState.Stopped)
                        {
                            this._VideoFrame.UpdateFrame(_VideoPlayer,
                                                            new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y),
                                                            new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
                            this._VideoFrame.Draw(gameTime, GraphicsDevice, spriteBatch, null, _camera);
                        }
                        break;
                    }
                case GAME_STATE.MAIN_MENU:
                    {
                        mainMenu.Draw(gameTime, GraphicsDevice, spriteBatch, new BasicEffect(GraphicsDevice), _camera);
                        break;
                    }

                case GAME_STATE.PLAYING:
                    {
                        stage.Draw(gameTime, GraphicsDevice, spriteBatch, new BasicEffect(GraphicsDevice), _camera);
                        break;
                    }

                default:
                    break;
            }

            base.Draw(gameTime);
        }

        
    }
}
