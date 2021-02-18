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
        private float speed = 0.1f;
        private float timer;
        private float coolDown = 2;
        private int stamina = 100;
        private Vector2 chaseLine;
        private float deltaTime;
        private float cooldownTime = 50;
        private bool working = false;
        private bool sleeping = false;
        private object key = new object();

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
            position = Headquarter.positionHG;
            scale = new Vector2(1, 1);
        }

        public override void LoadContent(ContentManager content)
        {
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
                lock(key)
                {
                    if (working)
                    {
                        Mine.currentGold -= goldCap;
                        currentGold = goldCap;
                        Thread.Sleep(5000);
                        speed = 0.01f;
                        working = false;
                    }
                }
                lock(key)
                {
                    if (sleeping)
                    {
                        Headquarter.CurrentGold += currentGold;
                        currentGold = 0;
                        Thread.Sleep(5000);
                        speed = 0.01f;
                        sleeping = false;
                    }
                }
                
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (deltaTime >= coolDown)
                deltaTime = 0;
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
            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}