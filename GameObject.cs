using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SpaceRTS
{
    abstract class GameObject
    {
        protected Texture2D sprite;
        protected Vector2 position;
        protected Color color;
        protected Vector2 origin;
        protected float rotation;


        public GameObject()
        {

        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, rotation, origin, 1, SpriteEffects.None, 1);
        }


    }
}
