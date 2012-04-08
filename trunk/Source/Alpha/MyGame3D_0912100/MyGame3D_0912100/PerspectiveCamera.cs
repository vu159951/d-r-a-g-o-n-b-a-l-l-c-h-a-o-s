using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame3D_0912100
{
    public class PerspectiveCamera : Camera
    {
        private float _NearPlaneDistance;

        public float NearPlaneDistance
        {
            get { return _NearPlaneDistance; }
            set { _NearPlaneDistance = value; }
        }
        private float _FarPlaneDistance;

        public float FarPlaneDistance
        {
            get { return _FarPlaneDistance; }
            set { _FarPlaneDistance = value; }
        }
        private float _FieldOfView;

        public float FieldOfView
        {
            get { return _FieldOfView; }
            set { _FieldOfView = value; }
        }
        private float _AspectRatio;

        public float AspectRatio
        {
            get { return _AspectRatio; }
            set { _AspectRatio = value; }
        }

        public PerspectiveCamera(
            Vector3 cameraPosition,
            Vector3 cameraTarget,
            Vector3 cameraUpVector,
            float nearPlaneDistance,
            float farPlaneDistance,
            float fieldOfView,
            float aspectRatio
            )
        {
            this.CameraPosition = cameraPosition;
            this.CameraTarget = cameraTarget;
            this.CameraUpVector = cameraUpVector;
            this.NearPlaneDistance = nearPlaneDistance;
            this.FarPlaneDistance = farPlaneDistance;
            this.FieldOfView = fieldOfView;
            this.AspectRatio = aspectRatio;
            UpdateMatrices();
        }

        public override void Update(GameTime gametime, Microsoft.Xna.Framework.Input.KeyboardState kbstate, Microsoft.Xna.Framework.Input.MouseState moustate)
        {
            UpdateMatrices();
            base.Update(gametime, kbstate, moustate);
        }

        private void UpdateMatrices()
        {
            this.View = Matrix.CreateLookAt(this.CameraPosition, this.CameraTarget, this.CameraUpVector);
            this.Projection = Matrix.CreatePerspectiveFieldOfView(
                this.FieldOfView,
                this.AspectRatio,
                this.NearPlaneDistance,
                this.FarPlaneDistance);
        }
    }
}
