using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HackathonGame
{
    public class GameObject
    {
        Vector2 position;
        Vector2 velocity;
        Vector2 size;
        Texture2D texture;

        public GameObject(Vector2 position, Vector2 velocity, Vector2 size, Texture2D texture)
        {
            this.position = position;
            this.velocity = velocity;
            this.texture = texture;
            this.size = size;
        }

        public virtual void Update()
        {

        }

        protected virtual void Move()
        {

        }

        public virtual void Draw(SpriteBatch spr)
        {
            spr.Draw(texture, new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.size.X, (int)this.size.Y), null, Color.White);
        }
    }
}
