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
        private bool isDead = false;
        private int id;
        private Thread t;
        private int goldCap = 300;
        public static int currentGold;
        private int speed = 10;

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
            while (!isDead)
            {
                if (currentGold <= goldCap)
                {
                    //Gå til HQ
                    if (position.X < Headquarter.positionHG.X)
                        position.X += speed;
                    else
                        position.X -= speed;

                    if (position.Y < Headquarter.positionHG.Y)
                        position.Y += speed;
                    else
                        position.Y -= speed;
                }
                else
                {
                    //Gå til Mine
                    if (position.X < Mine.minePosition.X)
                        position.X += speed;
                    else
                        position.X -= speed;

                    if (position.Y < Mine.minePosition.Y)
                        position.Y += speed;
                    else
                        position.Y -= speed;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}