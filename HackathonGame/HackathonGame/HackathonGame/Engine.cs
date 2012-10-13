using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HackathonGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Room room;
        public static int gameScore = 0;

        public static Vector2 screenResolution = new Vector2(480, 720);

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
            this.graphics.PreferredBackBufferWidth = (int)screenResolution.X;
            this.graphics.PreferredBackBufferHeight = (int)screenResolution.Y;
            this.graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureBin.LoadContent(Content);
            SoundBin.LoadSounds(Content);
            room = new Room(screenResolution);
            MediaPlayer.Play(SoundBin.GetSong("descent"));
            MediaPlayer.IsRepeating = true;
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            if (Input.IsKeyboardTapped(Keys.Escape))
                this.Exit();

            room.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(28, 18, 67));

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            room.Draw(spriteBatch);

            if (title > 0)
            {
                title--;
                Vector2 size = TextureBin.mainFont.MeasureString("volcano Ice\n  Mountian");
                float alpha = MathHelper.SmoothStep(0f, 1f, title / 200f);
                spriteBatch.DrawString(TextureBin.mainFont, "volcano ice\n  Mountian", Engine.screenResolution / 2 - size / 2, Color.Red * alpha);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        float title = 200;
    }
}
