using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

// -----------------------------------------------------------------------
// <copyright file="$safeitemrootname$.cs" company="$registeredorganization$">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MyGame3D_0912100
{
    public class VideoFrame : VisibleGameEntity
    {
        private Texture2D _VideoTexture;
        private Vector2 _Size = new Vector2();

        public void UpdateFrame(VideoPlayer player, Vector2 TopLeft, Vector2 Size)
        {
            this._TopLeft = TopLeft;
            this._Size = Size;
            this._VideoTexture = player.GetTexture();
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            Rectangle screen = new Rectangle((int)this._TopLeft.X, (int)this._TopLeft.Y, (int)this._Size.X, (int)this._Size.Y);

            if(this._VideoTexture != null)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(this._VideoTexture, screen, Color.White);

                spriteBatch.End();

                
            }
        }
    }
}
