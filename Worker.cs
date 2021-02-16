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
        private GameWorld gameworld;
        private bool isDead = false;
        public int id;
        private Thread t;
        private int goldCap;
        public static int currentGold;
        private float speed = 1;
        private float timer;
        private float coolDown = 100;
        private int stamina = 100;
        private Vector2 chaseLine;
        private float deltaTime;
        private float cooldownTime = 50;
        private bool working = false;
        private bool sleeping = false;

        public Worker(int id)
        {
            this.id = id;
            goldCap = 300;
            currentGold = 0;
            t = new Thread(new ThreadStart(Work));
            t.IsBackground = true;
            t.Start();
            color = Color.White;
            position = new Vector2(500, 500);
            scale = new Vector2(1, 1);
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Worker");
            gameworld = new GameWorld();
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Headquarter)
            {
                sleeping = true;
            }

            if (other is Mine)
            {
                working = true;
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

                    Thread.Sleep(1000);
                    working = false;
                }
                if (sleeping)
                {
                    Headquarter.CurrentGold += currentGold;
                    currentGold = 0;

                    Thread.Sleep(1000);
                    sleeping = false;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentGold >= goldCap)
            {
                //Gå til HQ
                if (position != new Vector2(200, 200))
                {
                    chaseLine = new Vector2(200, 200) - position;
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
            if (!working)
                position += chaseLine * speed * deltaTime;

            if (timer < cooldownTime + 10)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timer > cooldownTime)
            {
                stamina--;
                timer = 0;
            }

            if (stamina <= 0)
            {
                isDead = true;
                gameworld.Destroy(this);
            }
        }
    }
}