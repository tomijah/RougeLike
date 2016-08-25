namespace RougeLike.Game.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK;

    public class PlaneMesh : GameMesh
    {
        public PlaneMesh(float width, float height)
        {
            var verticles = new List<Vertex>();
            var indices = new List<int>();

            verticles.Add(new Vertex(width / 2, height / 2, 0.0f, 0, 0, 1));
            verticles.Add(new Vertex(width / 2, -height / 2, 0.0f, 0, 0, 1));
            verticles.Add(new Vertex(-width / 2, -height / 2, 0.0f, 0, 0, 1));
            verticles.Add(new Vertex(-width / 2, height / 2, 0.0f, 0, 0, 1));

            indices.AddRange(new[] { 0, 3, 1, 1, 3, 2 });

            Vertices = verticles;
            Indices = indices;
            SetupMesh();
        }
    }
}