using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceRTS
{
    class Barack : GameObject
    {
        public Barack(Vector2 position)
        {
            sprite = GameWorld.sprites["Barack"];
            this.position = position;
            color = Color.White;
            scale = new Vector2(1, 1);

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
