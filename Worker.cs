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
        private float speed = 10;
        private float timer;
        private float coolDown = 50;
        private int stamina = 100;
        private int lifeEnergy;
        private Vector2 chaseLine;

        public Worker(int id)
        {
            this.id = id;
            t = new Thread(new ThreadStart(Work));

            t.Start();
            color = Color.White;
            position = new Vector2(20, 20);
            scale = new Vector2(1, 1);
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Worker");
        }

        public override void OnCollision(GameObject other)
        {
        }

        private void LifeEnergy()
        {
            if (currentGold <= goldCap)
            {
                if (lifeEnergy >= 0)
                {
                    lifeEnergy -= 1;
                }
                else
                {
                    isDead = false;
                }
            }
        }

        public void Work()
        {
            while (!isDead)
            {
                chaseLine = Headquarter.positionHG - position;
                chaseLine.Normalize();

                //if (currentGold <= goldCap)
                //{
                //    //Gå til HQ
                //    if (position.X < Headquarter.positionHG.X)
                //        position.X += speed;
                //    else
                //        position.X -= speed;

                //    if (position.Y < Headquarter.positionHG.Y)
                //        position.Y += speed;
                //    else
                //        position.Y -= speed;
                //}
                //else
                //{
                //    //Gå til Mine
                //    if (position.X < Mine.minePosition.X)
                //        position.X += speed;
                //    else
                //        position.X -= speed;

                //    if (position.Y < Mine.minePosition.Y)
                //        position.Y += speed;
                //    else
                //        position.Y -= speed;
                //}
            }
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += chaseLine * speed * deltaTime;

            if (timer < coolDown + 1)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timer > coolDown)
            {
                stamina--;
                timer = 0;
            }

            if (stamina <= 0)
            {
                isDead = true;
            }
        }
    }
}