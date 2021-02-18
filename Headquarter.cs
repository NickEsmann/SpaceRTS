using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRTS
{
    internal class Headquarter : GameObject
    {
        public static int GoldCapasity = 1000;
        public static int CurrentGold = 0;
        public static Vector2 positionHG;

        public Headquarter(Vector2 position)
        {
            sprite = GameWorld.sprites["HQ"];
            this.position = position;
            color = Color.White;
            positionHG = position;
            scale = new Vector2(1, 1);
        }

        public override void LoadContent(ContentManager contentManager)
        {
        }

        public override void Update(GameTime gametime)
        {
        }

        public override void OnCollision(GameObject other)
        {
        }
    }
}