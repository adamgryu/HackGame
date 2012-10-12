using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HackathonGame
{
    public class Player : GameObject
    {
        Keys Up;
        Keys Down;
        Keys Left;
        Keys Right;

        public Player(Vector2 position, Room room) :
            base(position, Vector2.Zero, TextureBin.Get("player"))
        {
            Up = Keys.Up;
            Down = Keys.Down;
            Left = Keys.Left;
            Right = Keys.Right;
        }

        public override void Update()
        {


            base.Update();
        }
    }
}
