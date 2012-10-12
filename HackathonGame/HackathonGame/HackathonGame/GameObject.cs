using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HackathonGame
{
    public class GameObject
    {
        Vector2 position;
        Vector2 velocity;
        Texture2D texture;

        public GameObject(Vector2 position, Vector2 velocity, Texture2D texture)
        {
            this.position = position;
            this.velocity = velocity;
            this.texture = texture;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spr)
        {
            spr.Draw(texture, new Rectangle(this.position.X, this.position.Y, this.size.X, this.size.Y), null, Color.White);
        }
    }
}
