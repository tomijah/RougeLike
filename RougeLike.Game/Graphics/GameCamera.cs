namespace RougeLike.Game.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK;

    public class GameCamera
    {
        private Vector3 position = Vector3.Zero;

        private bool needCalculateMatrix = true;

        private Matrix4 viewMatrix = Matrix4.Zero;

        private Vector3 target = Vector3.Zero;

        private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

        public void SetPosition(Vector3 pos)
        {
            position = pos;
            needCalculateMatrix = true;
        }

        public void Move(Vector3 t)
        {
            position += t;
            target += t;
            needCalculateMatrix = true;
        }

        public void SetTarget(Vector3 t)
        {
            target = t;
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
    }
}