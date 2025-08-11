using System;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas;
using Microsoft.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win2DApp.MyMath;
using Win2DApp.Utils;
using Microsoft.UI.Xaml.Media.Animation;

namespace Win2DApp.Programs
{
    internal class Vec2DProjection
    {
        public enum Handle { None, Dynamic, Static, Origin }
        
        private const float HitRadius = 10f;
        private const float FillRadius = 6f;
        private const float StrokeThickness = 2f;

        private MVector2 origin = new(200, 200);
        private MVector2 vDyn = new(50, 35);
        private MVector2 vStatic = new(200, 0);

        public Handle ActiveHandle {get; private set; } = Handle.None;

        public MVector2 Origin => origin;
        public MVector2 DynamicEnd => origin + vDyn;
        public MVector2 StaticEnd => origin + vStatic;

        public void MousePressed(in MVector2 pos) 
        {
            ActiveHandle = HitTest(pos);
            switch (ActiveHandle)
            {
                case Handle.Dynamic: vDyn = pos - origin; break;
                case Handle.Static: vStatic = pos - origin; break;
                case Handle.Origin: origin = pos; break;
                default: break;
        }
        }
        public void MouseDragged(in MVector2 pos)
        {
            switch (ActiveHandle)
            {
                case Handle.Dynamic: vDyn = pos - origin; break;
                case Handle.Static: vStatic = pos - origin; break;
                case Handle.Origin: origin = pos; break;
            }
        }

        public void MouseReleased() => ActiveHandle = Handle.None;
        public void Draw(CanvasDrawingSession d)
        {
            // vectors
            d.DrawLine(origin, StaticEnd, Colors.Red, StrokeThickness);
            d.DrawLine(origin, DynamicEnd, Colors.Yellow, StrokeThickness);

            // projection (guard zero static)
            if (LengthSq(vStatic) > 1e-6f)
        {
                var proj = MVector2.Projection(vDyn, vStatic);
                d.DrawLine(DynamicEnd, origin + proj, Colors.Green, StrokeThickness);
            }

            // handles
            DrawHandle(d, DynamicEnd, ActiveHandle == Handle.Dynamic);
            DrawHandle(d, StaticEnd, ActiveHandle == Handle.Static);
            DrawHandle(d, origin, ActiveHandle == Handle.Origin);
        }
        
        private Handle HitTest(MVector2 pos) 
        { 
            if (DistSq(DynamicEnd, pos) <= HitRadius * HitRadius) return Handle.Dynamic;
            if (DistSq(StaticEnd, pos) <= HitRadius * HitRadius) return Handle.Static;
            if (DistSq(origin, pos) <= HitRadius * HitRadius) return Handle.Origin;
            return Handle.None;
        }

        private static float DistSq(in MVector2 a, in MVector2 b) => LengthSq(a - b);
        private static float LengthSq(in MVector2 v) => v.x * v.x + v.y * v.y;

        private static void DrawHandle(CanvasDrawingSession d, MVector2 p, bool active)
        {
            if (active) d.FillCircle(p, FillRadius, Colors.MintCream);
            else d.DrawCircle(p, HitRadius, Colors.Red);
        }
        // draw two initial vectors
        // function that checks if mouse pressing on a circle
        // track mouse position and update the dynamic vector
    }
}
