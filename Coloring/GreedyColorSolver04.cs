using System;
using System.Linq;

namespace Coloring
{
    public class GreedyColorSolver04 : IColorSolver
    {
        public void Execute(Node[] nodes, Edge[] edges)
        {
            var colors = Enumerable.Range(0, nodes.Length).ToArray();

            var sortedNodes = nodes.Where(n => !n.ColorId.HasValue).OrderByDescending(n => n.EdgeCount).ThenByDescending(n => n.UnavailableCount);

            do
            {
                var node = sortedNodes.First();
                //Console.Write("select node {0} with edge count {1} and unavaiable count {2}", node.Id, node.EdgeCount, node.UnavailableCount);
                foreach (var color in colors)
                {
                    if (node.CanAssignColor(color))
                    {
                        //Console.WriteLine("...assigned color {0}", color);
                        node.AssignColor(color);
                        break;
                    }
                }

                sortedNodes = nodes.Where(n => !n.ColorId.HasValue).OrderByDescending(n => n.EdgeCount).ThenByDescending(n => n.UnavailableCount);

            } while (sortedNodes.Any());

            var usedColors = nodes.Select(n => n.ColorId).Distinct().ToArray();
            int origCount;

            do
            {
                origCount = usedColors.Length;

                foreach (var color in usedColors)
                {
                    var nodesWithColor = nodes.Where(n => n.ColorId == color).ToArray();

                    foreach (var node in nodesWithColor)
                    {
                        foreach (var color2 in usedColors.Where(u => u != color).ToArray())
                        {
                            if (color2 != node.ColorId && node.CanAssignColor(color2.Value))
                            {
                                node.ColorId = color2;
                                break;
                            }
                        }
                    }
                }

                usedColors = nodes.Select(n => n.ColorId).Distinct().ToArray();

            } while (usedColors.Length < origCount);
        }
    }
}