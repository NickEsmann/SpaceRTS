using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceRTS
{
    class Tile : GameObject
    {

        public Tile(int x, int y, int tileSize)
        {
            position = new Vector2(x * tileSize, y* tileSize);
        }

        public override void LoadContent(ContentManager content)
        {
            content.Load<Texture2D>("Map/scifiTile_41");
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
