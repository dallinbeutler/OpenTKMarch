using Assimp;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKmarch
{
    public static class Util
    {

        public static int bytesize(object item)
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(item);
        }

        public static int bytesize<T>(this T[] item)
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(item[0]) * item.Length;
        }
        public static int bytesize<T >(this T item)
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(item);
        }

        public static double toRadD(this double item)
        {
            return (Math.PI / 180) * item;
        }

        public static double toRadD(this float item)
        {
            return ((Math.PI / 180) * item);
        }
        public static float toRadF(this double item)
        {
            return (float)((Math.PI / 180) * item);
        }

        public static float toRadF(this float item)
        {
            return (float)((Math.PI / 180) * item);
        }

        static float finterp(float x, float goal, float stepsize)
        {
            if (x > goal)
                return x - stepsize;
            else
                return x + stepsize;
        }
        private static Matrix4 FromMatrix(Matrix4x4 mat)
        {
            Matrix4 m = new Matrix4();
            m.M11 = mat.A1;
            m.M12 = mat.A2;
            m.M13 = mat.A3;
            m.M14 = mat.A4;
            m.M21 = mat.B1;
            m.M22 = mat.B2;
            m.M23 = mat.B3;
            m.M24 = mat.B4;
            m.M31 = mat.C1;
            m.M32 = mat.C2;
            m.M33 = mat.C3;
            m.M34 = mat.C4;
            m.M41 = mat.D1;
            m.M42 = mat.D2;
            m.M43 = mat.D3;
            m.M44 = mat.D4;
            return m;
        }

        private static Vector3 FromVector(Vector3D vec)
        {
            Vector3 v;
            v.X = vec.X;
            v.Y = vec.Y;
            v.Z = vec.Z;
            return v;
        }

        private static Color4 FromColor(Color4D color)
        {
            Color4 c;
            c.R = color.R;
            c.G = color.G;
            c.B = color.B;
            c.A = color.A;
            return c;
        }
    }
}
