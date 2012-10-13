using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HackathonGame
{
    public class Block : GameObject 
    {
        const float HORIZONTAL_MOVE_SPEED = 0.5f;

        public int Size;
        const int BLOCK_LIFE = 4;
        public int life = BLOCK_LIFE;

        public Block(Room room, Vector2 position, Vector2 Size)
            : base(room, position, Vector2.Zero, Size, TextureBin.Get("pixel"))
        {
            
        }

        public override void Update()
        {
            velocity.Y += 0.01f;
            if (this.Top > room.camera.Y + Engine.screenResolution.Y)
                room.blocks.BufferRemove(this);

            if (this.velocity.Y > 6)
                this.velocity.Y = 6;


            /*if (Input.MousePosition.X < position.X + size.X / 2)
                velocity.X -= HORIZONTAL_MOVE_SPEED;
            if (Input.MousePosition.X > position.X + size.X / 2)
                velocity.X += HORIZONTAL_MOVE_SPEED;*/

            MoveY();

            base.Update();
        }

        public override void Draw(SpriteBatch spr)
        {
            spr.Draw(texture, new Rectangle((int)(this.position.X - room.camera.X), (int)(this.position.Y - room.camera.Y), (int)this.size.X, (int)this.size.Y), null, Color.White * (0.3f + 0.7f * (life / (float)BLOCK_LIFE)));
        }
    }
}
