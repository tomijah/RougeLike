namespace RougeLike.Game.Graphics
{
    using OpenTK;

    using RougeLike.Game.Graphics.Geometries;

    public class Object3D
    {
        private Matrix4 modelMatrix = Matrix4.Identity;

        private bool matrixNeedsUpdate = true;

        private Vector3 rotation = Vector3.Zero;

        private Vector3 scale = Vector3.One;

        private Vector3 position = Vector3.Zero;

        private Geometry3D geometry;

        public Object3D(Geometry3D geom)
        {
            geometry = geom;
        }

        public Object3D(Assimp.Mesh mesh)
        {
            geometry = new Geometry3D(mesh);
            geometry.SetupBuffers();
        }

        public Object3D(string path)
        {
            geometry = new Geometry3D(path);
            geometry.SetupBuffers();
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

                modelMatrix = scaleMatrix * (rotateX * rotateY * rotateZ) * translateMatrix;

                matrixNeedsUpdate = false;
            }

            return modelMatrix;
        }

        public Geometry3D Geometry => geometry;

        public void SetRotation(float x, float y, float z)
        {
            rotation.X = x;
            rotation.Y = y;
            rotation.Z = z;
            matrixNeedsUpdate = true;
        }

        public void SetScale(float x, float y, float z)
        {
            scale.X = x;
            scale.Y = y;
            scale.Z = z;
            matrixNeedsUpdate = true;
        }

        public void Move(float x, float y, float z)
        {
            position.X += x;
            position.Y += y;
            position.Z += z;
            matrixNeedsUpdate = true;
        }

        public void SetPosition(float x, float y, float z)
        {
            position.X = x;
            position.Y = y;
            position.Z = z;
            matrixNeedsUpdate = true;
        }
    }
}