using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackathonGame
{
    public class BlockGroup : GameObject
    {
        const int BLOCK_NUM = 4;
        Block[] blocks = new Block[BLOCK_NUM];
        Vector2[] blocksOffset = new Vector2[BLOCK_NUM];

        public BlockGroup(Room room, Vector2 pos)
            : base(room, pos, Vector2.Zero, Vector2.One * Room.GRIDSIZE, TextureBin.Get("pixel"))
        {
            for (int i = 0; i < BLOCK_NUM; i++)
            {
                blocks[i] = new Block(room, position, size);
                blocksOffset[i] = new Vector2(i, 0);

                blocks[i].velocity = this.velocity;
                blocks[i].position = this.position + blocksOffset[i] * Room.GRIDSIZE;
            }
            blocks[BLOCK_NUM - 1].position.Y += Room.GRIDSIZE;
            blocks[BLOCK_NUM - 1].position.X -= Room.GRIDSIZE;

        }

        public override void Update()
        {
            Vector2 originalPos = this.position;

            float moveX = this.velocity.X;
            for (int i = 0; i < BLOCK_NUM; i++)
            {
                blocks[i].velocity = this.velocity;
                float diff = blocks[i].MoveX().X;
                if (Math.Abs(diff) < Math.Abs(moveX))
                    moveX = diff;
                blocks[i].position.X -= diff;
            }
            for (int i = 0; i < BLOCK_NUM; i++)
            {
                blocks[i].position.X += moveX;
            }

            this.velocity.Y += 0.1f;
            float moveY = this.velocity.Y;
            for (int i = 0; i < BLOCK_NUM; i++)
            {
                blocks[i].velocity = this.velocity;
                float diff = blocks[i].MoveY().Y;
                if (Math.Abs(diff) < Math.Abs(moveY))
                    moveY = diff;
                blocks[i].position.Y -= diff;
            }
            for (int i = 0; i < BLOCK_NUM; i++)
            {
                blocks[i].position.Y += moveY;
            }
        }

        public override void Draw(SpriteBatch spr)
        {
            foreach (Block b in blocks)
            {
                b.Draw(spr);
            }
        }
    }
}
