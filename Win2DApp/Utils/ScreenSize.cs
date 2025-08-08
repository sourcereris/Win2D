using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win2DApp.Utils
{
    internal class ScreenSize
    {
        public static float Width { get; private set; }
        public static float Height { get; private set; }
        public static void SetSize(float width, float height)
        {
            Width = width;
            Height = height;
        }
        public static void SetSize(double width, double height)
        {
            Width = (float)width;
            Height = (float)height;
        }
    }
}
