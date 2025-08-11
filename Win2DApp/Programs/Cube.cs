using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win2DApp.MyMath;
using Win2DApp.Utils;

namespace Win2DApp.Programs
{
    

    internal class Cube
    {
        public readonly List<MVector3> Vertices = [];
        public List<MVector3> CopyVertices { get; set; } = new();
        (int, int, int)[] Triangles = new (int, int, int)[12];

        public Cube() 
        {
            FillVertices();
            FillTriangles();
        }

        void FillVertices() 
        {
            Vertices.Add(new(-1, -1, -1));
            Vertices.Add(new( 1, -1, -1));
            Vertices.Add(new( 1,  1, -1));
            Vertices.Add(new(-1,  1, -1));
            Vertices.Add(new(-1, -1,  1));
            Vertices.Add(new( 1, -1,  1));
            Vertices.Add(new( 1,  1,  1));
            Vertices.Add(new(-1,  1,  1));
        }
        void FillTriangles()
        {
            Triangles[0] = (0, 1, 2);
            Triangles[1] = (0, 2, 3);
            Triangles[2] = (1, 5, 6);
            Triangles[3] = (1, 6, 2);
            Triangles[4] = (0, 4, 5);
            Triangles[5] = (0, 5, 1);
            Triangles[6] = (3, 7, 4);
            Triangles[7] = (3, 4, 0);
            Triangles[8] = (3, 6, 7);
            Triangles[9] = (3, 2, 6);
            Triangles[10] = (7, 6, 5);
            Triangles[11] = (7, 5, 4);
        }
        public void DrawCube(CanvasAnimatedDrawEventArgs e) 
        {
            var d = e.DrawingSession;

            CopyVertices = Vertices.Select(v => new MVector3(v.x, v.y, v.z)).ToList();

            foreach (var v in CopyVertices) 
            {
                v.x += 1f;
                v.y += 1f;
                v.z += 1f;

                v.x *= (ScreenSize.Width / 2);
                v.y *= (ScreenSize.Height / 2);
            }

            for(int i = 0; i < Triangles.Length; ++i) 
            {
                float x1 = CopyVertices[Triangles[i].Item1].x;
                float y1 = CopyVertices[Triangles[i].Item1].y;

                float x2 = CopyVertices[Triangles[i].Item2].x;
                float y2 = CopyVertices[Triangles[i].Item2].y;

                float x3 = CopyVertices[Triangles[i].Item3].x;
                float y3 = CopyVertices[Triangles[i].Item3].y;

                d.DrawLine(x1, y1, x2, y2, Colors.White); 
                d.DrawLine(x2, y2, x3, y3, Colors.White);
                d.DrawLine(x3, y3, x1, y1, Colors.White);
            }
        }
    }
}
