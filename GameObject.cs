using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SpaceRTS
{
    public abstract class GameObject
    {
        public Texture2D sprite;
        public Vector2 position;
        public Color color;
        public Vector2 origin;
        public float rotation;
        public int offsetX;
        public int offsetY;

        public GameObject()
        {
        }

        public virtual Rectangle Collision
        {
            get
            {
                return new Rectangle(
                       (int)position.X + offsetX,
                       (int)position.Y,
                       (int)sprite.Width,
                       (int)sprite.Height + offsetY
                   );
            }
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, 0f, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        public abstract void OnCollision(GameObject other);

        public void CheckCollision(GameObject other)
        {
            if (Collision.Intersects(other.Collision))
            {
                OnCollision(other);
            }
        }
    }
}