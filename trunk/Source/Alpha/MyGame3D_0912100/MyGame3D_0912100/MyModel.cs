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
    public abstract class MyModel
    {
        protected float _Scale = 1.0f;

        public float Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }
        protected Vector3 _Position;

        public Vector3 Position
        {
            get { return _Position; }
            set { _Position = value; }
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
            }
        }

        protected Matrix _Rotation;

        virtual public void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {

        }
        virtual public void  Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            
        }
    }
}
