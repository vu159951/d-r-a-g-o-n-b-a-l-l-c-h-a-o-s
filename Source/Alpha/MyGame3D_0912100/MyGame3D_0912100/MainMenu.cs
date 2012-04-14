﻿using System;
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
using System.Xml;
// -----------------------------------------------------------------------
// <copyright file="$safeitemrootname$.cs" company="$registeredorganization$">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MyGame3D_0912100
{
    public class MainMenu : VisibleGameEntity
    {
        private List<PlanarButton> _ButtonList;
        private int _nButton;
        private int _focusButton;
        private PlanarModel _Title;
        private bool _fros = false;
        private MyVideoPlayer _MainMenuVideoPlayer;
        private string backgrounVideo = "MainMenu\\Start";

        //event

        public event EventHandler NewGame;
        public event EventHandler Option;

        public MainMenu(ContentManager content, string texturePrefix, string[] textures, Vector3[] positions, Vector2[] sizes)
        {
            _ButtonList = new List<PlanarButton>();
            for(int i=0; i<textures.Length; i++)
            {
                PlanarButton planarButtonTemp = new PlanarButton(content, texturePrefix + textures[i], sizes[i], 1.0f, positions[i], Matrix.Identity);
                planarButtonTemp.EnableAnimation(false);
                _ButtonList.Add(planarButtonTemp);
            }
            _nButton = _ButtonList.Count;
            this._MainMenuVideoPlayer = new MyVideoPlayer();
            this._MainMenuVideoPlayer.SetVideoToPlay(backgrounVideo, content);
            _focusButton = 0;
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
                            this.NewGame(this, null);
                            break;
                        }

                    case 1:
                        {
                            this.Option(this, null);
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

            for (int i = 0; i < _nButton; i++)
                _ButtonList[i].Update(gameTime, kbs, ms);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            if (this._MainMenuVideoPlayer.GetVideoPlayerState() != MediaState.Stopped)
            {
                this._MainMenuVideoPlayer.Draw(gameTime, graphicsDevice, spriteBatch, null, camera);
            }

            for(int i=0; i<_nButton; i++)
            {
                _ButtonList[i].Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
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