using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceRTS
{
    internal class Mine : GameObject
    {
        public int GoldCapasity;
        private float timer;
        private float cooldownTime = 2;
        public static new Vector2 position;
        static Semaphore MySemaphore = new Semaphore(0, 5);
        Thread MineThread;

        

        public Mine()
        {
            sprite = GameWorld.sprites["Mine"];
            MineThread.IsBackground = true;


        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Worker && timer > cooldownTime)
            {
                Worker.currentGold += 100;
            }
            timer = 0;
        }

        public override void Update(GameTime gametime)
        {
            if (timer < cooldownTime + 1)
            {
                timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            }
        }

        static void Workline()
        {
            for (int i = 1; 1 <= 5; i++)
            {
                new Thread(Enter).Start(i);
            }

            Thread.Sleep(500);
            MySemaphore.Release(5);


        }

        static void Enter(object Worker)
        {
            MySemaphore.WaitOne();
            Thread.Sleep(1000 * (int)Worker);
            MySemaphore.Release();
        }
    }
}