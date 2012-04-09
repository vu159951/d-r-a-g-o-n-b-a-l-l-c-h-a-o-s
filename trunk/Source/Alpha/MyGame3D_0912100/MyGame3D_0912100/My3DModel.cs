using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SkinnedModel;

namespace MyGame3D_0912100
{
    public class My3DModel : MyModel
    {
        private Model _Model;
        private Dictionary<string, AnimationClip> _ListAnimation = new Dictionary<string, AnimationClip>();
        private AnimationPlayer _AnimationPlayer;
        private Matrix[] _Bones;
        private float _Scale = 1.0f;

        public float Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }
        private float _PositionX = 0;

        public float PositionX
        {
            get { return _PositionX; }
            set { _PositionX = value; }
        }
        private float _PositionY = -50;

        public float PositionY
        {
            get { return _PositionY; }
            set { _PositionY = value; }
        }
        private float _PositionZ = 0;

        public float PositionZ
        {
            get { return _PositionZ; }
            set { _PositionZ = value; }
        }

        public My3DModel(ContentManager content, string modelName)
        {
            // TODO: Complete member initialization
            this._Model = content.Load<Model>(modelName);

            SkinningData MySkiningData = this._Model.Tag as SkinningData;

            if (MySkiningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            this._AnimationPlayer = new AnimationPlayer(MySkiningData);

            this._ListAnimation = MySkiningData.AnimationClips;

            this._Bones = this._AnimationPlayer.GetSkinTransforms();

            //this._AnimationPlayer.StartClip(this._ListAnimation["G_Idle"]);

        }


        public void Draw(GameTime gametime, GraphicsDevice graphicsDevice, Effect effect, Camera camera)
        {
            //foreach (ModelMesh mesh in this._Model.Meshes)
            //{
            //    foreach (BasicEffect eff in mesh.Effects)
            //    {
            //        eff.EnableDefaultLighting();
            //        eff.TextureEnabled = true;
            //        eff.LightingEnabled = true;
            //        eff.World = Matrix.Identity * Matrix.CreateScale(this._Scale) * Matrix.CreateTranslation(new Vector3(this._PositionX, this._PositionY, this._PositionZ));
            //        eff.View = camera.View;
            //        eff.Projection = camera.Projection;
            //    }
            //    mesh.Draw();
            //}

            foreach (ModelMesh mesh in this._Model.Meshes)
            {
                foreach (SkinnedEffect eff in mesh.Effects)
                {
                    eff.SetBoneTransforms(this._Bones);

                    eff.View = camera.View;
                    eff.Projection = camera.Projection;

                    eff.EnableDefaultLighting();

                    eff.SpecularColor = new Vector3(0.25f);
                    eff.SpecularPower = 16;
                }

                mesh.Draw();
            }
        }


        public void Update(GameTime gametime, Microsoft.Xna.Framework.Input.KeyboardState kbstate, Microsoft.Xna.Framework.Input.MouseState moustate)
        {
            this._AnimationPlayer.Update(gametime.ElapsedGameTime, true, Matrix.Identity);
        }


        public void PlayClip(string ClipName, bool haveLoop)
        {
            this._AnimationPlayer.StartClip(this._ListAnimation[ClipName], haveLoop);
        }
    }
}
