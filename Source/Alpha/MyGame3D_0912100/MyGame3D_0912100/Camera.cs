using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame3D_0912100
{
    public abstract class Camera : InvisibleGameEntity
    {
        protected Vector3 _CameraPosition;

        public Vector3 CameraPosition
        {
            get { return _CameraPosition; }
            set { _CameraPosition = value; }
        }
        protected Vector3 _CameraTarget;

        public Vector3 CameraTarget
        {
            get { return _CameraTarget; }
            set { _CameraTarget = value; }
        }
        protected Vector3 _CameraUpVector;

        public Vector3 CameraUpVector
        {
            get { return _CameraUpVector; }
            set { _CameraUpVector = value; }
        }

        protected Matrix _View;

        public Matrix View
        {
            get { return _View; }
            set { _View = value; }
        }

        protected Matrix _Projection;

        public Matrix Projection
        {
            get { return _Projection; }
            set { _Projection = value; }
        }

        public Camera()
        {

        }
    }
}
