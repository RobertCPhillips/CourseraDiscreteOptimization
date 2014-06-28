using System.Linq;

namespace Coloring
{
    public class GreedyColorSolver03 : IColorSolver
    {
        public void Execute(Node[] nodes, Edge[] edges)
        {
            var colors = Enumerable.Range(0, nodes.Length).ToArray();

            var sortedNodes = nodes.OrderByDescending(n => n.EdgeCount).ToArray();

            foreach (var node in sortedNodes)
            {
                foreach (var color in colors)
                {
                    if (node.CanAssignColor(color))
                    {
                        node.ColorId = color;
                        break;
                    }
                }
            }
        }
    }
}