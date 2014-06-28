using System;
using System.Collections.Generic;
using System.Linq;


namespace VertTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var points = new[] {1,2,3,4,5,6};

            //start at point 1, end at point 6

            var solutions = new List<int[]>();

            var pointsToEnumerate = points.Skip(1).Take(4).ToArray();

            foreach (var point in pointsToEnumerate)
            {
                var pointsToSpan = pointsToEnumerate.Where(p => p != point).ToArray();
                var solution = new List<int>(point);
            }
        }

        static List<int[]> BuildSolutions()
        {
            return new List<int[]>();
        }
    }
}
