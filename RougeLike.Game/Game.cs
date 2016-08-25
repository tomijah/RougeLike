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
    using RougeLike.Game.Graphics.Shaders;

    public class Game : GameWindow
    {
        private Scene scene;

        private GameMesh mesh;

        private ShaderBase shader;

        private GameCamera camera;

        private double elapsed = 0;

        public Game()
            : base(800, 600, new GraphicsMode((ColorFormat)32, 16, 0, 8, (ColorFormat)0, 2, false))
        {
            VSync = VSyncMode.On;
            WindowBorder = WindowBorder.Fixed;
            Title = "Rougelike";
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.Blend);
            GL.CullFace(CullFaceMode.Back);
            GL.ClearColor(Color.Black);

            var context = new AssimpContext();
            scene = context.ImportFile("Models/smooth.dae");
            mesh = new GameMesh(scene.Meshes[0]);
            shader = new BasicMeshShader();
            camera = new GameCamera();
            camera.SetPosition(0.0f, 0.0f, 5.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            elapsed += e.Time;
            var keyboardState = OpenTK.Input.Keyboard.GetState();

            if (keyboardState[Key.Escape])
            {
                Exit();
            }

            var cameraSpeed = (float)e.Time * 5.0f;

            if (keyboardState[Key.W])
            {
                camera.Move(0.0f, 0.0f, -cameraSpeed);
            }

            if (keyboardState[Key.S])
            {
                camera.Move(0.0f, 0.0f, cameraSpeed);
            }

            if (keyboardState[Key.A])
            {
                camera.Move(-cameraSpeed, 0.0f, 0.0f);
            }

            if (keyboardState[Key.D])
            {
                camera.Move(cameraSpeed, 0.0f, 0.0f);
            }

            mesh.SetRotation(0, (float)elapsed, 0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            GameRenderer.DrawMesh(mesh, camera, shader);
            SwapBuffers();
        }
    }
}