using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Win2DApp.MyMath
{
    internal class MVector3
    {
        public static MVector3 Zero => new(0, 0, 0);
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public MVector3(float v1, float v2, float v3) 
        {
            x = v1;
            y = v2;
            z = v3;
        }

        public static implicit operator Vector3(MVector3 v) 
            => new Vector3(v.x, v.y, v.z);

        public void Add(MVector3 v) 
        {
            x += v.x;
            y += v.y;
            z += v.z;
        }

        public float Length()
            => (float)Math.Sqrt(x * x + y * y + z * z);

        public static MVector3 operator *(MVector3 v, float n)
            => new (v.x * n, v.y * n, v.z * n);
        public static MVector3 operator *(float n, MVector3 v)
            => new (v.x * n, v.y * n, v.z * n);

        public static MVector3 operator +(MVector3 v, MVector3 w) 
            => new (v.x + w.x, v.y + w.y, v.z + w.z);

        public static MVector3 operator -(MVector3 v, MVector3 w)
            => new(v.x + w.x, v.y + w.y, v.z + w.z);
        
        public static MVector3 operator -(MVector3 v)
            => new(-v.x, -v.y, -v.z);

        public static MVector3 operator /(MVector3 v, float n)
            => new(v.x / n, v.y / n, v.z / n);
        public static MVector3 operator /(float n, MVector3 v)
            => new(v.x / n, v.y / n, v.z / n);

        public static MVector3 Normalize(MVector3 v)
        {
            float length = v.Length();
            if (length == 0) return new MVector3(0, 0, 0);
            return new MVector3(v.x / length, v.y / length, v.z / length);
        }

        public static float Dot(MVector3 v, MVector3 w)
            => (v.x * w.x + v.y * w.y + v.z * w.z);

        public static MVector3 Cross(MVector3 v, MVector3 w)
            => new(v.y*w.z - v.z*w.y, v.z*w.x - v.x*w.z, v.x*w.y - v.y*w.x);
    }
}
