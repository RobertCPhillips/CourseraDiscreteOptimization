using System;
using System.Diagnostics;
using System.Linq;

namespace Coloring
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
            var nodeCount = Int32.Parse(parameters[0]);
            var edgeCount = Int32.Parse(parameters[1]);


            var nodes = Enumerable.Range(0, nodeCount)
                .Select(n => new Node(n))
                .ToArray();

            var edges = fileItems.Skip(1).Select((p, i) =>
            {
                var fileItem = p.Split(' ');
                var leftId = Int32.Parse(fileItem[0]);
                var rightId = Int32.Parse(fileItem[1]);
                var left = nodes.Single(n => n.Id == leftId);
                var right = nodes.Single(n => n.Id == rightId);

                var edge = new Edge(i, left, right);
                left.AddEdge(edge);
                right.AddEdge(edge);

                return edge;
            }).ToArray();

            //IColorSolver colorSolver = new GreedyColorSolver01();
            //IColorSolver colorSolver = new GreedyColorSolver03();
            IColorSolver colorSolver = new GreedyColorSolver04();
            colorSolver.Execute(nodes, edges);

            var colorCount = nodes.Select(n => n.ColorId).Distinct().Count();
            Console.Out.WriteLine("{0} 0", colorCount);

            foreach (var node in nodes)
                Console.Out.Write("{0} ", node.ColorId);

            //var colorList = ColorList.GetRgbList(nodeCount).ToArray();
            //foreach (var n in nodes)
            //{
            //    Debug.WriteLine("{0} [style=filled fillcolor = \"{1}\" label=\"{0}_{2}\"];", n.Id, colorList[n.ColorId.Value], n.ColorId.Value);
            //}
            //foreach (var e in edges)
            //{
            //    Debug.WriteLine("{0} -- {1};", e.Left.Id, e.Right.Id);
            //}
        }
    }
}
