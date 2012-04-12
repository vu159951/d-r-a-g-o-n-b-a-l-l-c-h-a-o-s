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
namespace MyGame3D_0912100
{
    public class PlanarModel : MyModel
    {
        private VertexPositionTexture[] _Vertices;

        private Texture2D _Texture;

        private float _Scale;

        private Vector3 _Position;

        private Vector2 _Size;

        private Matrix _Rotation;

        private Effect MyEffect;

        private bool isAnimate = true;

        private string EFFECT_NAME = "MyEffect";

        public PlanarModel(ContentManager content, string texture, Vector2 size, float scale, Vector3 position, Matrix rotation)
        {
            this._Texture = content.Load<Texture2D>(texture);
            this.Scale = scale;
            this.Position = position;
            this.Rotation = rotation;
            this.Size = size;
            MyEffect = content.Load<Effect>(EFFECT_NAME);


            Vector3[] vertices = new Vector3[6];
            vertices[0] = new Vector3(Size.X/2, Size.Y/2, 0);
            vertices[1] = new Vector3(Size.X / 2, -Size.Y / 2, 0);
            vertices[2] = new Vector3(-Size.X / 2, -Size.Y / 2, 0);

            vertices[3] = new Vector3(Size.X / 2, Size.Y / 2, 0);
            vertices[4] = new Vector3(-Size.X / 2, -Size.Y / 2, 0);
            vertices[5] = new Vector3(-Size.X / 2, Size.Y / 2, 0);

            Vector2[] textureCordinate = new Vector2[6];
            textureCordinate[0] = new Vector2(1, 0);
            textureCordinate[1] = new Vector2(1, 1);
            textureCordinate[2] = new Vector2(0, 1);
            textureCordinate[3] = new Vector2(1, 0);
            textureCordinate[4] = new Vector2(0, 1);
            textureCordinate[5] = new Vector2(0, 0);
            this.InitMyPlanarModel(vertices, textureCordinate);

        }

        public Matrix Rotation
        {
            get
            {
                return this._Rotation;
            }
            set
            {
                this._Rotation = value;
            }
        }

        public Vector3 Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                this._Position = value;
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
            }
        }

        public Vector2 Size
        {
            get
            {
                return this._Size;
            }
            set
            {
                this._Size = value;
            }
        }

        public bool IsAnimate
        {
            get
            {
                return this.isAnimate;
            }
            set
            {
                this.isAnimate = value;
            }
        }

        public void InitMyPlanarModel(Vector3[] vertices, Vector2[] textureCordinate)
        {
            this._Vertices = new VertexPositionTexture[vertices.Length];
            for(int i = 0; i < vertices.Length; i++)
            {
                this._Vertices[i] = new VertexPositionTexture(vertices[i], textureCordinate[i]);
            }
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            MyEffect.CurrentTechnique = MyEffect.Techniques["Technique1"];
            MyEffect.Parameters["World"].SetValue(Matrix.CreateScale(this.Scale)
                                        * Matrix.CreateTranslation(this.Position)
                                        * this.Rotation);
            MyEffect.Parameters["View"].SetValue(camera.View);
            MyEffect.Parameters["Projection"].SetValue(camera.Projection);
            MyEffect.Parameters["Texture"].SetValue(_Texture);

            foreach (EffectPass pass in MyEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList,
                                                                         this._Vertices,
                                                                         0,
                                                                         2,
                                                                         VertexPositionTexture.VertexDeclaration);

            }
        }

        public override void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {
            if(this.IsAnimate)
                this._Rotation = Matrix.CreateRotationY(MathHelper.ToRadians((float)gameTime.TotalGameTime.TotalMilliseconds / (float)gameTime.ElapsedGameTime.TotalMilliseconds));
        }
    }
}
