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

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 size;
        public Texture2D texture;
        protected Room room;

        public float Left { get { return position.X; } set { this.position.X = value; } }
        public float Right { get { return (position.X + size.X); } set { this.position.X = value - size.X; } }
        public float Top { get { return position.Y; } set { this.position.Y = value; } }
        public float Bottom { get { return (position.Y + size.Y); } set { this.position.Y = value - size.Y; } }
        public Vector2 Center { get { return position + size / 2; } }

        public bool hitWallLeft = false;
        public bool hitWallRight = false;

        public GameObject(Room room, Vector2 position, Vector2 velocity, Vector2 size, Texture2D texture)
        {
            this.position = position;
            this.velocity = velocity;
            this.texture = texture;
            this.size = size;
            this.room = room;
        }

        public GameObject(Room room, Vector2 v1, Vector2 v2)
        {
            this.position = v1;
            this.size = v2 - v1;
            this.room = room;
            this.texture = TextureBin.Get("pixel");
            this.velocity = Vector2.Zero;
        }


        public virtual void Update()
        {

        }

        public virtual Vector2 Move()
        {
            return MoveX() + MoveY();
        }

        public Vector2 MoveY()
        {
            Vector2 prevPos = this.position;

            float maxY = room.Size.Y;
            float minY = float.NegativeInfinity;
            GameObject topHit = null;
            foreach (GameObject other in room.blocks)
            {
                if (this == other)
                    continue;
                if (this.Right > other.Left && this.Left < other.Right)
                {
                    if (other.Top > this.Center.Y && other.Top < maxY)
                        maxY = other.Top;
                    if (other.Bottom < this.Center.Y && other.Bottom > minY)
                    {
                        minY = other.Bottom;
                        topHit = other;
                    }
                }
            }
            this.position.Y += this.velocity.Y;

            if (this.Top < minY)
            {
                this.Top = minY;
                this.velocity.Y = 0;
                HitCeiling(topHit);
            }
            if (this.Bottom > maxY)
            {
                this.Bottom = maxY;
                this.velocity.Y = 0;
                HitGround();
            }

            return this.position - prevPos;
        }

        protected virtual void HitCeiling(GameObject topHit)
        {
            // Nothing.
        }

        public Vector2 MoveX()
        {
            Vector2 prevPos = this.position;

            float maxX = float.PositiveInfinity;
            float minX = float.NegativeInfinity;
            foreach (GameObject other in room.blocks)
            {
                if (other == this)
                    continue;

                if (this.Bottom > other.Top && this.Top < other.Bottom)
                {
                    if (other.Right < this.Center.X && other.Right > minX)
                        minX = other.Right;
                    if (other.Left > this.Center.X && other.Left < maxX)
                        maxX = other.Left;
                }
            }
            this.position.X += this.velocity.X;

            if (this.Right > maxX)
            {
                this.Right = maxX;
                this.velocity.X = 0;
                hitWallRight = true;
            }
            if (this.Left < minX)
            {
                this.Left = minX;
                this.velocity.X = 0;
                hitWallLeft = true;
            }

            return this.position - prevPos;
        }

        public bool IntersectsWith(GameObject other)
        {
            return this.Left < other.Right && this.Right > other.Left && this.Top < other.Bottom && this.Bottom > other.Top;
        }

        protected virtual void HitGround()
        {
            // Do nothing.
        }

        public virtual void Draw(SpriteBatch spr)
        {
            spr.Draw(texture, new Rectangle((int)(this.position.X - room.camera.X), (int)(this.position.Y - room.camera.Y), (int)this.size.X, (int)this.size.Y), null, Color.White);
        }
    }
}
