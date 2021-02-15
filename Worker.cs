using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SpaceRTS
{
    internal class Worker : GameObject
    {
        private int id;
        private Thread t;
        private int goldCap = 300;
        public static int currentGold;

        public Worker(int id)
        {
            this.id = id;
            t = new Thread(new ThreadStart(Work));
            t.Start();
            color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Worker");
        }

        public override void OnCollision(GameObject other)
        {
        }

        public void Work()
        {
            if (currentGold <= goldCap)
            {
                //Gå til HQ
                if (position.X < Headquarter.positionHG.X)
                    position.X += 10;
                else
                    position.X -= 10;

                if (position.Y < Headquarter.positionHG.Y)
                    position.Y += 10;
                else
                    position.Y -= 10;
            }
            else
            //Gå til Mine
            if (position.X < Mine.minePosition.X)
                position.X += 10;
            else
                position.X -= 10;

            if (position.Y < Mine.minePosition.Y)
                position.Y += 10;
            else
                position.Y -= 10;
        }

        public override void Update(GameTime gameTime)
        {
            Work();
        }
    }
}