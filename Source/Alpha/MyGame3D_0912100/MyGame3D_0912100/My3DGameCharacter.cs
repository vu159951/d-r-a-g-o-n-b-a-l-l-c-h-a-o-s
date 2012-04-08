using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml;

namespace MyGame3D_0912100
{
    public abstract class My3DGameCharacter : VisibleGameEntity
    {
        protected My3DModel _Model;

        /// <summary>
        /// Clip Name
        /// </summary>
        protected const string GROUND_IDLE = "G_Idle";
        protected const string WALKING = "Walking";

        public override void Update(GameTime gametime, Microsoft.Xna.Framework.Input.KeyboardState kbstate, Microsoft.Xna.Framework.Input.MouseState moustate)
        {
            this._Model.Update(gametime, kbstate, moustate);

            if(kbstate.IsKeyDown(Keys.A))
            {
                //this._Model.PlayClip(FLY_IDLE);
            }
            else if (kbstate.IsKeyDown(Keys.S))
            {
                this._Model.PlayClip(GROUND_IDLE);
            }
        }

        public override void Draw(GameTime gametime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            this._Model.Draw(gametime, graphicsDevice, effect, camera);
        }
    }
}
