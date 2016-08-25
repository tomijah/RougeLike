namespace RougeLike.Game.Graphics.Shaders
{
    using OpenTK.Graphics.OpenGL4;

    public class LightSourceShader: ShaderBase
    {
        public LightSourceShader()
        {
            AttachShader(Resources.BasicMeshVertex, ShaderType.VertexShader);
            AttachShader(Resources.LightSourceFragment, ShaderType.FragmentShader);
            Link();
        }

        public override void Update()
        {
        }
    }
}