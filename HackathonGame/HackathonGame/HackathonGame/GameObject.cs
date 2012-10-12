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
        Room room;

        public float Left { get { return position.X; } }
        public float Right { get { return (position.X + size.X); } }
        public float Top { get { return position.Y; } }
        public float Bottom { get { return (position.Y + size.Y); } }

        public GameObject(Vector2 position, Vector2 velocity, Vector2 size, Texture2D texture, Room room)
        {
            this.position = position;
            this.velocity = velocity;
            this.texture = texture;
            this.size = size;
            this.room = room;
        }

        public virtual void Update()
        {
            
        }

        protected virtual void Move()
        {
            foreach (GameObject block in room.blocks)
            {
            }
        }

        public virtual void Draw(SpriteBatch spr)
        {
            spr.Draw(texture, new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.size.X, (int)this.size.Y), null, Color.White);
        }
    }
}
