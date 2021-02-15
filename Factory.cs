using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRTS
{
    class Factory : GameObject
    {
        public Factory(Vector2 position)
        {
            sprite = GameWorld.sprites["Factory"];
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
