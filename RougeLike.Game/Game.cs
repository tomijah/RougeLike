namespace RougeLike.Game
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Input;

    public class Game : GameWindow
    {
        public Game()
            : base(800, 600)
        {
            VSync = VSyncMode.On;
            WindowBorder = WindowBorder.Fixed;
        }


        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.ClearColor(Color.MediumSlateBlue);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboardState = OpenTK.Input.Keyboard.GetState();

            if (keyboardState[Key.Escape])
            {
                Exit();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            // Draw
            SwapBuffers();
        }
    }
}