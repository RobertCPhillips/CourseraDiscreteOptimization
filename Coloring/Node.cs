using System.Collections.Generic;
using System.Linq;

namespace Coloring
{
    public class Node
    {
        private readonly List<Edge> _edges;
        private readonly List<Node> _nodes;
        private readonly List<int> _unavailableColors; 

        public Node(int id)
        {
            Id = id;
            _edges = new List<Edge>();
            _nodes = new List<Node>();
            _unavailableColors = new List<int>();
        }

        public int Id { get; private set; }

        public int? ColorId { get; set; }

        public int UnavailableCount { get { return _unavailableColors.Count; } }

        public void AddEdge(Edge edge)
        {
            if(_edges.All(e => e.Id != edge.Id)) _edges.Add(edge);

            if (edge.Left.Id != Id) _nodes.Add(edge.Left);
            else if (edge.Right.Id != Id) _nodes.Add(edge.Right);
        }

        public bool CanAssignColor(int color)
        {
            return !_nodes.Any(n => n.ColorId.HasValue && n.ColorId == color);
        }

        public void AssignColor(int color)
        {
            ColorId = color;
            foreach (var node in _nodes.Where(n => !n.ColorId.HasValue))
            {
                node.RemoveColor(color);
            }
        }

        public void RemoveColor(int color)
        {
            if (!_unavailableColors.Contains(color)) _unavailableColors.Add(color);
        }

        public int EdgesWithColorCount
        {
            get
            {
                return _edges.Sum(e =>
                    {
                        if (e.Left.ColorId.HasValue || e.Right.ColorId.HasValue)
                            return 1;
                        return 0;
                    });
            }
        }

        public int EdgeCount
        {
            get { return _edges.Count; }
        }
    }
}