using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SpaceRTS
{
    class Bank : GameObject
    {
        public Bank(Vector2 position)
        {
            sprite = GameWorld.sprites["Bank"];
            this.position = position;
            color = Color.White;
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void OnCollision(GameObject other)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
