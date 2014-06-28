namespace Knapsack
{
    public class KnapsackItem
    {
        public KnapsackItem(int id, int value, int weight)
        {
            Id = id;
            Value = value;
            Weight = weight;
            EstimatedValue = value;
        }

        public int Id { get; private set; }
        public int Value { get; private set; }
        public int Weight { get; private set; }

        public int Selected { get; set; }
        public int EstimatedValue { get; set; }
    }
}