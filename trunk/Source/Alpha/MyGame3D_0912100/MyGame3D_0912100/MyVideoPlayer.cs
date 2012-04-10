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

        public MyVideoPlayer()
        {
            this._VideoPlayer = new VideoPlayer();
        }

        
        public void SetVideoToPlay(string name, ContentManager content)
        {
            this._Video = content.Load<Video>("TrailerVideo\\Trailer");
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
            this._VideoFrame.UpdateFrame(_VideoPlayer,
                                        new Vector2(graphicsDevice.Viewport.X, graphicsDevice.Viewport.Y),
                                        new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height));
            this._VideoFrame.Draw(gameTime, graphicsDevice, spriteBatch, null, camera);
        }

        
        public MediaState GetVideoPlayerState()
        {
            return this._VideoPlayer.State;
        }

    }
}
