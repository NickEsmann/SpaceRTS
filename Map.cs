using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRTS
{
    class Map
    {
        private int row;
        private int col;
        private int gridSize;
        private List<GameObject> grid;
        public Map()
        {
            grid = new List<GameObject>();
            mapMaker();
        }


        public void LoadContent(ContentManager content)
        {
            foreach (GameObject go in grid)
            {
                go.LoadContent(content); 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in grid)
            {
                go.Draw(spriteBatch); 
            }
        }

        private void mapMaker()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    grid.Add(new Tile(row, col, gridSize));
                }
            }
        }
    }
}