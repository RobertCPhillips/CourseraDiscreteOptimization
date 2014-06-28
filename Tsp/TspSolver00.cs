using System.Linq;

namespace Tsp
{
    public class TspSolver00 : ITspSolver
    {
        public TspSolution Execute(TsPoint[] points)
        {
            var sortedPoints = points.OrderBy(p => p.X).ThenBy(p => p.Y);

            var firstPoint = sortedPoints.First();
            var solution = new TspSolution(firstPoint);

            foreach (var point in sortedPoints.Skip(1))
                solution.AddNext(point);

            solution.Close();
            return solution;
        }
    }
}