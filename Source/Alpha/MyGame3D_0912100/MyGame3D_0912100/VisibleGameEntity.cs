using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame3D_0912100
{
    public abstract class VisibleGameEntity : GameEntity
    {
        virtual public void Draw(GameTime gametime, GraphicsDevice graphicsDevice, Effect effect, Camera camera)
        {

        }
    }
}
