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
        const int WALL_HEIGHT_SIZE = 300;
        
        public Vector2 Size;
        public Player player;
        public BufferedList<GameObject> blocks;
        public List<BlockGroup> blockGroups;
        public Vector2 camera = Vector2.Zero;

        public int lastBlockHeight = 0;
        public float lastLeft;
        public float lastRight;
        
        #region Timers
        int blockTimer = 0;
        #endregion

        public Room(Vector2 Size)
        {
            this.Size = Size;
            this.player = new Player(this, new Vector2(WALL_PADDING, Size.Y / 2));
            this.blocks = new BufferedList<GameObject>();
            this.blocks.Add(new Block(this, Vector2.Zero, new Vector2(WALL_PADDING, Size.Y)));
            this.blocks.Add(new Block(this, new Vector2(Size.X - WALL_PADDING, 0), new Vector2(WALL_PADDING, Size.Y)));
            this.blocks.Add(player);
            this.blockGroups = new List<BlockGroup>();
        }

        public void Update()
        {
            // Camera pans upward.
            this.camera.Y -= 0.5f;
            if (player.Top < camera.Y + 150)
                camera.Y = player.Top - 150;

            // Spawns blocks.
            blockTimer++;
            if (blockTimer == BLOCK_INTERVAL)
            {

                Random gridRandom = new Random();
                float choosenX = MathHelper.Lerp((float)(lastLeft + 100f), (float)(lastRight - (float)GRIDSIZE), (float)(gridRandom.NextDouble()));
                blocks.Add(new Block(this, new Vector2(choosenX, -GRIDSIZE * 3 + camera.Y), new Vector2(128, 48)));
                blockTimer = 0;
            }

            if (this.camera.Y < lastBlockHeight)
            {
                lastBlockHeight -= WALL_HEIGHT_SIZE;

                int randback = MathExtra.RandomInt(200);
                if (MathHelper.Distance(randback, lastLeft) < 10)
                    randback += 20;
                this.blocks.Add(new GameObject(this, new Vector2(-randback, lastBlockHeight), Vector2.Zero, new Vector2(100, WALL_HEIGHT_SIZE), TextureBin.Pixel));
                lastLeft = randback;

                randback = MathExtra.RandomInt(200);
                if (MathHelper.Distance(randback, lastRight) < 10)
                    randback += 20;
                this.blocks.Add(new GameObject(this, new Vector2(this.Size.X + 100 - randback, lastBlockHeight), Vector2.Zero, new Vector2(100, WALL_HEIGHT_SIZE), TextureBin.Pixel));
                lastRight = randback;
            }

            //if (Input.IsKeyboardTapped(Keys.A))
            //{
            //    blockGroups.Add(new BlockGroup(this, Input.MousePosition));
            //}

            foreach (GameObject b in blocks)
                b.Update();
            blocks.ApplyBuffers();
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
        }
    }
}
