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
    public class PlanarEntity : VisibleGameEntity
    {
        protected PlanarModel _planarModel;

        protected Vector3 _Position;

        protected float _Scale;

        private bool _isAnimate;

        public Vector3 Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                this._Position = value;
                _planarModel.Position = Position;
            }
        }

        public float Scale
        {
            get
            {
                return this._Scale;
            }
            set
            {
                this._Scale = value;
                this._planarModel.Scale = Scale;
            }
        }

        public bool IsAnimate
        {
            get
            {
                return this._isAnimate;
            }
            set
            {
                this._isAnimate = value;
                this._planarModel.IsAnimate = this.IsAnimate;
            }
        }

        virtual public void EnableAnimation(bool p)
        {
            this.IsAnimate = p;
        }
    }
}
