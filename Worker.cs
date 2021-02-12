﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRTS
{
    internal class Worker : GameObject
    {
        private int goldCap;
        public static int currentGold;

        public Worker()
        {
            color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Worker");
        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (currentGold <= goldCap)
            {
                //Gå til HQ
                if (position.X < Headquarter.position.X)
                    position.X++;
                else
                    position.X--;

                if (position.Y < Headquarter.position.Y)
                    position.Y++;
                else
                    position.Y--;
            }
            else
            //Gå til HQ
            if (position.X < Mine.position.X)
                position.X++;
            else
                position.X--;

            if (position.Y < Mine.position.Y)
                position.Y++;
            else
                position.Y--;
        }
    }
}