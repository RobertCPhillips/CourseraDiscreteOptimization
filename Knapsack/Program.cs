using System;
using System.Linq;

namespace Knapsack
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

            var parameters = fileItems[0].Split(' ');
            var itemCount = Int32.Parse(parameters[0]);
            var capacity = Int32.Parse(parameters[1]);

            var ksItems = fileItems.Skip(1).Select((p, i) =>
                {
                    var fileItem = p.Split(' ');
                    return new KnapsackItem(i, Int32.Parse(fileItem[0]), Int32.Parse(fileItem[1]));
                }).ToArray();

            IKnapSackSolver solver = new GreedySortByRatio();
            //IKnapSackSolver solver = new BranchAndBound01();
            //IKnapSackSolver solver = new BranchAndBound02();

            solver.Execute(capacity, ksItems);

            var valueSum = ksItems.Sum(p => p.Value * p.Selected);
            Console.Out.WriteLine("{0} 0", valueSum);

            foreach (var ksItem in ksItems)
                Console.Out.Write("{0} ", ksItem.Selected);
        }
    }
}
