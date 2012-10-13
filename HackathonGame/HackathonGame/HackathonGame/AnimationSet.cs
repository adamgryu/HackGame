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
        public Animation current;

        public Animation forward;
        public Animation backward;
        public Animation forwardPick;
        public Animation backwardPick;

        Player player;

        int timer = 0;
        private float angle;

        public AnimationSet(Player player)
        {
            this.player = player;
            this.forward = new Animation(TextureBin.Get("pfront"), 1, 8, 2);
            this.backward = new Animation(TextureBin.Get("pback"), 1, 8, 2);
            this.forwardPick = new Animation(TextureBin.Get("pfrontgrab"), 1, 8, 2);
            this.backwardPick = new Animation(TextureBin.Get("pbackgrab"), 1, 8, 2);

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
