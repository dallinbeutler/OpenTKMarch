using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKmarch
{
    class ShaderProgram
    {

        private int id { get; }


        public Dictionary<string, int> Uniforms;

        public ShaderProgram(string vCode, string fCode, params string[] uniforms)
        {

            //vertex shader
            int vid = GL.CreateShader(ShaderType.VertexShader);

            GL.ShaderSource(vid, vCode);
            GL.CompileShader(vid);
            GL.GetShaderInfoLog(vid, out string vertexLog);
            Console.WriteLine("Shader Log:");
            Console.WriteLine(vertexLog);
            Console.WriteLine();

            //fragment shader
            int fid = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fid, fCode);
            GL.CompileShader(fid);
            GL.GetShaderInfoLog(fid, out string fragLog);
            Console.WriteLine("Frag Log:");
            Console.WriteLine(fragLog);
            Console.WriteLine();


            // attach and link to form shader program
            id = GL.CreateProgram();

            GL.AttachShader(id, vid);
            GL.AttachShader(id, fid);
            GL.LinkProgram(id);
            GL.GetProgramInfoLog(fid, out string programLog);
            Console.WriteLine("Program Log:");
            Console.WriteLine(programLog);
            Console.WriteLine();



            GL.DeleteShader(vid);
            GL.DeleteShader(fid);

            InitUniforms(uniforms);
        }

        public void Use()
        {
            GL.UseProgram(id);
        }

        private void InitUniforms(params string[] paramNames)
        {
            Uniforms = new Dictionary<string, int>();

            for (int i = 0; i < paramNames.Length; i++)
            {
                Uniforms[paramNames[i]] = GL.GetUniformLocation(id, paramNames[i]);
            }
        }


        public void SetVector4 (string paramName, Vector4 value)
        {
            GL.UseProgram(this.id);
            GL.Uniform4(Uniforms[paramName], value);
        }

        public void SetVector4(string paramName, System.Drawing.Color value) 
        {
            GL.UseProgram(this.id);
            GL.Uniform4(Uniforms[paramName], value);           
        }

        public void SetVector4(string paramName, float r, float g, float b, float a)
        {
            GL.UseProgram(this.id);
            GL.Uniform4(Uniforms[paramName], r, g, b, a);          
        }

        public void SetVector3(string paramName, float x, float y, float z)
        {
            GL.UseProgram(this.id);
            GL.Uniform3(Uniforms[paramName], x, y, z);
        }
        public void SetVector3(string paramName, Vector3 vector)
        {
            GL.UseProgram(this.id);
            GL.Uniform3(Uniforms[paramName], vector);
        }

        public void SetVector(string paramName, float x)
        {
            GL.UseProgram(this.id);
            GL.Uniform1(Uniforms[paramName], x);
        }
        public void SetVector(string paramName, double x)
        {
            GL.UseProgram(this.id);
            GL.Uniform1(Uniforms[paramName], x);
        }
        public void SetVector(string paramName, int x)
        {
            GL.UseProgram(this.id);
            GL.Uniform1(Uniforms[paramName], x);
        }

        public void SetMat4(string paramName, ref Matrix4 x)
        {
            try
            {
                GL.UseProgram(this.id);
                GL.UniformMatrix4(Uniforms[paramName], false, ref x);
            }
            catch
            {
                throw new Exception("no Param found");
            }
        }
    }
}
