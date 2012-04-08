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
    public class My2DSprite
    {

        private bool _BAnimation;

        public bool BAnimation
        {
            get { return _BAnimation; }
            set { _BAnimation = value; }
        }
        private Texture2D[] _Textures;

        public Texture2D[] Textures
        {
            get { return _Textures; }
            set { 
                _Textures = value;
                _nTextures = _Textures.Length;
                _iTexture = 0;
            }
        }
        private int _nTextures;

        public int nTextures
        {
            get { return _nTextures; }
            set { _nTextures = value; }
        }
        private int _iTexture;

        public int iTexture
        {
            get { return _iTexture; }
            set { _iTexture = value; }
        }
        private Vector2 _Position;

        public Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public My2DSprite(Texture2D[] textures, Vector2 position)
        {
            this.Textures = textures;
            this.Position = position;
            this.BAnimation = true;
            //this.NormalDelay = _nTextures/;
        }

        private float _NormalDelay = 16f;

        public float NormalDelay
        {
            get { return _NormalDelay; }
            set { _NormalDelay = value; }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Textures[_iTexture], _Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (_BAnimation)
            {
                int delta = (int)(gameTime.TotalGameTime.Milliseconds / _NormalDelay);
                _iTexture = delta % _nTextures;
            }
            else
                _iTexture = 0;
        }

    }
}
