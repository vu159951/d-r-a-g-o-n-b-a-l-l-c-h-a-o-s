using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MyGame3D_0912100
{
    public class OptionMenu : VisibleGameEntity
    {
        List<PlanarButton> _ButtonList;
        int _nButton;
        int _focusButton;
        private bool _fros = false;
        private MyVideoPlayer _MainMenuVideoPlayer;
        private string backgroundVideo = "MainMenu\\Start";
        private PlanarModel _PlanarTitle;
        private PlanarModel _PlanarPartOfBar;
        //info
        private int _volume;
        private int _difficuty;

        public OptionMenu(ContentManager content, 
            string texturePrefix, string[] textures,
            Vector3[] position, Vector2[] sizes)
        {
            _ButtonList = new List<PlanarButton>();
            _PlanarTitle = new PlanarModel(content, 
                texturePrefix + textures[0], 
                sizes[0], 
                1.0f, 
                position[0], 
                Matrix.Identity);
            for(int i=1; i<textures.Length-1; i++)
            {
                PlanarButton planarButtonTemp = new PlanarButton(content, 
                    texturePrefix + textures[i], 
                    sizes[i], 
                    1.0f, 
                    position[i], 
                    Matrix.Identity);
                planarButtonTemp.EnableAnimation(false);
                _ButtonList.Add(planarButtonTemp);
            }
            _PlanarPartOfBar = new PlanarModel(content, 
                texturePrefix + textures[textures.Length - 1], 
                sizes[textures.Length - 1], 
                1.0f, 
                position[textures.Length - 1], 
                Matrix.Identity);
            _PlanarPartOfBar.IsAnimate = false;
            _nButton = _ButtonList.Count;
            this._MainMenuVideoPlayer = new MyVideoPlayer();
            this._MainMenuVideoPlayer.SetVideoToPlay(backgroundVideo, content);

            _focusButton = 0;
            _volume = 3;
            _difficuty = 3;
        }


        override public void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {

            if(this._MainMenuVideoPlayer.GetVideoPlayerState() == MediaState.Stopped)
            {
                this._MainMenuVideoPlayer.PlayVideo(true);
            }

            int focusingButton = _focusButton;
            if(!this._fros && kbs.IsKeyDown(Keys.Down))
            {
                focusingButton = (++focusingButton) % _nButton;
                this.SetFocusButton(focusingButton);
                this._fros = true;
            }

            else if (!this._fros && kbs.IsKeyDown(Keys.Up))
            {
                --focusingButton;
                if (focusingButton < 0)
                    focusingButton = _nButton - 1;
                this.SetFocusButton(focusingButton);
                this._fros = true;
            }

            else if (!this._fros && kbs.IsKeyDown(Keys.Enter))
            {
                this._fros = true;

                switch(focusingButton)
                {
                    case 0:
                        {
                            //this.NewGame(this, null);
                            break;
                        }

                    case 1:
                        {
                            //this.Option(this, null);
                            break;
                        }

                    default:
                        break;
                }
            }

            else if(kbs.IsKeyUp(Keys.Up) && kbs.IsKeyUp(Keys.Down) && kbs.IsKeyUp(Keys.Enter))
            {
                this._fros = false;
            }
            
            this.SetFocusButton(_focusButton);
            this._PlanarTitle.Update(gameTime, kbs, ms);
            for (int i = 0; i < _nButton; i++)
                _ButtonList[i].Update(gameTime, kbs, ms);
            this._PlanarPartOfBar.Update(gameTime, kbs, ms);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            if (this._MainMenuVideoPlayer.GetVideoPlayerState() != MediaState.Stopped)
            {
                this._MainMenuVideoPlayer.Draw(gameTime, graphicsDevice, spriteBatch, null, camera);
            }

            _PlanarTitle.Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);

            for(int i=0; i<_nButton; i++)
            {
                _ButtonList[i].Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
            }

            //volume bar
            for(int i=0; i<_volume; i++)
            {
                _PlanarPartOfBar.Position = new Vector3(_PlanarPartOfBar.Position.X + 25 * i, 
                    _ButtonList[0].Position.Y, 0);
                _PlanarPartOfBar.Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
            }

            //difficuty bar
            for (int i = 0; i < _difficuty; i++)
            {
                _PlanarPartOfBar.Position = new Vector3(_PlanarPartOfBar.Position.X + 25 * i, 
                    _ButtonList[1].Position.Y, 0);
                _PlanarPartOfBar.Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
            }
        }

        private void SetFocusButton(int i)
        {
            if (i >= 0 && i < _nButton)
            {
                _ButtonList[_focusButton].EnableAnimation(false);
                _ButtonList[_focusButton].Rotation = Matrix.Identity;
                _ButtonList[i].EnableAnimation(true);
                _focusButton = i;
            }
        }
    }
}
