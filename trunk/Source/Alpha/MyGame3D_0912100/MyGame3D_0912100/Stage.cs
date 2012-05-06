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

        private PlanarModel _Ground;
        private Vector3 CAMERAPOSITION = new Vector3(0, -30, 5); //vị trí cam
        private Vector3 CAMERATARGET = new Vector3(0, 0, 0); //nhìn tới điểm đó
        private Vector3 CAMERAUPVECTOR = Vector3.Up;
        private float NEARPLANEDISTANCE = 10;
        private float FARPLANEDISTANCE = 1000;
        private float FIELDOFVIEW = MathHelper.PiOver4;
        private float ASPECTRATIO = 1;

        private Camera _Camera;
        //private MAP //chua co map :D

        public Stage(ContentManager content, My3DGameCharacter playerCharacter, My3DGameCharacter computerCharacter, string MapName)
        {
            _PlayerCharacter = playerCharacter;
            _ComputerCharacter = computerCharacter;

            _Ground = new PlanarModel(content, "Map\\Ground", new Vector2(50, 50), 1f, new Vector3(0, 0, 0), Matrix.Identity);
            _Ground.IsAnimate = false;
            _Camera = new PerspectiveCamera(
                this.CAMERAPOSITION,
                this.CAMERATARGET,
                this.CAMERAUPVECTOR,
                this.NEARPLANEDISTANCE,
                this.FARPLANEDISTANCE,
                this.FIELDOFVIEW,
                this.ASPECTRATIO);
        }

        public override void Update(GameTime gameTime, KeyboardState kbs, MouseState ms)
        {
            //_PlayerCharacter.Update(gameTime, kbs, ms);
            _ComputerCharacter.Update(gameTime, kbs, ms);
            _Ground.Update(gameTime, kbs, ms);
            Vector3 newCameraPos = _Camera.CameraPosition;
            if(kbs.IsKeyDown(Keys.Up))
            {
                newCameraPos.Y++;
            }
            if (kbs.IsKeyDown(Keys.Down))
            {
                newCameraPos.Y--;
            }
            if (kbs.IsKeyDown(Keys.Left))
            {
                newCameraPos.X--;
            }
            if (kbs.IsKeyDown(Keys.Right))
            {
                newCameraPos.X++;
            }
            if (kbs.IsKeyDown(Keys.Z))
            {
                newCameraPos.Z--;
            }
            if (kbs.IsKeyDown(Keys.X))
            {
                newCameraPos.Z++;
            }
            _Camera.CameraPosition = newCameraPos;
            _Camera.Update(gameTime, kbs, ms);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Effect effect, Camera camera)
        {
            _Ground.Draw(gameTime, graphicsDevice, spriteBatch, effect, _Camera);
            //_PlayerCharacter.Draw(gameTime, graphicsDevice, spriteBatch, effect, _Camera);
            _ComputerCharacter.Draw(gameTime, graphicsDevice, spriteBatch, effect, _Camera);
            this.DrawCordinate(graphicsDevice);
        }

        private void DrawCordinate(GraphicsDevice graphicsDevice)
        {
            BasicEffect effect = new BasicEffect(graphicsDevice);
            effect.VertexColorEnabled = true;
            effect.Projection = _Camera.Projection;
            effect.View = _Camera.View;
            effect.World = Matrix.Identity;


            VertexPositionColor[] vertices = new VertexPositionColor[6];
            vertices[0] = new VertexPositionColor(new Vector3(-100, 0, 0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(100, 0, 0), Color.GreenYellow);
            vertices[2] = new VertexPositionColor(new Vector3(0, -100, 0), Color.Blue);
            vertices[3] = new VertexPositionColor(new Vector3(0, 100, 0), Color.White);
            vertices[4] = new VertexPositionColor(new Vector3(0, 0, -100), Color.Red);
            vertices[5] = new VertexPositionColor(new Vector3(0, 0, 100), Color.Green);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList,
                vertices, 0, 3, VertexPositionColor.VertexDeclaration);
            }

        }
    }
}
