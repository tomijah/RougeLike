namespace RougeLike.Game.Graphics
{
    using OpenTK;

    public class GameCamera
    {
        private Vector3 position = Vector3.Zero;

        private bool needCalculateMatrix = true;

        private Matrix4 viewMatrix = Matrix4.Zero;

        private Vector3 target = Vector3.Zero;

        private readonly Vector3 up = new Vector3(0.0f, 0.0f, 1.0f);

        public void SetPosition(float x, float y, float z)
        {
            position.X = x;
            position.Y = y;
            position.Z = z;
            needCalculateMatrix = true;
        }

        public void Move(float x, float y, float z, bool moveLookAt = false)
        {
            position.X += x;
            position.Y += y;
            position.Z += z;
            if (moveLookAt)
            {
                target.X += x;
                target.Y += y;
                target.Z += z;
            }

            needCalculateMatrix = true;
        }

        public void SetTarget(float x, float y, float z)
        {
            target.X = x;
            target.Y = y;
            target.Z = z;
            needCalculateMatrix = true;
        }

        public Matrix4 GetViewMatrix()
        {
            if (needCalculateMatrix)
            {
                viewMatrix = Matrix4.LookAt(position, target, up);
                needCalculateMatrix = false;
            }

            return viewMatrix;
        }

        public Matrix4 ViewMatrix => GetViewMatrix();
    }
}