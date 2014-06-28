using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsp
{
    public class TspSolver01 : ITspSolver
    {
        private readonly double _threshold;

        public TspSolver01(double threashold)
        {
            _threshold = threashold;
        }

        public TspSolver01() : this(.75)
        {
            
        }

        public TspSolution Execute(TsPoint[] points)
        {
            var firstPoint = points.First();
            var solution = new TspSolution(firstPoint);

            //var alreadyVisited = new List<int> { firstPoint.Id };
            var notVisited = new List<TsPoint>(points.Where(p => p.Id != firstPoint.Id));

            var last = firstPoint;

            //while (alreadyVisited.Count != points.Length)
            while(notVisited.Any())
            {
                //var notVisited = points.Where(p => !alreadyVisited.Contains(p.Id)).ToArray();
                var next = GetClosestPoint(last, notVisited);

                //var nextNotVisited = points.Where(p => !alreadyVisited.Contains(p.Id) && p != next).ToArray();

                //check for potential orphans
                var pointsAbove = notVisited.Where(p => p != next && p.Y > last.Y).ToArray();
                var pointsBelow = notVisited.Where(p => p != next && p.Y < last.Y).ToArray();
                var pointsRight = notVisited.Where(p => p != next && p.X > last.X).ToArray();
                var pointsLeft = notVisited.Where(p => p != next && p.X < last.X).ToArray();

                TsPoint theAbove = null;
                TsPoint theBelow = null;
                TsPoint theRight = null;
                TsPoint theLeft = null;

                if (pointsAbove.Length == 1)
                {
                    theAbove = pointsAbove.Single();
                    if (next.DistanceFrom(last) / theAbove.DistanceFrom(last) < _threshold) theAbove = null;
                }
                if (pointsBelow.Length == 1)
                {
                    theBelow = pointsBelow.Single();
                    if (next.DistanceFrom(last) / theBelow.DistanceFrom(last) < _threshold) theBelow = null;
                }
                if (pointsRight.Length == 1)
                {
                    theRight = pointsRight.Single();
                    if (next.DistanceFrom(last) / theRight.DistanceFrom(last) < _threshold) theRight = null;
                }
                if (pointsLeft.Length == 1)
                {
                    theLeft = pointsLeft.Single();
                    if (next.DistanceFrom(last) / theLeft.DistanceFrom(last) < _threshold) theLeft = null;
                }

                var nextPoints = new[] {theAbove, theBelow, theRight, theLeft}.Where(p => p != null).ToList();
                
                if (nextPoints.Any())
                {
                    var unOrphaned = GetClosestPoint(last, nextPoints);
                    //alreadyVisited.Add(unOrphaned.Id);
                    notVisited.Remove(unOrphaned);
                    last = unOrphaned;
                    solution.AddNext(unOrphaned);
                }
                else
                {
                    //alreadyVisited.Add(next.Id);
                    notVisited.Remove(next);
                    last = next;
                    solution.AddNext(next);
                }
            }
            solution.Close();
            //solution.OutputToDebug();
            return solution;
        }

        private TsPoint GetClosestPoint(TsPoint referencePoint, List<TsPoint> points)
        {
            var result = points.Aggregate(points.First(),
                    (min, curr) =>
                    curr.DistanceFrom(referencePoint) < min.DistanceFrom(referencePoint) ? curr : min);

            return result;
        }
    }
}