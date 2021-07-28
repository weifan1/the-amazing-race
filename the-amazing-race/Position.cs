using System;

namespace the_amazing_race
{
    class Position
    {
        public static Position operator +(Position a, Position b) => new Position(
                a.X + b.X,
                a.Y + b.Y);
        public static Position operator -(Position a, Position b) => new Position(
                a.X - b.X,
                a.Y - b.Y);
        public static Position operator *(Position p, double scalar) => new Position(
                p.X * scalar,
                p.Y * scalar);
        public static Position operator *(double scalar, Position p) => new Position(
                p.X * scalar,
                p.Y * scalar);
        public static Position operator /(Position p, double scalar) => new Position(
                p.X / scalar,
                p.Y / scalar);  //  no point in catching the DivideByZeroException, only to rethrow
        public static Position operator /(double scalar, Position p) => new Position(
                p.X / scalar,
                p.Y / scalar);  //  no point in catching the DivideByZeroException, only to rethrow

        public readonly double X;
        public readonly double Y;

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetAngleTo(Position someOtherPosition) => (this - someOtherPosition).GetAngle();

        public double GetAngle()
        {
            if (X == 0 && Y == 0)
            {
                return Double.NaN;
            }

            if (X == 0)
            {
                return Y > 0
                        ? Math.PI / 2
                        : Math.PI * 3 / 2;
            }

            if (Y == 0)
            {
                return X > 0
                        ? 0
                        : Math.PI;
            }

            return Math.Atan(Y / X) + (X < 0 ? Math.PI : 0);
        }

        public Position Rotate(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Position(
                    cos * X - sin * Y,
                    sin * X + cos * Y);
        }

        public double GetDistanceTo(Position someOtherPosition) => (someOtherPosition - this).GetLength();

        public double GetLength() => Math.Sqrt(X * X + Y * Y);

        public double Dot(Position someOtherPosition) => (this.X * someOtherPosition.X + this.Y * someOtherPosition.Y);
    }
}
