using System;

namespace Tsp
{
    public class TsPoint
    {
        public TsPoint(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        public int Id { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public double DistanceFrom(TsPoint point)
        {
            return Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2));
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", X, Y);
        }

        public static bool operator ==(TsPoint a, TsPoint b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Id == b.Id;
        }

        public static bool operator !=(TsPoint a, TsPoint b)
        {
            return !(a == b);
        }

        protected bool Equals(TsPoint other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TsPoint)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}