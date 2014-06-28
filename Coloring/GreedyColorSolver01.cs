namespace Coloring
{
    public class GreedyColorSolver01 : IColorSolver
    {
        public void Execute(Node[] nodes, Edge[] edges)
        {
            foreach (var edge in edges)
            {
                if (!edge.Left.ColorId.HasValue)
                {
                    edge.Left.ColorId = edge.Left.Id;
                }
                
                if (!edge.Right.ColorId.HasValue)
                {
                    edge.Right.ColorId = edge.Right.Id;
                }
            }
        }
    }
}