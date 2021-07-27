using System;
using System.Collections.Generic;
using System.Text;

namespace the_amazing_race_wei_fan
{
    class Position
    {
        public double x;
        public double y;

        public Position(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public double DistanceTo(Position someOtherPosition)
        {
            double dX = x - someOtherPosition.x;
            double dY = y - someOtherPosition.y;
            return Math.Sqrt(dX * dX + dY * dY);
        }
    }
}
