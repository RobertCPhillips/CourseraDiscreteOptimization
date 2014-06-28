using System.Collections.Generic;
using System.Linq;

namespace Tsp
{
    public class TspSolver03 : ITspSolver
    {
        public TspSolution Execute(TsPoint[] points)
        {
            var minX = points.Min(p => p.X);
            var maxX = points.Max(p => p.X);
            var rangeMinX = 1.1*minX;
            var rangeMaxX = .9*maxX;

            TsPoint upperRight = null;
            TsPoint upperLeft = null;
            TsPoint lowerRight = null;
            TsPoint lowerLeft = null;

            foreach (var point in points.Skip(1))
            {
                if (point.X >= rangeMaxX)
                {
                    if (upperRight == null || point.Y >= upperRight.Y) upperRight = point;
                    if (lowerRight == null || point.Y <= lowerRight.Y) lowerRight = point;
                }
                else if(point.X <= rangeMinX)
                {
                    if (upperLeft == null || point.Y >= upperLeft.Y) upperLeft = point;
                    if (lowerLeft == null || point.Y <= lowerLeft.Y) lowerLeft = point;                 
                }
            }

            var solution1 = CreateSolution(points.Where(p => p != upperRight).ToArray(), upperRight);
            solution1.OutputToDebug();

            var solution2 = CreateSolution(points.Where(p => p != upperRight).ToArray(), lowerRight);
            solution2.OutputToDebug();
            if (solution2.Distance < solution1.Distance) solution1 = solution2;

            var solution3 = CreateSolution(points.Where(p => p != upperRight).ToArray(), upperLeft);
            solution3.OutputToDebug();
            if (solution3.Distance < solution1.Distance) solution1 = solution3;
            
            var solution4 = CreateSolution(points.Where(p => p != upperRight).ToArray(), lowerLeft);
            solution4.OutputToDebug();
            if (solution4.Distance < solution1.Distance) solution1 = solution4;

            return solution1;
        }

        private TspSolution CreateSolution(TsPoint[] points, TsPoint firstPoint)
        {
            var solution = new TspSolution(firstPoint);

            var alreadyVisited = new List<int> { firstPoint.Id };

            var last = firstPoint;

            while (alreadyVisited.Count != points.Length)
            {
                var notVisited = points.Where(p => !alreadyVisited.Contains(p.Id)).ToArray();
                var next = notVisited.Aggregate(notVisited.First(),
                                                (min, curr) =>
                                                curr.DistanceFrom(last) < min.DistanceFrom(last) ? curr : min);

                alreadyVisited.Add(next.Id);
                last = next;
                solution.AddNext(next);
            }
            solution.Close();
            return solution;
        }
    }
}