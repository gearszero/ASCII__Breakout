using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektBreakout
{
    public struct Coordinate
    {
        public float y;
        public float x;

        public Coordinate(float y, float x)
        {
            this.y = y;
            this.x = x;
        }

        public static Coordinate operator +(Coordinate p, Vector v)
        {
            return new Coordinate(p.y + v.y, p.x + v.x);
        }
    }
}