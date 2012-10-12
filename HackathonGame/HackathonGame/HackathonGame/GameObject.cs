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
        public Vector2 Center { get { return position + size / 2;} }

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
            float maxX = room.Size.X;
            float minX = 0;
            foreach (GameObject other in room.blocks)
            {
                if (this.Bottom > other.Top && this.Top < other.Bottom)
                {
                    if (other.Right < this.Center.X && other.Right > minX)
                        minX = other.Right
                    if (other.Left > this.Center.X && other.Left > maxX)
                        maxX = other.Left;
                }
            }
            this.position.X += this.velocity.X;
            //if (this.velocity 
        }

        public virtual void Draw(SpriteBatch spr)
        {
            spr.Draw(texture, new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.size.X, (int)this.size.Y), null, Color.White);
        }
    }
}
