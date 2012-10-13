using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackathonGame
{
    public class AnimationSet
    {
        Animation current;

        Animation forward;
        Animation backward;
        Animation forwardPick;
        Animation backwardPick;

        Player player;

        int timer = 0;
        private float angle;

        public AnimationSet(Player player)
        {
            this.player = player;
           // this.forward = new AnimationSet(TextureBin.Get("Right"), 5, 16, 2);
            //this.forward = new AnimationSet(TextureBin.Get("Left"), 5, 16, 2);
            //this.backward = new AnimationSet(TextureBin.GetTexture("Back"), 4, 16, 2);
            //this.up = new AnimationSet(TextureBin.GetTexture("Up"), 5, 16, 2);
            //this.down = new AnimationSet(TextureBin.GetTexture("Down"), 5, 16, 2);
            //this.launch = new AnimationSet(TextureBin.GetTexture("Launch"), 4, 16, 4);

            this.current = this.forward;
        }

        public void Update()
        {
            if (timer > 0)
                timer--;
            else
                this.current = forward;

            current.Update();
            /*if (player.onGround)
            {
                current.frames = 1;
            }
            else
            {
                current.frames = 5;
            }*/
        }

        public Texture2D GetTexture()
        {
            return current.GetTexture();
        }

        public Rectangle GetSourceRect()
        {
            return current.GetSourceRect();
        }

        internal void NewDirection(Vector2 direction)
        {
            /*
            timer = 10;
            angle = MathHelper.ToDegrees(MathHelper.WrapAngle(MathExtra.GetAngleFromVector(direction)));
            if (angle > -45 && angle < 45)
                current = forward;
            else if (angle > -120 && angle <= -45)
                current = up;
            else if (angle < -120 || angle > 110)
                current = backward;
            else if (angle > 45 && angle <= 110)
                current = down;
            if (angle > 110 && angle < 150)
                current = launch;
             */
        }
    }

}
