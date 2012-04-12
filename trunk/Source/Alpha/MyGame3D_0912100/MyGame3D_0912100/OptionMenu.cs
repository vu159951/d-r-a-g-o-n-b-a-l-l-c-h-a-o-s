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
    using Microsoft.Xna.Framework.Content;

    public class OptionMenu : VisibleGameEntity
    {
        private PlanarModel xxx;
        private Matrix _Rotation;
        private MyVideoPlayer myVideoPlayer;
        private string backgroundVideo = "TrailerVideo\\Trailer";

        public OptionMenu(ContentManager content)
        {
            //xxx = new PlanarModel(content, "xxx", 1f, Vector3.Zero, Matrix.Identity);
            Vector3[] vertices = new Vector3[6];
            vertices[0] = new Vector3(10, 10, 0);
            vertices[1] = new Vector3(10, -10, 0);
            vertices[2] = new Vector3(-10, -10, 0);

            vertices[3] = new Vector3(10, 10, 0);
            vertices[4] = new Vector3(-10, -10, 0);
            vertices[5] = new Vector3(-10, 10, 0);

            Vector2[] textureCordinate = new Vector2[6];
            textureCordinate[0] = new Vector2(1, 0);
            textureCordinate[1] = new Vector2(1, 1);
            textureCordinate[2] = new Vector2(0, 1);
            textureCordinate[3] = new Vector2(1, 0);
            textureCordinate[4] = new Vector2(0, 1);
            textureCordinate[5] = new Vector2(0, 0);
            xxx.InitMyPlanarModel(vertices, textureCordinate);
            myVideoPlayer = new MyVideoPlayer();
            myVideoPlayer.SetVideoToPlay(backgroundVideo, content);
            myVideoPlayer.PlayVideo(true);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            myVideoPlayer.Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
            xxx.Draw(gameTime, graphicsDevice, spriteBatch, new BasicEffect(graphicsDevice), camera);
        }

        public override void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {
            myVideoPlayer.Update(gameTime, kbs, ms);
            xxx.Update(gameTime, kbs, ms);
        }

        public Matrix Rotation
        {
            get
            {
                return this._Rotation;
            }
            set
            {
                this._Rotation = value;
                xxx.Rotation = Rotation;
            }
        }

    }
}
