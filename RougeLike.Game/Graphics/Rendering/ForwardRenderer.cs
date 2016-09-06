namespace RougeLike.Game.Graphics.Rendering
{
    using OpenTK.Graphics.OpenGL4;

    using RougeLike.Game.Graphics.Shaders;

    public class ForwardRenderer
    {
        private readonly ShaderBase shader;

        public ForwardRenderer(ShaderBase shader)
        {
            this.shader = shader;
        }

        public void RenderObject(Object3D obj, GameCamera camera)
        {
            shader.Use();
            var view = camera.ViewMatrix;
            shader.SetView(ref view);

            var model = obj.GetModelMatrix();
            shader.SetModel(ref model);

            obj.Geometry.BindVao();
            obj.Geometry.Draw();
        }
    }
}