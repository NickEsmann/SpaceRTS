using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRTS
{
    internal class Headquarter
    {
        public int Lv;
        private int GoldCapasity;
        private int CurrentGold;
        private Texture2D Sprite;

        public Headquarter()
        {
        }

        public void AddGold(int Gold)
        {
            if (CurrentGold <= GoldCapasity)
            {
                Lv++;
                CurrentGold = 0;
                GoldCapasity = GoldCapasity * 2;
            }
            else
                CurrentGold += Gold;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Sprite = contentManager.Load<Texture2D>("scifiStructure_07");
        }
    }
}