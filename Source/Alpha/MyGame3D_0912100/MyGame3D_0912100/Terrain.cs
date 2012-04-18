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
    public class TerrainModel : MyModel
    {
        private int _nCols;

        private int _nRows;

        private int _nVetices;

        private Vector2 _Size;

        private VertexPositionColor[] _Vetices;

        public TerrainModel(ContentManager content, string heightMapTexture, float scale, Vector3 position, Matrix rotation)
        {
            this.Scale = scale;
            this.Position = position;
            this.Rotation = rotation;
            Texture2D textureTemp = content.Load<Texture2D>(heightMapTexture);
            _nCols = textureTemp.Width - 1;
            _nRows = textureTemp.Height - 1;
            Color[] textureColors = new Color[textureTemp.Width * textureTemp.Height];
            textureTemp.GetData(textureColors);
            _nVetices = _nCols * _nRows * 6;
            _Vetices = new VertexPositionColor[_nVetices];
            /*
             * --------->x
             * A---B |
             * |   | |
             * D---C |
             *       y
             */
            
            for (int x = 0; x < textureTemp.Height; x++)
                for (int z = 0; z < textureTemp.Width; z++)
                {
                    _Vetices[x*z + 1] = new VertexPositionColor(new Vector3(x, textureColors[x * textureTemp.Height + z * textureTemp.Width].R/6, z), Color.White); // A
                    _Vetices[x * z + 1] = new VertexPositionColor(new Vector3(x + 1, textureColors[(x + 1)* textureTemp.Height + textureTemp.Width].R / 6, z), Color.White); //B
                    _Vetices[x * z + 1] = new VertexPositionColor(new Vector3(x, textureColors[x * textureTemp.Height + textureTemp.Width].R / 6, z), Color.White); //C

                    _Vetices[x * z + 1] = new VertexPositionColor(new Vector3(x, textureColors[x * textureTemp.Height + textureTemp.Width].R / 6, z), Color.White);
                    _Vetices[x * z + 1] = new VertexPositionColor(new Vector3(x, textureColors[x * textureTemp.Height + textureTemp.Width].R / 6, z), Color.White);
                    _Vetices[x * z + 1] = new VertexPositionColor(new Vector3(x, textureColors[x * textureTemp.Height + textureTemp.Width].R / 6, z), Color.White);


                }
        }
    }
}
