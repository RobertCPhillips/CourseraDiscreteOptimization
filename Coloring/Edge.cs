namespace Coloring
{
    public class Edge
    {
        public Edge(int id, Node left, Node right)
        {
            Id = id;
            Left = left;
            Right = right;
        }

        public int Id { get; private set; }
        public Node Left { get; private set; }
        public Node Right { get; private set; }
    }
}