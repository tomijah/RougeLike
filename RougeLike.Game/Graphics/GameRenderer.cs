﻿namespace RougeLike.Game.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK;
    using OpenTK.Graphics.OpenGL4;

    public class GameRenderer
    {
        public static void DrawMesh(GameMesh mesh, GameCamera camera, ShaderBase shader)
        {
            shader.Use();

            int viewLocation = GL.GetUniformLocation(shader.ProgramId, "view");
            int projectionLocation = GL.GetUniformLocation(shader.ProgramId, "projection");
            int modelLocation = GL.GetUniformLocation(shader.ProgramId, "model");

            var projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, (float)800 / (float)600, 0.1f, 100.0f);
            GL.UniformMatrix4(projectionLocation, false, ref projection);

            var view = camera.GetViewMatrix();
            GL.UniformMatrix4(viewLocation, false, ref view);

            var model = mesh.GetModelMatrix();
            GL.UniformMatrix4(modelLocation, false, ref model);

            GL.BindVertexArray(mesh.Vao);
            GL.DrawElements(BeginMode.Triangles, mesh.Indices.Count, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }
    }
}