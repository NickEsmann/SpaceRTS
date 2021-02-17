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
        private int GoldCapasity = 1000;
        private bool isDead = false;
        private int id;
        private Thread t;
        private int goldCap;
        public static int currentGold;
        private float speed = 10;
        private float timer;
        private float coolDown = 1;
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
            position = new Vector2(20, 20);
            scale = new Vector2(1, 1);
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Worker");
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Headquarter)
            {
                working = true;
            }

            if (other is Mine)
            {
                sleeping = true;
            }
        }

        //public void AddGold(int Gold)
        //{
        //    if (CurrentGold <= GoldCapasity)
        //    {
        //        Lv++;
        //        CurrentGold = 0;
        //        GoldCapasity *= 2;
        //    }
        //    else
        //        CurrentGold += Gold;
        //}

        public void Work()
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

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine(Headquarter.positionHG);
            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
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

                if (timer < coolDown + 1)
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}