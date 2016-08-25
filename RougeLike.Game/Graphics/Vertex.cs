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

        public Vertex(Vector3 position, Vector3 normal)
        {
            Position = position;
            Normal = normal;
        }

        public Vertex(Vector3 position)
        {
            Position = position;
            Normal = Vector3.Zero;
        }

        public Vertex(float x, float y, float z)
            : this(new Vector3(x, y, z))
        {
        }
    }
}