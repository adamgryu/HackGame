using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackathonGame
{
    public class Room
    {
        const int WALL_PADDING = 60;

        /// <summary>
        /// Interval between blocks falling.
        /// </summary>s
        const int BLOCK_INTERVAL = 100;

        /// <summary>
        /// Size of squares in the grid.
        /// </summary>
        public const int GRIDSIZE = 48;
        const int WALL_HEIGHT_SIZE = 200;
        
        public Vector2 Size;
        public Player player;
        public BufferedList<GameObject> blocks;
        public BufferedList<Snow> snow;
        public List<BlockGroup> blockGroups;
        public Vector2 camera = Vector2.Zero;

        public float lastBlockHeight = 0;
        float side1 = 0;
        float side2 = 0;
        int score = 0;

        public float wallLeftAngle = MathExtra.RandomFloat() * (float)MathHelper.TwoPi;
        public float wallRightAngle = MathExtra.RandomFloat() * (float)MathHelper.TwoPi;
        
        #region Timers
        int blockTimer = 0;
        #endregion
        public int mode = 0;

        public Room(Vector2 Size)
        {
            this.Size = Size;
            this.player = new Player(this, new Vector2(WALL_PADDING, Size.Y / 2));
            this.blocks = new BufferedList<GameObject>();
            this.blocks.Add(new Block(this, Vector2.Zero, new Vector2(WALL_PADDING, Size.Y)));
            this.blocks.Add(new Block(this, new Vector2(Size.X - WALL_PADDING, 0), new Vector2(WALL_PADDING, Size.Y)));
            this.blocks.Add(player);
            this.blockGroups = new List<BlockGroup>();
            this.snow = new BufferedList<Snow>();
        }

        public void Update()
        {

            snow.Add(new Snow(new Vector2(MathExtra.RandomFloat() * Engine.screenResolution.X, -8)));
            if (blockTimer % 3 == 0)
                snow.Add(new LavaSnow(new Vector2(MathExtra.RandomFloat() * Engine.screenResolution.X, Engine.screenResolution.Y - 16))); 
            foreach (Snow s in snow)
            {
                s.Update(this);
            }
            snow.ApplyBuffers();

            if (player.Health > 0)
            {
                // Camera pans upward.
                this.camera.Y -= 0.5f + (score / 800f);
                score = -(int)(camera.Y / 10);


                if (player.Top < camera.Y + 150)
                    camera.Y = player.Top - 150;

                // Spawns blocks.
                blockTimer++;
                if (blockTimer == BLOCK_INTERVAL)
                {

                    Random gridRandom = new Random();
                    float blockWidth = 128;
                    float choosenX = MathHelper.Lerp((float)(side1), (float)(Engine.screenResolution.X - side2 - blockWidth), (float)(gridRandom.NextDouble()));
                    blocks.Insert(0, new Block(this, new Vector2(choosenX, lastBlockHeight), new Vector2(blockWidth, 32)));
                    blockTimer = 0;
                }

                // Spawns SMART blocks.
                if (this.camera.Y - 64 < lastBlockHeight)
                {
                    GenerateNewBlocks();
                }

                //if (Input.IsKeyboardTapped(Keys.A))
                //{
                //    blockGroups.Add(new BlockGroup(this, Input.MousePosition));
                //}

                foreach (GameObject b in blocks)
                    b.Update();
                blocks.ApplyBuffers();
            }
            else if (Input.IsKeyboardTapped(Keys.Enter))
                Engine.room = new Room(Engine.screenResolution);
            
        }

        private void GenerateNewBlocks()
        {
            wallLeftAngle += 0.19f;
            wallRightAngle += 0.54f;
            float height = WALL_HEIGHT_SIZE;

            int lastMode = mode;
            mode = MathExtra.RandomInt(24);
            if (mode <= 1)
            {
                // HOLES
                side1 = -64;
                side2 = -64;
                height = 100;
            }
            else if (mode <= 2)
            {
                side1 = 100;
                side2 = 100;
                height = 64;
            }
            else if (mode <= 3)
            {
                side1 = -64;
                side2 = 140;
                height = 64;
            }
            else if (mode <= 4)
            {
                side1 = 120;
                side2 = -64;
                height = 64;
            }
            else if (mode <= 5)
            {
                side1 = 120;
                side2 = 120;
                height = 200;
            }
            else if (mode <= 6)
            {
                side1 = 30;
                side2 = 30;
                height = 200;
            }
            else if (mode <= 7)
            {
                side1 = 200;
                side2 = 20;
                height = 48;
                if (lastMode == 8)
                {
                    side1 = -64;
                    side2 = -64;
                }
            }
            else if (mode <= 8)
            {
                side1 = 20;
                side2 = 200;
                height = 48;
                if (lastMode == 7)
                {
                    side1 = -64;
                    side2 = -64;
                }
            }
            else if (mode <= 9)
            {
                side1 = -64;
                side2 = -64;
                height = 128;
                this.blocks.Add(new GameObject(this, new Vector2(180, lastBlockHeight - 90), new Vector2(Engine.screenResolution.X - 180, lastBlockHeight - 30)));
            }
            else if (mode <= 10)
            {
                side1 = -64;
                side2 = -64;
                height = 400;
                this.blocks.Add(new GameObject(this, new Vector2(210, lastBlockHeight - 400 + 33), new Vector2(Engine.screenResolution.X - 210, lastBlockHeight - 33)));
            }
            else if (mode <= 11)
            {
                // HOLES
                side1 = -64;
                side2 = -64;
                height = 100;
            }
            else if (mode < 100)
            {
                side1 = 20 + MathExtra.RandomInt(50);
                side2 = 20 + MathExtra.RandomInt(50);
                height = 40 + MathExtra.RandomInt(110);
            }

            this.blocks.Add(new GameObject(this, new Vector2(-64, lastBlockHeight - height), new Vector2(side1, lastBlockHeight)));
            this.blocks.Add(new GameObject(this, new Vector2(this.Size.X - side2, lastBlockHeight - height), new Vector2(this.Size.X + 64, lastBlockHeight)));
            lastBlockHeight -= height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float offset = (-camera.Y / 4f);
            Texture2D parallaxTexture = TextureBin.Get("bkg_back");
            offset = (int)Math.Floor(offset) % (int)Engine.screenResolution.Y;
            spriteBatch.Draw(parallaxTexture, new Rectangle(0, (int)(offset),(int)Engine.screenResolution.X, (int)Engine.screenResolution.Y), Color.White);
            spriteBatch.Draw(parallaxTexture, new Rectangle(0, (int)(-Engine.screenResolution.Y + offset), (int)Engine.screenResolution.X, (int)Engine.screenResolution.Y), Color.White);

            offset = (-camera.Y / 2f);
            parallaxTexture = TextureBin.Get("bkg_front");
            offset = (int)Math.Floor(offset) % (int)Engine.screenResolution.Y;
            spriteBatch.Draw(parallaxTexture, new Rectangle(0, (int)(offset), (int)Engine.screenResolution.X, (int)Engine.screenResolution.Y), Color.White);
            spriteBatch.Draw(parallaxTexture, new Rectangle(0, (int)(-Engine.screenResolution.Y + offset), (int)Engine.screenResolution.X, (int)Engine.screenResolution.Y), Color.White);

            player.Draw(spriteBatch);
            foreach (GameObject go in blocks)
                go.Draw(spriteBatch);
            foreach (GameObject go in blockGroups)
                go.Draw(spriteBatch);

            foreach (Snow s in snow)
            {
                s.Draw(spriteBatch);
            }

            lavaAngle += 0.1f;
            float lavHeight = 48 + (float)Math.Sin(lavaAngle) * 16f;
            spriteBatch.Draw(TextureBin.Pixel, new Rectangle(0, (int)(Engine.screenResolution.Y -  lavHeight), (int)Engine.screenResolution.X, (int)lavHeight + 4), Color.Red);

            spriteBatch.DrawString(TextureBin.smallFont, "SCORE " + score, new Vector2(32, 32), Color.Red);

            if (player.Health <= 0)
            {
                if (fadeTimer > 0)
                    fadeTimer--;
                float alpha = MathHelper.SmoothStep(0.6f, 0f, fadeTimer / 200f);
                spriteBatch.Draw(TextureBin.Pixel, new Rectangle(0, 0, (int)Engine.screenResolution.X, (int)Engine.screenResolution.Y), Color.Black * alpha);
                Vector2 size = TextureBin.mainFont.MeasureString("Game Over");
                alpha = MathHelper.SmoothStep(1f, 0f, fadeTimer / 200f);
                spriteBatch.DrawString(TextureBin.mainFont, "Game Over", Engine.screenResolution / 2 - size / 2, Color.White * alpha);
                size = TextureBin.smallFont.MeasureString("Press ENTER");
                alpha = MathHelper.SmoothStep(0.9f, 0f, fadeTimer / 200f);
                spriteBatch.DrawString(TextureBin.smallFont, "Press ENTER", Engine.screenResolution / 2 - size / 2 + Vector2.UnitY * 64, Color.White * alpha);
            }
        }

        float lavaAngle = 0;
        float fadeTimer = 200f;
    }
}
