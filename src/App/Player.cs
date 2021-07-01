using System;
using OpenTK.Input;

using GraphicsRenderer;
using Input;

namespace App
{
    class Player
    {
        private int x, y;
        private int velocity = 5;

        public Player(int posX, int posY)
        {
            x = posX;
            y = posY;
        }

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }

        public void Update()
        {
            // A simple player controller with gamepad support
            if (InputHandler.KeyDown(Key.W) || GamePadController.GpButtonDown(ButtonName.UP))
            {
                y -= velocity;
            }
            if (InputHandler.KeyDown(Key.A) || GamePadController.GpButtonDown(ButtonName.LEFT))
            {
                x -= velocity;
            }
            if (InputHandler.KeyDown(Key.S) || GamePadController.GpButtonDown(ButtonName.DOWN))
            {
                y += velocity;
            }
            if (InputHandler.KeyDown(Key.D) || GamePadController.GpButtonDown(ButtonName.RIGHT))
            {
                x += velocity;
            }
        }

        public void Render()
        {
            Graphics.DrawPlane(new OpenTK.Vector2(x, y), new OpenTK.Vector2(25, 25), OpenTK.Color.OrangeRed);
        }

    }
}
