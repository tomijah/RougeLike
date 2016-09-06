namespace RougeLike.Game.Graphics.Shaders
{
    using System;
    using System.Collections.Generic;

    using OpenTK;
    using OpenTK.Graphics.OpenGL4;

    public abstract class ShaderBase: IDisposable
    {
        private static int programInUse = 0;

        public int ProgramId;

        protected List<int> ShaderIds = new List<int>();

        private int projectionLocation;

        private int viewLocation;

        private int modelLocation;

        protected ShaderBase()
        {
            ProgramId = GL.CreateProgram();
        }

        public void Init(Matrix4 projection)
        {
            AttachShaders();
            Link();
            Use();
            projectionLocation = GetUniformLocation("projection");
            viewLocation = GetUniformLocation("view");
            modelLocation = GetUniformLocation("model");
            GetAllUniformLocations();
            SetMatrix4(projectionLocation, ref projection);
        }

        private void DisposeResources()
        {
            if (ProgramId != 0)
            {
                GL.DeleteProgram(ProgramId);
            }
        }

        public virtual void Update(double elapsedTime)
        {
        }

        protected abstract void AttachShaders();

        protected virtual void GetAllUniformLocations()
        {
        }

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
                Console.WriteLine("Failed to Compile Shader." + Environment.NewLine + compileInfo);
                Console.ReadLine();
                GL.DeleteShader(id);
                Environment.Exit(1);
            }

            ShaderIds.Add(id);
            GL.AttachShader(ProgramId, id);
        }

        private void Link()
        {
            GL.LinkProgram(ProgramId);

            int statusCode;
            string linkInfo;
            GL.GetProgramInfoLog(ProgramId, out linkInfo);
            GL.GetProgram(ProgramId, GetProgramParameterName.LinkStatus, out statusCode);
            if (statusCode != 1)
            {
                Console.WriteLine("Failed to Link Program." + Environment.NewLine + linkInfo);
                Console.ReadLine();
                DetachAndDeleteShaders();
                GL.DeleteProgram(ProgramId);
                Environment.Exit(1);
            }

            DetachAndDeleteShaders();
        }

        protected int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(ProgramId, name);
        }

        public void SetMatrix4(int location, ref Matrix4 matrix)
        {
            GL.UniformMatrix4(location, false, ref matrix);
        }

        public void SetProjection(ref Matrix4 projection)
        {
            SetMatrix4(projectionLocation, ref projection);
        }

        public void SetView(ref Matrix4 view)
        {
            SetMatrix4(viewLocation, ref view);
        }

        public void SetModel(ref Matrix4 model)
        {
            SetMatrix4(modelLocation, ref model);
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

        private void DetachAndDeleteShaders()
        {
            for (int i = 0; i < ShaderIds.Count; i++)
            {
                GL.DetachShader(ProgramId, ShaderIds[i]);
                GL.DeleteShader(ShaderIds[i]);
            }
        }
    }
}