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

        private static Semaphore MySemaphore = new Semaphore(0, 5);
        

        //private Thread MineThread;

        public Mine(Vector2 position)
        {
            this.position = position;
            //MineThread.IsBackground = true;
            minePosition = position;
            color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Mine");
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


        //    public static void Workline()
        //    {
        //        for (int i = 1; 1 <= 5; i++)
        //        {
        //            new Thread(Enter).Start(i);
        //        }

        //        Thread.Sleep(500);
        //        MySemaphore.Release(5);
        //    }

        //    static void Enter(object Worker)
        //    {
        //        MySemaphore.WaitOne();
        //        Thread.Sleep(1000 * (int)Worker);
        //        MySemaphore.Release();
        //    }

    }
}