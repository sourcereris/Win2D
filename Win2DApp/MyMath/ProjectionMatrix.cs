using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win2DApp.MyMath
{
    internal class ProjectionMatrix
    {
        private float[,] matrix = new float[4, 4];

        public ProjectionMatrix() { }

        void FillProjectionMatrix(float width, float height, float angle, float zNear, float zFar)
        {
            matrix[0, 0] = height / width * (float)(1 / Math.Tan(angle / 2));
            matrix[1, 1] = (float)(1 / Math.Tan(angle / 2));
            matrix[2, 2] = zFar / (zFar - zNear);
            matrix[2, 3] = 1f;
            matrix[3, 2] = -(zFar * zNear) / (zFar - zNear);
        }

        public static MVector3 Project(in MVector3 v, in ProjectionMatrix m)
        {
            MVector3 temp = MVector3.Zero;
            temp.x *= m.matrix[0, 0];
            temp.y *= m.matrix[1, 1];
            temp.z  = v.z * m.matrix[2, 2] + v.w * m.matrix[3, 2];
            temp.w  = v.z * m.matrix[2, 3];
            return temp;
        }
    }
}
