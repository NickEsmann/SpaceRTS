using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SpaceRTS
{
    class Barack : GameObject
    {
        private Vector2 barackPosition;
        static Mutex mSoldier = new Mutex();

        public Barack(Vector2 position)
        {
            sprite = GameWorld.sprites["Barack"];
            this.position = position;
            color = Color.White;
            scale = new Vector2(1, 1);
            barackPosition = position;
        }

        //public void SpawnSoldier()
        //{
        //    mSoldier.WaitOne();
        //    if (true)
        //    {
        //        if (Headquarter.CurrentGold == 50)
        //        {
        //            goldHolder = Headquarter.CurrentGold - 50;

        //            GameWorld.AddSoldier(new Soldier());
        //        }
        //    }
        //    Thread.Sleep(5000);
        //    mSoldier.ReleaseMutex();

        }

        #region Abstract Class
        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        #endregion
    }
}
