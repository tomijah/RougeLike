namespace RougeLike.Game.Graphics.Geometries
{
    using System;
    using System.Collections.Generic;

    using OpenTK;
    using OpenTK.Graphics.OpenGL4;

    public class Geometry3D : IDrawable
    {
        private readonly List<Vertex> vertices;

        private readonly List<int> indices;

        private int vbo;

        private int vao;

        private int ebo;

        public Geometry3D()
        {
            vertices = new List<Vertex>();
            indices = new List<int>();
        }

        public Geometry3D(Assimp.Mesh mesh) : this()
        {
            Add(mesh, Vector3.Zero);
        }

        public Geometry3D(string modelPath) : this()
        {
            var context = new Assimp.AssimpContext();
            Add(context.ImportFile(modelPath).Meshes[0], Vector3.Zero);
        }

        public int Vao => vao;

        public void Add(Assimp.Mesh mesh, Vector3 translate)
        {
            var meshIndices = mesh.GetIndices();
            int offset = vertices.Count;

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                Vertex v = new Vertex();
                v.Position.X = mesh.Vertices[i].X + translate.X;
                v.Position.Y = mesh.Vertices[i].Y + translate.Y;
                v.Position.Z = mesh.Vertices[i].Z + translate.Z;

                v.Normal.X = mesh.Normals[i].X;
                v.Normal.Y = mesh.Normals[i].Y;
                v.Normal.Z = mesh.Normals[i].Z;
                if (mesh.VertexColorChannelCount > 0)
                {
                    v.Color.X = mesh.VertexColorChannels[0][i].R;
                    v.Color.Y = mesh.VertexColorChannels[0][i].G;
                    v.Color.Z = mesh.VertexColorChannels[0][i].B;
                    v.Color.W = mesh.VertexColorChannels[0][i].A;
                }
                else
                {
                    v.Color = Vector4.One;
                }

                vertices.Add(v);
            }

            for (int i = 0; i < meshIndices.Length; i++)
            {
                indices.Add(meshIndices[i] + offset);
            }
        }

        public void SetupBuffers()
        {
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            ebo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                (IntPtr)(vertices.Count * (2 * Vector3.SizeInBytes + Vector4.SizeInBytes)),
                vertices.ToArray(),
                BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);

            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                (IntPtr)(indices.Count * sizeof(int)),
                indices.ToArray(),
                BufferUsageHint.StaticDraw);

            // Positions
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 2 * Vector3.SizeInBytes + Vector4.SizeInBytes, 0);

            // Normals
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 2 * Vector3.SizeInBytes + Vector4.SizeInBytes, Vector3.SizeInBytes);

            // Colors
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 2 * Vector3.SizeInBytes + Vector4.SizeInBytes, 2 * Vector3.SizeInBytes);

            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.DrawElements(BeginMode.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void BindVao()
        {
            GL.BindVertexArray(vao);
        }
    }
}