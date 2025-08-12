using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win2DApp.Utils;

namespace Win2DApp.MyMath
{
    internal class ProjectionMatrix
    {
        private float[,] matrix = new float[4, 4];
        private float[,] RotateXMat = new float[4, 4];
        public ProjectionMatrix() 
        {
        }

        public void FillProjectionMatrix(float angle, float zNear, float zFar)
        {
            matrix[0, 0] = ScreenSize.Height / ScreenSize.Width * (float)(1 / Math.Tan(angle / 2));
            matrix[1, 1] = (float)(1 / Math.Tan(angle / 2));
            matrix[2, 2] = zFar / (zFar - zNear);
            matrix[2, 3] = -(zFar * zNear) / (zFar - zNear);
            matrix[3, 2] = 1f;
        }

        public static MVector3 Project(in MVector3 v, in ProjectionMatrix m)
        {
            MVector3 temp = MVector3.Zero;
            temp.x  = v.x * m.matrix[0, 0] + v.y * m.matrix[0, 1] + v.z * m.matrix[0, 2] + m.matrix[0, 3];
            temp.y  = v.x * m.matrix[1, 0] + v.y * m.matrix[1, 1] + v.z * m.matrix[1, 2] + m.matrix[1, 3];
            temp.z  = v.x * m.matrix[2, 0] + v.y * m.matrix[2, 1] + v.z * m.matrix[2, 2] + m.matrix[2, 3];
            float w = v.x * m.matrix[3, 0] + v.y * m.matrix[3, 1] + v.z * m.matrix[3, 2] + m.matrix[3, 3];

            if (w != 0) 
            {
                temp.x /= w;
                temp.y /= w;
                temp.z /= w;
            }

            return temp;
        }

        public static MVector3 RotateX(in MVector3 v, float angle) 
        {
            MVector3 w = new(0, 0, 0);
            angle *= MathF.PI / 180f; 
            w.x = v.x;
            w.y = v.y * MathF.Cos(angle) + v.z * MathF.Sin(angle);
            w.z = v.y * -MathF.Sin(angle) + v.z * MathF.Cos(angle);
            return w;
        }
        public static MVector3 RotateY(in MVector3 v, float angle)
        {
            MVector3 w = new(0, 0, 0);
            angle *= MathF.PI / 180f;
            w.x = v.x * MathF.Cos(angle) + v.z * -MathF.Sin(angle);
            w.y = v.y;
            w.z = v.x * MathF.Sin(angle) + v.z * MathF.Cos(angle);
            return w;
        }
    }
}
