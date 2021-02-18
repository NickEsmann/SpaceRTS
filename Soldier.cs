using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace SpaceRTS
{
    public class Soldier : GameObject
    {
        #region fields
        private Vector2 soldierPosition;
        private Thread s;
        #endregion

        public Soldier()
        {
            sprite = GameWorld.sprites["Soldier"];
            position = soldierPosition;
            s = new Thread(MethodSoldier);
            s.IsBackground = true;
        }

        private void MethodSoldier()
        {
            while (true)
            {

            }
        }

        public void InternalThreadStart()
        {
            s.Start();
        }


        #region Abstract Class
        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(GameObject other)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
