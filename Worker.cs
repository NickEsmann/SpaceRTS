using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SpaceRTS
{
    internal class Worker : GameObject
    {
        private bool isDead = false;
        private int id;
        private Thread t;
        private int goldCap;
        public static int currentGold;
        private float speed = 5;
        private float timer;
        private float coolDown = 5;
        private int stamina = 100;
        private Vector2 chaseLine;
        private float deltaTime;
        private float cooldownTime = 50;
        private bool working = false;
        private bool sleeping = false;

        public Worker(int id)
        {
            sprite = GameWorld.sprites["Worker"];
            this.id = id;
            goldCap = 300;
            currentGold = 0;
            t = new Thread(new ThreadStart(Work));
            t.IsBackground = true;
            t.Start();
            color = Color.White;
            position = new Vector2(20, 20);
            scale = new Vector2(1, 1);
        }

        public override void LoadContent(ContentManager content)
        {
            //sprite = content.Load<Texture2D>("Worker");
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Mine)
            {
                working = true;
            }

            if (other is Headquarter)
            {
                sleeping = true;
            }
        }

        public void Work()
        {
            while (!isDead)
            {
                if (working)
                {
                    Mine.currentGold -= goldCap;
                    currentGold = goldCap;
                    speed = 0;
                    Thread.Sleep(5000);
                    speed = 5;
                    working = false;
                }
                if (sleeping)
                {
                    Headquarter.CurrentGold += currentGold;
                    currentGold = 0;
                    speed = 0;
                    Thread.Sleep(5000);
                    speed = 5;
                    sleeping = false;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (deltaTime >= coolDown)
                deltaTime = 0;
            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Debug.WriteLine(deltaTime);
            if (currentGold >= goldCap)
            {
                //Gå til HQ
                if (position != Headquarter.positionHG)
                {
                    chaseLine = Headquarter.positionHG - position;
                    chaseLine.Normalize();
                    position += chaseLine * speed * deltaTime;
                }
            }
            else
            {
                //Gå til Mine
                if (Mine.minePosition != position)
                {
                    chaseLine = Mine.minePosition - position;
                    chaseLine.Normalize();
                    position += chaseLine * speed * deltaTime;
                }
            }

            //if (timer < coolDown + 1)
            //{
            //    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //}

            //if (timer > coolDown)
            //{
            //    stamina--;
            //    timer = 0;

            //    if (timer < coolDown + 1)
            //    {
            //        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //    }
            //}
        }
    }
}