namespace RougeLike.Game.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK;
    using OpenTK.Graphics.OpenGL4;

    public class GameMesh
    {
        public List<Vertex> Vertices;

        public List<int> Indices;

        private int vbo;

        public int Vao;

        private int ebo;

        private Matrix4 modelMatrix = Matrix4.Identity;

        private bool matrixNeedsUpdate;

        private Vector3 rotation = Vector3.Zero;

        private Vector3 scale = Vector3.One;

        private Vector3 position = Vector3.Zero;

        public GameMesh(List<Vertex> vertices, List<int> indices)
        {
            Vertices = vertices;
            Indices = indices;
            SetupMesh();
        }

        public GameMesh(Assimp.Mesh mesh)
        {
            var indices = mesh.GetIndices();
            Vertices = new List<Vertex>(mesh.VertexCount);
            Indices = new List<int>(indices.Length);

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                Vertex v = new Vertex();
                v.Position.X = mesh.Vertices[i].X;
                v.Position.Y = mesh.Vertices[i].Y;
                v.Position.Z = mesh.Vertices[i].Z;

                v.Normal.X = mesh.Normals[i].X;
                v.Normal.Y = mesh.Normals[i].Y;
                v.Normal.Z = mesh.Normals[i].Z;

                Vertices.Add(v);
            }

            for (int i = 0; i < indices.Length; i++)
            {
                Indices.Add(indices[i]);
            }

            SetupMesh();
        }

        public Matrix4 GetModelMatrix()
        {
            if (matrixNeedsUpdate)
            {
                var scaleMatrix = Matrix4.CreateScale(scale);
                var translateMatrix = Matrix4.CreateTranslation(position);
                var rotateX = Matrix4.CreateRotationX(rotation.X);
                var rotateY = Matrix4.CreateRotationY(rotation.Y);
                var rotateZ = Matrix4.CreateRotationZ(rotation.Z);

                modelMatrix = translateMatrix * (rotateX * rotateY * rotateZ) * scaleMatrix;

                matrixNeedsUpdate = false;
            }

            return modelMatrix;
        }

        public void SetRotation(Vector3 rot)
        {
            rotation = rot;
            matrixNeedsUpdate = true;
        }

        public void SetScale(Vector3 s)
        {
            scale = s;
            matrixNeedsUpdate = true;
        }

        public void SetPosition(Vector3 p)
        {
            position = p;
            matrixNeedsUpdate = true;
        }

        private void SetupMesh()
        {
            Vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            ebo = GL.GenBuffer();

            GL.BindVertexArray(Vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                (IntPtr)(Vertices.Count * 2 * Vector3.SizeInBytes),
                Vertices.ToArray(),
                BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);

            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                (IntPtr)(Indices.Count * sizeof(int)),
                Indices.ToArray(),
                BufferUsageHint.StaticDraw);

            // Positions
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 2 * Vector3.SizeInBytes, 0);

            // Normals
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 2 * Vector3.SizeInBytes, Vector3.SizeInBytes);

           

            GL.BindVertexArray(0);
        }
    }
}