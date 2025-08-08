using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace Win2DApp.MyMath
{
    internal class MVector2
    {
        public static readonly MVector2 zero = new MVector2(0.0f, 0.0f);
        public float x { get; private set; }
        public float y { get; private set; }

        public MVector2(float x, float y) 
        {
            this.x = x;
            this.y = y;
        }
        public MVector2(double x, double y)
        {
            this.x = (float)x;
            this.y = (float)y;
        }

        public MVector2(MVector2 mVector2)
        { 
            x = mVector2.x; 
            y = mVector2.y; 
        }

        public static implicit operator Vector2(MVector2 v) 
            => new Vector2(v.x, v.y);


        public void Add(MVector2 v)
        {
            x += v.x;
            y += v.y;
        }

        public void Rotate(float degree) 
        {
            var len = Length();
            var dx = x / len;
            var dy = y / len;

            x = x + (float)(dx * Math.Cos(degree) + dy * Math.Sin(degree));
            y = y + (float)(dx * -Math.Sin(degree) + dy * Math.Cos(degree));
        }

        public float Length() 
        {
            return (float) Math.Sqrt(x * x + y * y);        
        }

        public static float AngleBetweenVectors(MVector2 v, MVector2 w) 
        {
            var mag = v.Length() * w.Length();
            var dot = DotProduct(v, w);

            return (float) Math.Acos(dot / mag);
        }

        public static float DotProduct(MVector2 v, MVector2 w) 
            => (v.x * w.x) + (v.y * w.y);
        

        public static float Magnitude(MVector2 v) 
        {  
            return (float)Math.Sqrt(v.x * v.x + v.y * v.y); 
        }

        public static MVector2 Normalize(MVector2 v) 
        {
            var div = 1 / Magnitude(v);
            return new MVector2(v.x * div, v.y * div);
        }

        public static MVector2 Projection(MVector2 v, MVector2 w)
        {
            var dot = DotProduct(v, w);
            var lenSquared = Math.Pow(Magnitude(w),2);
            return new ((float)(dot / lenSquared) * w);
        }
        public void NormalizeThisVector() 
        {
            float len = Length();
            if (len == 0) x = y = 0;
            else if (len == 1) return; // already normalized
            else if (len < 0.00001f) { x = 0; y = 0; return; } // too small, set to zero
            else{x /= len;y /= len;}
        }
        
        public static bool operator ==(MVector2 v1, MVector2 v2)
        {
            if(v1.x == v2.x && v1.y == v2.y) return true;
            return false;
        }
        public static bool operator !=(MVector2 v1, MVector2 v2)
        {
            if (v1.x == v2.x && v1.y == v2.y) return false;
            return true;
        }
        public static MVector2 operator *(MVector2 v, float n)
            => new MVector2(v.x * n, v.y * n);
        public static MVector2 operator *(float n, MVector2 v)
            => new MVector2(v.x * n, v.y * n);
        public static MVector2 operator +(MVector2 v, MVector2 w)
            => new MVector2(v.x + w.x, v.y + w.y);
        public static MVector2 operator -(MVector2 v, MVector2 w)
            => new MVector2(v.x - w.x, v.y - w.y);
    }
}
