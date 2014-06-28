using System;
using System.Globalization;
using System.Linq;

namespace Tsp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No input...");
                return;
            }

            var file = args[0];
            var fileItems = System.IO.File.ReadAllLines(file);

            var points = fileItems.Skip(1).Select((p, i) =>
            {
                var fileItem = p.Split(' ');
                return new TsPoint(i, Double.Parse(fileItem[0], NumberStyles.Float), Double.Parse(fileItem[1], NumberStyles.Float));
            }).ToArray();

            //ITspSolver solver = new TspSolver00();
            //ITspSolver solver = new TspSolver01();
            //ITspSolver solver = new TspSolver02();
            //ITspSolver solver = new TspSolver03();
            //ITspSolver solver = new TspSolver04();
            ITspSolver solver = new TspSolver05();

            var solution = solver.Execute(points);
            if (solution.Route.Count != points.Length)
                throw new Exception("missing points");
            //new Fixer01().FixIt(solution);

            var valueSum = solution.Distance;
            Console.Out.WriteLine("{0} 0", valueSum);

            foreach (var pointId in solution.Route)
                Console.Out.Write("{0} ", pointId.Id);
        }
    }

    public class TspSolver05 : ITspSolver
    {
        public TspSolution Execute(TsPoint[] points)
        {
            TspSolution current = null;
            var sortedPoints = points.OrderBy(p => p.X + p.Y).ToArray();

            for (double ts = .5; ts < 1.00; ts = ts + .01)
            {
                var newSolution = new TspSolver01(ts).Execute(sortedPoints);
                if (current == null || newSolution.Distance < current.Distance) current = newSolution;
            }

            if (current != null) current.OutputToDebug();
            return current;
        }
    }
}
