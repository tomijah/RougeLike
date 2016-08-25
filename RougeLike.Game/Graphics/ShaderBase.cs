namespace RougeLike.Game.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK.Graphics.OpenGL4;

    public abstract class ShaderBase: IDisposable
    {
        private static int programInUse = 0;

        public int ProgramId;

        protected List<int> ShaderIds = new List<int>();

        protected ShaderBase()
        {
            ProgramId = GL.CreateProgram();
        }

        private void DisposeResources()
        {
            if (ProgramId != 0)
            {
                GL.DeleteProgram(ProgramId);
            }
        }

        public abstract void Update();

        protected void AttachShader(string code, ShaderType type)
        {
            int id = GL.CreateShader(type);
            GL.ShaderSource(id, code);
            GL.CompileShader(id);

            int statusCode;
            string compileInfo;
            GL.GetShaderInfoLog(id, out compileInfo);
            GL.GetShader(id, ShaderParameter.CompileStatus, out statusCode);
            if (statusCode != 1)
            {
                Console.WriteLine(
                    "Failed to Compile Shader Source." + Environment.NewLine + compileInfo + Environment.NewLine
                    + "Status Code: " + statusCode);
                Console.ReadLine();
                GL.DeleteShader(id);
                Environment.Exit(1);
            }

            ShaderIds.Add(id);
            GL.AttachShader(ProgramId, id);
        }

        protected void Link()
        {
            GL.LinkProgram(ProgramId);
            for (int i = 0; i < ShaderIds.Count; i++)
            {
                GL.DetachShader(ProgramId, ShaderIds[i]);
                GL.DeleteShader(ShaderIds[i]);
            }
        }

        public void Dispose()
        {
            DisposeResources();
            GC.SuppressFinalize(this);
        }

        public void Use()
        {
            if (programInUse != ProgramId)
            {
                GL.UseProgram(ProgramId);
                programInUse = ProgramId;
            }
        }
    }
}