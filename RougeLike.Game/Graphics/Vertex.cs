namespace RougeLike.Game.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK;

    public struct Vertex
    {
        public Vector3 Position;

        public Vector3 Normal;

        public Vector4 Color;

        public Vertex(Vector3 position, Vector3 normal, Vector4 color)
        {
            Position = position;
            Normal = normal;
            Color = color;
        }

        public Vertex(Vector3 position)
        {
            Position = position;
            Normal = Vector3.Zero;
            Color = new Vector4(1, 1, 1, 1);
        }

        public Vertex(float x, float y, float z)
            : this(new Vector3(x, y, z))
        {
        }

        public Vertex(float x, float y, float z, float nx, float ny, float nz)
        {
            Color = new Vector4(1, 1, 1, 1);
            Position.X = x;
            Position.Y = y;
            Position.Z = z;

            Normal.X = nx;
            Normal.Y = ny;
            Normal.Z = nz;
        }
    }
}