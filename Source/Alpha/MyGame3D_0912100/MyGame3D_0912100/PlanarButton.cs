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

namespace MyGame3D_0912100
{
    public class PlanarButton : PlanarEntity
    {
       

        public PlanarButton(ContentManager content, String texture, Vector2 size, float scale, Vector3 position, Matrix rotation)
        {
            _planarModel = new PlanarModel(content, texture, size, scale, position, rotation);
            this.Position = position;
            this.Scale = scale;
            this._isAnimate = false;
        }

        

        public override void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {
            _planarModel.Update(gameTime, kbs, ms);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            _planarModel.Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
        }

        
    }
}
