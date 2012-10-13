using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackathonGame
{
    public class LavaSnow : Snow
    {
        float yVel = -10;
        float xVel = MathExtra.RandomFloat() * 6 - 3;
        public LavaSnow(Vector2 pos)
            : base(pos)
        {

        }

        public override void Update(Room room)
        {
            this.pos.X += xVel;
            this.pos.Y += yVel;
            yVel += 0.5f;
            if (this.pos.Y > Engine.screenResolution.Y)
                room.snow.BufferRemove(this);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureBin.Pixel, new Rectangle((int)pos.X, (int)pos.Y, 8, 8), Color.Red);
        }
    }
}
