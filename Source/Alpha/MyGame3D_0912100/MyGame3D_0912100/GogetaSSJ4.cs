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
    public class GogetaSSJ4 : FlyableCharacter
    {
        private string XmlString = "Gogeta.xml";

        public GogetaSSJ4(ContentManager content)
        {
            this._Model = new My3DModel(content, "Gogeta4/Gogeta4");
            this._Model.PlayClip(GROUND_IDLE, true);
        }



    }
}
