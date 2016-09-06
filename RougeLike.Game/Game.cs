namespace RougeLike.Game
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Threading.Tasks;

    using Assimp;

    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Input;

    using RougeLike.Game.Graphics;
    using RougeLike.Game.Graphics.Geometries;
    using RougeLike.Game.Graphics.Rendering;
    using RougeLike.Game.Graphics.Shaders;

    public class Game : GameWindow
    {
        private ForwardRenderShader shader;

        private GameCamera camera;

        private Object3D wolf;

        private Object3D cube;

        private Object3D tile;

        private ForwardRenderer render;

        private double elapsed = 0;

        private double frameRefresh = 0;

        public Game()
            : base(1600, 900, GraphicsMode.Default, "Rougelike", GameWindowFlags.FixedWindow)
        {
            VSync = VSyncMode.Off;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.ClearColor(Color.Black);

            shader = new ForwardRenderShader();
            shader.Init(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, (float)Width / (float)Height, 2f, 1000.0f));
            render = new ForwardRenderer(shader);
            wolf = new Object3D("Models/ww.dae");
            cube = new Object3D("Models/cube.dae");
            tile = new Object3D("Models/dungeon.ply");
            wolf.SetPosition(0, 0, -3);
            tile.SetPosition(0, 6, -3);
            cube.SetPosition(0, -6, -1);
            camera = new GameCamera();
            camera.SetPosition(-15.0f, 0.0f, 0.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            elapsed += e.Time;
            frameRefresh += e.Time;
            var keyboardState = OpenTK.Input.Keyboard.GetState();

            if (keyboardState[Key.Escape])
            {
                Exit();
            }

            var cameraSpeed = (float)e.Time * 10.0f;

            if (keyboardState[Key.W])
            {
                camera.Move(cameraSpeed, 0.0f, 0.0f, true);
            }

            if (keyboardState[Key.S])
            {
                camera.Move(-cameraSpeed, 0.0f, 0.0f, true);
            }

            if (keyboardState[Key.A])
            {
                camera.Move(0, cameraSpeed, 0.0f, true);
            }

            if (keyboardState[Key.D])
            {
                camera.Move(0, -cameraSpeed, 0.0f, true);
            }

            if (keyboardState[Key.Q])
            {
                camera.Move(0, 0.0f, -cameraSpeed, true);
            }

            if (keyboardState[Key.E])
            {
                camera.Move(0, 0.0f, cameraSpeed, true);
            }

            wolf.SetRotation(0, 0, (float)elapsed / 2);
            tile.SetRotation(0, 0, (float)elapsed / 2);
            cube.SetRotation(0, 0, (float)elapsed / 2);
            if (frameRefresh > 1)
            {
                Title = "Rougelike | Fps: " + Math.Round(RenderFrequency, MidpointRounding.ToEven);
                frameRefresh = 0;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            render.RenderObject(wolf, camera);
            render.RenderObject(cube, camera);
            render.RenderObject(tile, camera);
            SwapBuffers();
        }
    }
}