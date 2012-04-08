using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MyGame3D_0912100
{

    public abstract class VisibleGameEntity : GameEntity
    {
        protected List<My2DSprite> _sprites;
        protected int _nSprite;
        protected Vector2 _TopLeft;
        protected Vector2 _Size;

        public virtual void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            for (int i = 0; i < _nSprite; i++)
                _sprites[i].Draw(gameTime, spriteBatch);
        }

        virtual public void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {
            for (int i = 0; i < _nSprite; i++)
                _sprites[i].Update(gameTime);
        }

        virtual public bool IsClicked(Vector2 point)
        {
            if (point.X >= _TopLeft.X &&
                point.X <= _TopLeft.X + _Size.X &&
                point.Y >= _TopLeft.Y &&
                point.Y <= _TopLeft.Y + _Size.Y)
                return true;
            return false;
        }
    }
}
