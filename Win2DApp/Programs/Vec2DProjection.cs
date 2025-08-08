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

namespace Win2DApp.Programs
{
    internal class Vec2DProjection
    {
        /// <summary>
        /// this class is used to project a 2D vector onto another 2D vector.
        /// </summary>
        
        public bool isMousePessedOnCircle = false;

        readonly MVector2 offset = new (200, 200);

        MVector2 dynamicVector = new(50, 35);
        static readonly MVector2 staticVector = new(200, 0);
        public Vec2DProjection(){}
        public void MoussePressed(MVector2 v)
        {
            if (IsMpressedOnCircle(v))
            {
                dynamicVector = v - offset;
                isMousePessedOnCircle = true;
            }else isMousePessedOnCircle = false;
        }
        bool IsMpressedOnCircle(MVector2 mousePos)
        {
            var pos = offset + dynamicVector;
            var dist = MVector2.Magnitude(pos - mousePos);
            if (dist < 10) return true;
            return false;
        }
        
        public void DrawVectors(CanvasDrawingSession d)
        { 
            d.DrawLine(offset, offset + staticVector, Colors.Red, 2);
            d.DrawLine(offset, offset + dynamicVector, Colors.Blue, 2);

            var projection = MVector2.Projection(dynamicVector, staticVector);
            d.DrawLine(offset + dynamicVector, offset + projection, Colors.Green, 2);

            if(isMousePessedOnCircle) d.FillCircle(offset + dynamicVector, 10, Colors.Red);
            else d.DrawCircle(offset + dynamicVector, 10, Colors.Red);
        }
        // draw two initial vectors
        // function that checks if mouse pressing on a circle
        // track mouse position and update the dynamic vector
    }
}
