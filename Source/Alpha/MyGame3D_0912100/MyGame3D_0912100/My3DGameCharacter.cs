using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame3D_0912100
{
    public class My3DGameCharacter : VisibleGameEntity
    {
        private My3DModel _Model;

        /// <summary>
        /// Clip Name
        /// </summary>
        private const string GROUND_IDLE = "G_Idle";
        private const string FLY_IDLE = "Fly_Idle";


        public My3DGameCharacter(ContentManager content, string modelName)
        {
            this._Model = new My3DModel(content, modelName);
            this._Model.PlayClip(GROUND_IDLE);
        }

        public override void Update(GameTime gametime, Microsoft.Xna.Framework.Input.KeyboardState kbstate, Microsoft.Xna.Framework.Input.MouseState moustate)
        {
            this._Model.Update(gametime, kbstate, moustate);

            if(kbstate.IsKeyDown(Keys.A))
            {
                this._Model.PlayClip(FLY_IDLE);
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
