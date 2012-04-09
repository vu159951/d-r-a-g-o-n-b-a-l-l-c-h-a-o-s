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
using System.Xml;
// -----------------------------------------------------------------------
// <copyright file="$safeitemrootname$.cs" company="$registeredorganization$">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MyGame3D_0912100
{
    public class GokuSSJ2 : FlyableCharacter
    {
        private string XmlString = "GokuSSj2.xml";

        public GokuSSJ2(ContentManager content, Vector3 position)
        {
            this._Model = new My3DModel(content, "GokuSSj2/GokuSSj2");
            this._Model.PlayClip(GROUND_IDLE, true);
            this.Position = position;
        }

    }
}
