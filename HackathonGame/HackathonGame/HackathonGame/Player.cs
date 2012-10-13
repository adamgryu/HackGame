using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace HackathonGame
{
    public class Player : GameObject
    {
        int MAX_X_SPEED = 10;
        int MAX_Y_SPEED = 20;
        float ACCELERATION = 1.4f;
        float FRICTION = 0.9f;
        float WALL_SLOWDOWN = 0.25f;
        const float HOLD_SPEED = 1.5f;
        private int JUMP_LET_GO_SPEED = -20;
        const float WALL_JUMP_PUSH = 8;
        const float WALL_JUMP_UP = 9;
        const float STICKY = 5;

        Keys KeyUp;
        Keys KeyDown;
        Keys KeyLeft;
        Keys KeyRight;

        Boolean OnGround = false;
        bool OnGroundPrev = false;
        public int Health = 1;

        int wallTimer = 0;
        bool lastWallLeft = false;

        AnimationSet aniSet;
        bool canHitBlock = false;

        public Player(Room room, Vector2 position) :
            base(room, position, Vector2.Zero, new Vector2(48, 48), TextureBin.Get("dude2_f1"))
        {
            KeyUp = Keys.Space;
            KeyDown = Keys.Down;
            KeyLeft = Keys.Left;
            KeyRight = Keys.Right;
            aniSet = new AnimationSet(this);
        }

        public override void Update()
        {
            aniSet.Update();

            if (this.Top > room.camera.Y + Engine.screenResolution.Y || this.Right < 0 || this.Left > Engine.screenResolution.X)
                this.Health = 0;

            if (this.Left < 0 && velocity.X < -1)
                velocity.X = -1;
            if (this.Right > Engine.screenResolution.X && velocity.X > 1)
                velocity.X = 1;
                

            // Movement
            if (Input.IsKeyDown(KeyLeft))
            {
                this.velocity.X -= ACCELERATION;
                aniSet.current = aniSet.backward;
            }
            else if (Input.IsKeyDown(KeyRight))
            {
                this.velocity.X += ACCELERATION;
                aniSet.current = aniSet.forward;
            }

            // Limit the velocity.
            this.velocity.X = MathHelper.Clamp(this.velocity.X, -MAX_X_SPEED, MAX_X_SPEED);
            this.velocity.Y = MathHelper.Clamp(this.velocity.Y, -MAX_Y_SPEED, MAX_Y_SPEED);

            this.OnGround = false;
            this.hitWallRight = false;
            this.hitWallLeft = false;

            Move();
            if (this.OnGround)
            {
                canHitBlock = true;
                this.aniSet.current = aniSet.fStand;
                this.velocity.Y += STICKY;
                if (Input.IsKeyDown(KeyUp))
                {
                    this.velocity.Y = -11;
                    this.OnGround = false;
                    this.OnGroundPrev = false;
                    SoundBin.Play("jump");
                }
                this.velocity.X = MathExtra.ApplyFriction(this.velocity.X, FRICTION);
            }
            else if (OnGroundPrev)
            {
                this.velocity.Y -= STICKY;
            }

            if (Input.IsKeyUp(KeyUp) && this.velocity.Y <= JUMP_LET_GO_SPEED)
                this.velocity.Y = -JUMP_LET_GO_SPEED;


            if (this.hitWallLeft || this.hitWallRight)
            {
                canHitBlock = true;
                if (Input.IsKeyDown(KeyUp))
                    SoundBin.Play("jump");


                if (this.hitWallRight)
                {
                    this.lastWallLeft = false;
                    aniSet.current = aniSet.forwardPick;
                }
                if (this.hitWallLeft)
                {
                    this.lastWallLeft = true;
                    aniSet.current = aniSet.backwardPick;
                }
                wallTimer = 10;
            }
            wallTimer--;

            if (Input.IsKeyDown(KeyUp) && wallTimer > 0)
            {
                this.velocity.Y = -WALL_JUMP_UP;
                if (!lastWallLeft)
                    this.velocity.X = -WALL_JUMP_PUSH;
                else
                    this.velocity.X = WALL_JUMP_PUSH;
            }


            if (hitWallLeft || hitWallRight)
            {
                // if the player is hitting against the wall, slow them down until they stop.
                if (velocity.Y > HOLD_SPEED)
                {
                    velocity.Y -= WALL_SLOWDOWN;
                    if (velocity.Y < HOLD_SPEED) velocity.Y = HOLD_SPEED;
                }
                else if (velocity.Y < HOLD_SPEED)
                {
                    velocity.Y += WALL_SLOWDOWN;
                    if (velocity.Y > HOLD_SPEED) velocity.Y = HOLD_SPEED;
                }
            }
            else
            {
                // Add gravity to player
                this.velocity.Y += 0.5f;
            }

            base.Update();

            OnGroundPrev = OnGround;
        }

        protected override void HitCeiling(GameObject topHit)
        {
            if (topHit is Block)
            {
                if (canHitBlock)
                {
                    if (--((Block)topHit).life < 0)
                        room.blocks.BufferRemove(topHit);
                    canHitBlock = false;
                }
            }
            base.HitCeiling(topHit);
        }

        protected override void HitGround()
        {
            this.OnGround = true;
            base.HitGround();
        }

        public override Vector2 Move()
        {
            return base.Move();
        }

        public override void Draw(SpriteBatch spr)
        {
            this.texture = aniSet.GetTexture();
            base.Draw(spr);
        }
    }
}
