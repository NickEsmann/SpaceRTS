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
        public int GoldCapasity = 1000;
        private float timer;
        private float cooldownTime = 100;
        public static Vector2 minePosition;

        //private static Semaphore MySemaphore = new Semaphore(0, 5);
        public static new Vector2 position;

        private static Semaphore MySemaphore = new Semaphore(0, 5);
        private Thread MineThread;

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

        /*
        private static void Working()
        static void Workline()
        {
            for (int i = 1; 1 <= 5; i++)
            {
                //new Thread(Enter).Start(i);
            }

            //Thread.Sleep(500);
            Thread.Sleep(500);
            MySemaphore.Release(5);
        }

        static void Enter(object Worker)
        {
            MySemaphore.WaitOne();
            Thread.Sleep(1000 * (int)Worker);
            MySemaphore.Release();
        }
        */
    }
}