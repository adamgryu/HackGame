using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackathonGame
{
    public class Snow
    {
        public Vector2 pos;
        public float angle;
        public float angleSpeed;

        public Snow(Vector2 pos)
        {
            this.pos = pos;
            this.angle = MathExtra.RandomFloat() * MathHelper.Pi;
            this.angleSpeed = MathExtra.RandomFloat() * 0.01f;
        }

        public virtual void Update(Room room)
        {
            this.pos.X += (float)Math.Sin(angle) * 3;
            this.pos.Y += 5f;
            if (this.pos.Y > Engine.screenResolution.Y)
            {
                room.snow.BufferRemove(this);
            }
            this.angle += this.angleSpeed;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureBin.Pixel, new Rectangle((int)pos.X, (int)pos.Y, 4, 4), Color.White * 0.8f);
        }
    }
}
