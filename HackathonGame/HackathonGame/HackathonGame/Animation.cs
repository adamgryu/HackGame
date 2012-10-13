using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackathonGame
{
    public class Animation
    {
        int animationTimer = 0;
        int animationFrame = 0;

        Texture2D texture;
        public int frames;
        int width;
        int frameDuration;

        public Animation(Texture2D texture, int frames, int width, int frameDuration)
        {
            this.texture = texture;
            this.frames = frames;
            this.width = width;
            this.frameDuration = frameDuration;
        }

        public void Update()
        {
            animationTimer++;
            if (animationTimer > frameDuration)
            {
                animationTimer = 0;
                animationFrame++;
                if (animationFrame >= frames)
                    animationFrame = 0;
            }

            if (animationFrame > frames)
            {
                animationFrame = 0;
            }

        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public Rectangle GetSourceRect()
        {
            return new Rectangle(animationFrame * width, 0, width, texture.Height);
        }
    }
}
