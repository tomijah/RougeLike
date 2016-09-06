namespace RougeLike.Game.Graphics.Shaders
{
    using OpenTK;
    using OpenTK.Graphics.OpenGL4;

    public class ForwardRenderShader : ShaderBase
    {
        protected override void AttachShaders()
        {
            AttachShader(Resources.BasicMeshVertex, ShaderType.VertexShader);
            AttachShader(Resources.BasicMeshFragment, ShaderType.FragmentShader);
        }
    }
}