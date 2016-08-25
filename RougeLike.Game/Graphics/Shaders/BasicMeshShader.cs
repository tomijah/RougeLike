namespace RougeLike.Game.Graphics.Shaders
{
    using OpenTK.Graphics.OpenGL4;

    public class BasicMeshShader : ShaderBase
    {
        public BasicMeshShader()
        {
            AttachShader(Resources.BasicMeshVertex, ShaderType.VertexShader);
            AttachShader(Resources.BasicMeshFragment, ShaderType.FragmentShader);
            Link();
        }
    }
}