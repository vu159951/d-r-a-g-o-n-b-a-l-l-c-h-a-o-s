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

        public TerrainModel(ContentManager content, string heightMapTexture, float scale, Vector3 position, Matrix rotation)
        {
            this.Scale = scale;
            this.Position = position;
            this.Rotation = rotation;
            Texture2D textureTemp = content.Load<Texture2D>(heightMapTexture);
            _nCols = textureTemp.Width - 1;
            _nRows = textureTemp.Height - 1;
            Vector3[] textureColors = new Vector3[textureTemp.Width * textureTemp.Height];
            textureTemp.GetData(textureColors);
            Color[,] colors = new Color[textureTemp.Width, textureTemp.Height];
            Color c = new Color();
            for (int i = 0; i < textureTemp.Height; i++)
                for (int j = 0; j < textureTemp.Width; j++) 
                    colors[i, j] = new Color(textureColors[i * textureTemp.Height + textureTemp.Width]);
        }
    }
}
