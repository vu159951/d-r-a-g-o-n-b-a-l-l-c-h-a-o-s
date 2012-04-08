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
    public class MyXNAButton : VisibleGameEntity
    {
        public MyXNAButton(ContentManager content, string[] strTextures, int nTexture, Vector2 topleft, Vector2 size)
        {
            initMyXNAButton(content, strTextures, nTexture, ref topleft, ref size);
        }

        private void initMyXNAButton(ContentManager content, string[] strTextures, int nTexture, ref Vector2 topleft, ref Vector2 size)
        {
            this._TopLeft = topleft;
            this._nSprite = nTexture;
            this._Size = size;

            _sprites = new List<My2DSprite>();
            Texture2D[] textures = new Texture2D[nTexture];
            for (int i = 0; i < nTexture; i++)
            {
                textures[i] = content.Load<Texture2D>(strTextures[i]);
            }
            My2DSprite tempSprite = new My2DSprite(textures, topleft);
            _sprites.Add(tempSprite);
            _nSprite = 1;
            EnableAnimation(false);
        }

        public void SetDelayAnimation(float value)
        {
            for (int i = 0; i < _nSprite; i++)
                _sprites[i].NormalDelay = value;
        }


        public void EnableAnimation(bool value)
        {
            for (int i = 0; i < _nSprite; i++)
                _sprites[i].BAnimation = value;
        }

        public MyXNAButton(ContentManager content, string strPrefix, int nTexture, Vector2 topleft, Vector2 size)
        {
            string[] strTextures = new string[nTexture];
            for (int i = 0; i < nTexture; i++)
            {
                strTextures[i] = strPrefix + i.ToString("00");
            }
            initMyXNAButton(content, strTextures, nTexture, ref topleft, ref size);
        }


        public MyXNAButton(ContentManager content, string strXML)
        {

        }

    }
}
