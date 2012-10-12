using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HackathonGame
{
    public class Room
    {
        public Vector2 Size;

        Player player;

        public Room(Vector2 Size)
        {
            this.Size = Size;
            this.player = new Player(Vector2.Zero);
        }

        public void Update()
        {
            this.player.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
