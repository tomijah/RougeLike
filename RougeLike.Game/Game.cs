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
        private GameMesh mesh;

        private GameMesh lightMesh;

        private BasicMeshShader shader;

        private LightSourceShader lightSourceShader;

        private GameCamera camera;

        private double elapsed = 0;

        public Game()
            : base(
                1600,
                900,
                new GraphicsMode((ColorFormat)32, 8, 0, 8, (ColorFormat)0, 2, false),
                "Rougelike",
                GameWindowFlags.FixedWindow)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.CullFace(CullFaceMode.Back);
            GL.ClearColor(Color.Black);

            var context = new AssimpContext();
            mesh = new GameMesh(context.ImportFile("Models/abstract.dae").Meshes[0]);
            lightMesh = new GameMesh(context.ImportFile("Models/sphere.dae").Meshes[0]);
            lightMesh.SetScale(0.2f, 0.2f, 0.2f);
            
            shader = new BasicMeshShader();
            lightSourceShader = new LightSourceShader();
            camera = new GameCamera();
            camera.SetPosition(0.0f, 0.0f, 5.0f);
            shader.LightPosition = new Vector3(0.0f, 5.0f, 10.0f);
            lightMesh.SetPosition(shader.LightPosition.X, shader.LightPosition.Y, shader.LightPosition.Z);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            elapsed += e.Time;
            var keyboardState = OpenTK.Input.Keyboard.GetState();

            if (keyboardState[Key.Escape])
            {
                Exit();
            }

            var cameraSpeed = (float)e.Time * 10.0f;

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

            if (keyboardState[Key.Up])
            {
                lightMesh.Move(0.0f, 0.0f, -cameraSpeed);
                shader.LightPosition.Z -= cameraSpeed;
            }

            if (keyboardState[Key.Down])
            {
                lightMesh.Move(0.0f, 0.0f, cameraSpeed);
                shader.LightPosition.Z += cameraSpeed;
            }

            if (keyboardState[Key.Left])
            {
                lightMesh.Move(-cameraSpeed, 0.0f, 0.0f);
                shader.LightPosition.X -= cameraSpeed;
            }

            if (keyboardState[Key.Right])
            {
                lightMesh.Move(cameraSpeed, 0.0f, 0.0f);
                shader.LightPosition.X += cameraSpeed;
            }

            if (keyboardState[Key.PageUp])
            {
                lightMesh.Move(0.0f, cameraSpeed, 0.0f);
                shader.LightPosition.Y += cameraSpeed;
            }

            if (keyboardState[Key.PageDown])
            {
                lightMesh.Move(0.0f, -cameraSpeed, 0.0f);
                shader.LightPosition.Y -= cameraSpeed;
            }

            mesh.SetRotation((float)elapsed * 2, (float)elapsed * 2, 0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            GameRenderer.DrawMesh(mesh, camera, shader);
            GameRenderer.DrawMesh(lightMesh, camera, lightSourceShader);
            SwapBuffers();
        }
    }
}