using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.IO;
using System.Globalization;
namespace Lab2
{
    struct Dataltem
    {
        public double x, y;
        public Vector2 vec;
        public Dataltem(double arc_x, double arc_y, Vector2 arc_vec)
        {
            x = arc_x;
            y = arc_y;
            vec = arc_vec;
        }
        public string TolongString(string format)
        {
            return "x:"+x.ToString(format) + "  y:" + y.ToString(format) + "\n" +"vector: "+vec.ToString();
        }
        public override string ToString()
        {
            return base.ToString();
        }

    }
    public delegate Vector2 FdblVector2(double x, double y);
    static class sta
    {
        public static Vector2 init_vector2(double x, double y)
        {
            return new Vector2((float)x, (float)y);
        }
    }
}
