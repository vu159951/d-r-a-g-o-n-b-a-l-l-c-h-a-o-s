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
// -----------------------------------------------------------------------
// <copyright file="$safeitemrootname$.cs" company="$registeredorganization$">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MyGame3D_0912100
{
    public class Stage : VisibleGameEntity
    {
        private My3DGameCharacter _PlayerCharacter;

        private My3DGameCharacter _ComputerCharacter;

        //private MAP //chua co map :D

        public Stage(ContentManager content, My3DGameCharacter playerCharacter, My3DGameCharacter computerCharacter, string MapName)
        {
            _PlayerCharacter = playerCharacter;
            _ComputerCharacter = computerCharacter;
            //init map
        }

        public override void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {
            _PlayerCharacter.Update(gameTime, kbs, ms);
            _ComputerCharacter.Update(gameTime, kbs, ms);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            _PlayerCharacter.Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
            _ComputerCharacter.Draw(gameTime, graphicsDevice, spriteBatch, effect, camera);
        }


    }
}
