namespace RougeLike.Game.Graphics.Shaders
{
    using OpenTK;
    using OpenTK.Graphics.OpenGL4;

    public class BasicMeshShader : ShaderBase
    {
        public Vector3 LightPosition;

        public BasicMeshShader()
        {
            AttachShader(Resources.BasicMeshVertex, ShaderType.VertexShader);
            AttachShader(Resources.BasicMeshFragment, ShaderType.FragmentShader);
            Link();
        }

        public void UpdateLightPosition()
        {
            int lightLocation = GL.GetUniformLocation(ProgramId, "lightPos");
            var lp = LightPosition;
            GL.Uniform3(lightLocation, lp);
        }

        public override void Update()
        {
            UpdateLightPosition();
        }
    }
}