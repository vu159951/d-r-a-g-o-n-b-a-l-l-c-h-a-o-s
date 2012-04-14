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

// -----------------------------------------------------------------------
// <copyright file="$safeitemrootname$.cs" company="$registeredorganization$">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MyGame3D_0912100
{
    public class MyVideoPlayer : VisibleGameEntity
    {
        private VideoPlayer _VideoPlayer;
        private Video _Video;
        private VideoFrame _VideoFrame = new VideoFrame();
        private ContentManager _Content;

        

        public MyVideoPlayer()
        {
            this._VideoPlayer = new VideoPlayer();
        }

        
        public void SetVideoToPlay(string name, ContentManager content)
        {
            this._Content = content;
            this._Video = content.Load<Video>(name);
        }

        public void SetVolume(float volume)
        {
            this._VideoPlayer.Volume = volume;
        }


        public void PlayVideo(bool IsLoop)
        {
            if(this._Video!= null)
            {
                this._VideoPlayer.IsLooped = IsLoop;
                this._VideoPlayer.Play(this._Video);
            }
        }



        public override void  Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            this._VideoFrame.UpdateFrame(_Content, _VideoPlayer,
                                        new Vector3(graphicsDevice.Viewport.X, graphicsDevice.Viewport.Y, -50),
                                        new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height));
            this._VideoFrame.Draw(gameTime, graphicsDevice, spriteBatch, null, camera);
        }

        
        public MediaState GetVideoPlayerState()
        {
            return this._VideoPlayer.State;
        }

        public void SetVideoPlayerState(MediaState state)
        {
            switch(state)
            {
                case MediaState.Paused:
                    {
                        this._VideoPlayer.Pause();
                        break;
                    }

                case MediaState.Stopped:
                    {
                        this._VideoPlayer.Stop();
                        break;
                    }

                default:
                    break;
            }
        }



    }
}
