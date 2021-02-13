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
        public int Lv;
        private int GoldCapasity;
        private int CurrentGold;
        private float timer = 0.0f;
        private float cooldownTime = 2;
        public static Vector2 positionHG;

        public Headquarter(Vector2 position)
        {
            sprite = GameWorld.sprites["HQ"];
            this.position = position;
            color = Color.White;
        }

        public void AddGold(int Gold)
        {
            if (CurrentGold <= GoldCapasity)
            {
                Lv++;
                CurrentGold = 0;
                GoldCapasity *= 2;
            }
            else
                CurrentGold += Gold;
        }

        public override void LoadContent(ContentManager contentManager)
        {
        }

        public override void Update(GameTime gametime)
        {
            if (timer < cooldownTime + 1)
            {
                timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Worker && timer > cooldownTime)
            {
                AddGold(Worker.currentGold);
                timer = 0;
            }
        }
    }
}