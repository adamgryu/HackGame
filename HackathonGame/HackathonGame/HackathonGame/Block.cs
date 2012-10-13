using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HackathonGame
{
    public class Block : GameObject 
    {
        const float HORIZONTAL_MOVE_SPEED = 0.5f;

        public int Size;

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

            Move();

            base.Update();
        }
    }
}
