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
        public static Vector2 minePosition;
        private static Semaphore MySemaphore = new Semaphore(0, 5);

        public Mine(Vector2 position)
        {
            this.position = position;
            color = Color.White;
            minePosition = position;
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
        
        private static void Main()
        {
            for (int i = 1; 1 <= 5; i++)
            {
                new Thread(Enter).Start(i);
            }

            Thread.Sleep(500);
        }   
    }
}