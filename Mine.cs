using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceRTS
{
    internal class Mine : GameObject
    {
        private Texture2D mine;
        public int GoldCapasity;
        private float timer;
        private float cooldownTime;

        public override void LoadContent(ContentManager content)
        {
            mine = content.Load<Texture2D>("PNG/Retina/Environment/scifiEnvironment_06");
        }

        public override void OnCollision(GameObject other)
        {
            /*
            if (other is Worker && timer > cooldownTime)
            {
                Worker.Gold = Worker.Gold + 100;
            }
            timer = 0;
            */
        }

        public override void Update(GameTime gametime)
        {
            if (timer < cooldownTime + 1)
            {
                timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}