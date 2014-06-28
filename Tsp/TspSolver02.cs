using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsp
{
    public class TspGroup
    {
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        public int SortNumber { get; set; }
    }

    public class TspSolver02 : ITspSolver
    {
        private TspSolution _solution;
 
        public TspSolution Execute(TsPoint[] points)
        {
            var minX = points.Min(p => p.X);
            var maxX = points.Max(p => p.X);
            var minY = points.Min(p => p.Y);
            var maxY = points.Max(p => p.Y);

            const int pageSize = 7;
            var stepX = (maxX - minX) / pageSize;
            var stepY = (maxY - minY) / pageSize;

            var groups = new List<TspGroup>();

            var goingDown = false;
            var sortNumber = 0;

            for (var x = minX; x < maxX; x = x + stepX)
            {
                if (goingDown) 
                    sortNumber += pageSize-1;
                else if (sortNumber > 0)
                    sortNumber += pageSize+1;

                for (var y = minY; y < maxY; y = y + stepY)
                {
                    var sort = goingDown ? sortNumber-- : sortNumber++;
                    //Console.WriteLine(sort);

                    var group = new TspGroup {SortNumber = sort, MinX = x, MaxX = x+stepX, MinY = y, MaxY = y+stepY};
                    groups.Add(group);

                    if ((y + stepY) > (maxY - (stepY / pageSize)))
                    {
                        group.MaxY = maxY + 1;
                        y = maxY;
                    }
                    if ((x + stepX) > (maxX - (stepX / pageSize)))
                    {
                        group.MaxX = maxX + 1;
                    }
                }

                if ((x + stepX) > (maxX - (stepX / pageSize)))
                {
                    x = maxX;
                }

                goingDown = !goingDown;
            }

            var sortedGroups = groups.OrderBy(g => g.SortNumber).ToArray();

            for (var i = 0; i < sortedGroups.Length; i++)
            {
                var group = sortedGroups[i];
                bool goingUp = ((group.SortNumber / pageSize) % 2) == 0;
                TsPoint[] pointsPage;

                var pointsPage1 = points.Where(p =>
                                          p.X >= group.MinX && p.X < group.MaxX &&
                                          p.Y >= group.MinY && p.Y < group.MaxY);

                if (goingUp) pointsPage = pointsPage1.OrderBy(p => p.X).ThenBy(p => p.Y).ToArray();
                else pointsPage = pointsPage1.OrderBy(p => p.X).ThenByDescending(p => p.Y).ToArray();

                var firstPoint = pointsPage.First();
                if (_solution == null)
                {
                    _solution = new TspSolution(firstPoint);
                }
                else
                {
                    var lastItem = _solution.LastItem;
                    firstPoint = pointsPage.Aggregate(firstPoint,
                                                    (min, curr) =>
                                                    curr.DistanceFrom(lastItem) < min.DistanceFrom(lastItem) ? curr : min);
                    _solution.AddNext(firstPoint);
                }

                var alreadyVisited = new List<int> { firstPoint.Id };
                var last = firstPoint;

                while (alreadyVisited.Count != pointsPage.Length)
                {
                    var notVisited = pointsPage.Where(p => !alreadyVisited.Contains(p.Id)).ToArray();
                    var next = notVisited.Aggregate(notVisited.First(),
                                                    (min, curr) =>
                                                    curr.DistanceFrom(last) < min.DistanceFrom(last) ? curr : min);

                    alreadyVisited.Add(next.Id);
                    last = next;
                    _solution.AddNext(next);
                }
            }
            _solution.Close();
            return _solution;
        }
    }
}