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
        public static int Lv = 0;
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
            if (CurrentGold >= GoldCapasity)
            {
                Lv++;
                CurrentGold = 0;
                GoldCapasity *= 2;
            }
        }

        public override void OnCollision(GameObject other)
        {
        }
    }
}