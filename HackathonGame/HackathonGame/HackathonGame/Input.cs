using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace HackathonGame
{
    public static class Input
    {
        private static MouseState mouse;
        private static MouseState mousePrev;
        private static KeyboardState keyboard;
        private static KeyboardState keyboardPrev;

        public static void Update()
        {
            mousePrev = mouse;
            keyboardPrev = keyboard;
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
        }

        public static bool KeyboardTapped(Keys key)
        {
            return (keyboardPrev.IsKeyDown(key) && keyboard.IsKeyUp(key));
        }

        public static bool IsKeyDown(Keys key)
        {
            return keyboard.IsKeyDown(key);
        }

        public static bool MouseLeftButtonTapped
        {
            get { return (mouse.LeftButton == ButtonState.Pressed && mousePrev.LeftButton == ButtonState.Released); }
        }

        public static bool MouseRightButtonTapped
        {
            get { return (mouse.RightButton == ButtonState.Pressed && mousePrev.RightButton == ButtonState.Released); }
        }

        public static Vector2 MousePosition
        {
            get { return new Vector2(mouse.X, mouse.Y); }
        }
    }
}
