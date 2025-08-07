using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win2DApp.MyMath;

namespace Win2DApp.MyUtils
{
    internal class Triangle
    {
        List<MVector2> vertices = new();
        bool trigIsFull = false;
        public Triangle(){}

        public void DrawTriangle(CanvasAnimatedDrawEventArgs args) 
        {
            var d = args.DrawingSession;
            var num = vertices.Count;
            for (int i = 0; i < vertices.Count; ++i)
            {
                if (num > 1)
                    d.DrawLine(vertices[i], vertices[(i + 1) % num], Microsoft.UI.Colors.White);
            }
        }

        public void AddVertex(MVector2 vertex) 
        {
            if (!trigIsFull)
            {

                if (vertices.Count == 0) vertices.Add(vertex);
                else if (vertices.Count == 1) vertices.Add(vertex);
                else if (vertices.Count == 2)
                {
                    vertices.Add(vertex);
                    trigIsFull = true;
                }
            }
        }
    }
}
