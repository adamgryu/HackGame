using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HackathonGame
{
    public class TopBlock : Block 
    {
        public TopBlock(Room room, Vector2 position, Vector2 Size)
            : base(room, position, Size)
        {
            
        }

        public override void Update()
        {
            velocity.Y += 0.1f;
            Move();

            if (this.IntersectsWith(room.player))
                Engine.room = new Room(Engine.screenResolution);

            base.Update();
        }
    }
}
