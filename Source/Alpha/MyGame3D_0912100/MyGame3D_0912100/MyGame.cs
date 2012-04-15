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
        private Vector3 CAMERAPOSITION = new Vector3(0, -50, 300); //vị trí cam
        private Vector3 CAMERATARGET = new Vector3(0, 0, 0); //nhìn tới điểm đó
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
        private string TRAILER_VIDEO = "TrailerVideo\\Trailer";

        private const float BASE_VOLUME = 1.0f / 3.0f;

        private float VOLUME = 1.0f; //0.0f ~ 1.0f
        

        /// <summary>
        /// Game invisible entity
        /// </summary>

        private enum GAME_STATE {MAIN_MENU, CHOOSE_STAGE_INFO, PLAYING, OPTION, TRAILER};
        private GAME_STATE _GameState;
        private Camera _camera;

        private SkinnedEffect _SkinnedEffect;
     
        private bool _TRAILERISPLAYING = false;

        public float GAME_VOLUME
        {
            get { return VOLUME; }
            set
            {
                VOLUME = value;
                this._MyVideoPlayer.SetVolume(value);
                MediaPlayer.Volume = value;
            }
        }



        /// <summary>
        /// Game visible entity
        /// </summary>

        private MainMenu _MainMenu = null;
        private OptionMenu _OptionMenus = null;
        private Stage _Stage;

        private MyVideoPlayer _MyVideoPlayer;


        private Song _Song;
        

        /// <summary>
        /// Event
        /// </summary>
        public event EventHandler SkipTrailerEvent;

        

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

            //Delegate
            this._MainMenu.NewGame += new EventHandler(mainMenu_NewGame);
            this._MainMenu.Option += new EventHandler(mainMenu_Option);
            this._MainMenu.ExitGame += new EventHandler(mainMenu_ExitGame);
            SkipTrailerEvent += new EventHandler(SkipTrailer);


            this._OptionMenus.BackToMainMenu += new EventHandler(optionMenus_BackToMainMenu);
            this._OptionMenus.DownVolume += new EventHandler(_OptionMenus_DownVolume);
            this._OptionMenus.UpVolume += new EventHandler(_OptionMenus_UpVolume);       

            this._MyVideoPlayer = new MyVideoPlayer();
            this._MyVideoPlayer.SetVideoToPlay(TRAILER_VIDEO, Content);


            this._Song = Content.Load<Song>("MainMenu\\SoundEff");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = GAME_VOLUME;

            graphics.ApplyChanges();
        }

        void _OptionMenus_UpVolume(object sender, EventArgs e)
        {
            VOLUME += BASE_VOLUME;
            if (VOLUME > 1.0f)
                GAME_VOLUME = 1.0f;
            else
                GAME_VOLUME = VOLUME;
        }

        void _OptionMenus_DownVolume(object sender, EventArgs e)
        {
            VOLUME -= BASE_VOLUME;
            if (VOLUME < 0.0f)
                GAME_VOLUME = 0.0f;
            else
                GAME_VOLUME = VOLUME;
        }

        void optionMenus_BackToMainMenu(object sender, EventArgs e)
        {
            this._GameState = GAME_STATE.MAIN_MENU;
        }

        void mainMenu_ExitGame(object sender, EventArgs e)
        {
            this.Exit();
        }

        void SkipTrailer(object sender, EventArgs e)
        {
            if(this._MyVideoPlayer.GetVideoPlayerState() == MediaState.Playing)
            {
                this._MyVideoPlayer.SetVideoPlayerState(MediaState.Stopped);
            }
        }

        void mainMenu_Option(object sender, EventArgs e)
        {
            //new cai menu
            
            this._GameState = GAME_STATE.OPTION;
        }

        void mainMenu_NewGame(object sender, EventArgs e)
        {
            _Stage = new Stage(Content, new GogetaSSJ4(Content, new Vector3(0, 0, 0)), new GokuSSJ2(Content, new Vector3(0, 10, 0)), null);
            this._GameState = GAME_STATE.PLAYING;
            ResetGraphicsDeviceState();
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

            string[] textures = {
                                    "New Game",
                                    "Option",
                                    "Exit"
                                };
            Vector3[] positions = {
                                     new Vector3(0, -10, 0),
                                     new Vector3(0, -45, 0),
                                     new Vector3(0, -80, 0),
                                 };

            string texturePrefix = "MainMenu\\";
            Vector2[] sizes = {
                                  new Vector2(130, 60/2),
                                  new Vector2(130, 60/2),
                                  new Vector2(130, 60/2)
                              };

            _MainMenu = new MainMenu(Content, texturePrefix, textures, positions, sizes);

            string[] textures1 = {
                                    "Option",
                                    "SoundVolume",
                                    "Difficuty",
                                    "Back",
                                    "Ball"
                                };
            Vector3[] positions1 = {
                                     new Vector3(0, 60, 0),
                                     new Vector3(-40, 0, 0),
                                     new Vector3(-40, -40, 0),
                                     new Vector3(0, -80, 0),
                                     new Vector3(35, 0, 0),
                                 };

            string texturePrefix1 = "OptionMenu\\";
            Vector2[] sizes1 = {
                                  new Vector2(260/2, 52/2),
                                  new Vector2(220/2, 52/2),
                                  new Vector2(220/2, 52/2),
                                  new Vector2(260/2, 52/2),
                                  new Vector2(20, 30)
                              };
            this._OptionMenus = new OptionMenu(Content, texturePrefix1, textures1, positions1, sizes1);
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
                            if(this._MyVideoPlayer.GetVideoPlayerState() == MediaState.Stopped)
                            {
                                this._MyVideoPlayer.PlayVideo(false);
                                this._TRAILERISPLAYING = true;
                            }
                        }
                        else
                        {

                            if(kbState.IsKeyDown(Keys.Escape))
                            {
                                this.SkipTrailerEvent(this, null);
                            }

                            if (this._MyVideoPlayer.GetVideoPlayerState() == MediaState.Stopped)
                            {
                                this._GameState = GAME_STATE.MAIN_MENU;
                                Update(gameTime);
                            }
                        }
                        break;
                    }

                case GAME_STATE.MAIN_MENU:
                    {
                        _MainMenu.Update(gameTime, kbState, mouState);
                        if(MediaPlayer.State == MediaState.Stopped)
                        {
                            MediaPlayer.Play(this._Song);
                        }
                        break;
                    }
                case GAME_STATE.OPTION:
                    {
                        if (MediaPlayer.State == MediaState.Stopped)
                        {
                            MediaPlayer.Play(this._Song);
                        }
                        this._OptionMenus.Update(gameTime, kbState, mouState);
                        break;
                    }

                case GAME_STATE.PLAYING:
                    {
                        _Stage.Update(gameTime, kbState, mouState);
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
                        if (this._MyVideoPlayer.GetVideoPlayerState() != MediaState.Stopped)
                        {
                            this._MyVideoPlayer.Draw(gameTime, GraphicsDevice, spriteBatch, null, _camera);
                        }
                        break;
                    }
                case GAME_STATE.MAIN_MENU:
                    {
                        _MainMenu.Draw(gameTime, GraphicsDevice, spriteBatch, new BasicEffect(GraphicsDevice), _camera);
                        break;
                    }
                case GAME_STATE.OPTION:
                    {
                        this._OptionMenus.Draw(gameTime, GraphicsDevice, spriteBatch, new BasicEffect(GraphicsDevice), _camera );
                        break;
                    }
                case GAME_STATE.PLAYING:
                    {
                        _Stage.Draw(gameTime, GraphicsDevice, spriteBatch, new BasicEffect(GraphicsDevice), _camera);
                        break;
                    }

                default:
                    break;
            }
            //DrawCordinate();
            base.Draw(gameTime);
        }

        private void ResetGraphicsDeviceState()
        {
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        }

        private void DrawCordinate()
        {
            BasicEffect effect = new BasicEffect(GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.Projection = _camera.Projection;
            effect.View = _camera.View;
            effect.World = Matrix.Identity;


            VertexPositionColor[] vertices = new VertexPositionColor[6];
            vertices[0] = new VertexPositionColor(new Vector3(-100, 0, 0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(100, 0, 0), Color.GreenYellow);
            vertices[2] = new VertexPositionColor(new Vector3(0,-100, 0), Color.Blue);
            vertices[3] = new VertexPositionColor(new Vector3(0, 100, 0), Color.White);
            vertices[4] = new VertexPositionColor(new Vector3(0, 0, -100), Color.Red);
            vertices[5] = new VertexPositionColor(new Vector3(0, 0, 100), Color.Green);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList,
                vertices, 0, 3, VertexPositionColor.VertexDeclaration);
            }
            
        }
    }
}
