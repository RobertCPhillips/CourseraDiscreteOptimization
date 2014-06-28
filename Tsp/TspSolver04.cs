using System.Collections.Generic;
using System.Linq;

namespace Tsp
{
    public class TspSolver04 : ITspSolver
    {
        private readonly double _threshold;
        private readonly double _steps;

        public TspSolver04()
        {
            _threshold = .85;
            _steps = 6;
        }

        public TspSolution Execute(TsPoint[] points)
        {
            var minX = points.Min(p => p.X);
            var maxX = points.Max(p => p.X);
            var increment = (maxX - minX) / _steps;

            TspSolution solution = null;

            for (int i = 1; i <= _steps; i++)
            {
                var range = minX + increment;
                if (i == _steps) ++range;
                var thesePoints = points.Where(p => p.X >= minX && p.X < range).ToList();

                TsPoint firstPoint;
                if (solution == null)
                {
                    firstPoint = thesePoints.OrderBy(p => p.Y).First();
                    solution = new TspSolution(firstPoint);
                }
                else
                {
                    firstPoint = GetClosestPoint(solution.LastItem, thesePoints);
                    solution.AddNext(firstPoint);
                }
                
                CreateSolution(thesePoints, solution, firstPoint);
                minX = range;
            }

            if (solution != null)
            {
                solution.Close();
                solution.OutputToDebug();
            }
            return solution;
        }

        private void CreateSolutionOld(TsPoint[] points, TspSolution solution, TsPoint firstPoint)
        {
            var alreadyVisited = new List<int> { firstPoint.Id };

            var last = firstPoint;

            while (alreadyVisited.Count != points.Length)
            {
                var notVisited = points.Where(p => !alreadyVisited.Contains(p.Id)).ToArray();
                var next = notVisited.Aggregate(notVisited.First(),
                                                (min, curr) =>
                                                curr.DistanceFrom(last) < min.DistanceFrom(last) ? curr : min);

                if (next.Y < solution.LastItem.Y)
                {
                    //i went down, so is there only 1 left above me?
                    var above = points.Where(p => !alreadyVisited.Contains(p.Id) && p != next && p.Y > next.Y).ToArray();
                    if (above.Length == 1)
                    {
                        var theAbove = above.Single();
                        if (next.DistanceFrom(last) / theAbove.DistanceFrom(last) > .95)
                            next = theAbove;
                    }
                }
                else
                {
                    //i went up, so is there only 1 left below me?
                    var below = points.Where(p => !alreadyVisited.Contains(p.Id) && p != next && p.Y < next.Y).ToArray();
                    if (below.Length == 1)
                    {
                        var theBelow = below.Single();
                        if (next.DistanceFrom(last) / theBelow.DistanceFrom(last) > .95)
                            next = theBelow;
                    }
                }

                alreadyVisited.Add(next.Id);
                last = next;
                solution.AddNext(next);
            }
        }

        private void CreateSolution(IEnumerable<TsPoint> points, TspSolution solution, TsPoint firstPoint)
        {
            var notVisited = new List<TsPoint>(points.Where(p => p.Id != firstPoint.Id));
            var last = firstPoint;

            while (notVisited.Any())
            {
                var next = GetClosestPoint(last, notVisited);

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

                var nextPoints = new[] { theAbove, theBelow, theRight, theLeft }.Where(p => p != null).ToList();

                if (nextPoints.Any())
                {
                    var unOrphaned = GetClosestPoint(last, nextPoints);
                    notVisited.Remove(unOrphaned);
                    last = unOrphaned;
                    solution.AddNext(unOrphaned);
                }
                else
                {
                    notVisited.Remove(next);
                    last = next;
                    solution.AddNext(next);
                }
            }
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