using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MyGame3D_0912100
{
    public abstract class GameEntity
    {
        virtual public void Update(GameTime gametime, KeyboardState kbstate, MouseState moustate)
        {
            return;
        }
    }
}
