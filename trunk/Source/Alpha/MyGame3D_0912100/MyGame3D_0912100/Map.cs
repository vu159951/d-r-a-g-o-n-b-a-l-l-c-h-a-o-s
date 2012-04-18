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
    public class Map : VisibleGameEntity
    {
        private TerrainModel _TerrainModel;

        public Map(ContentManager content, string heightMapTexture, float scale, Vector3 position, Matrix rotation)
        {
            _TerrainModel = new TerrainModel(content, heightMapTexture, scale, position, rotation);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            _TerrainModel.Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
        }

        public override void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {
            _TerrainModel.Update(gameTime, kbs, ms);
        }
    }
}
