using System.Collections.Generic;

namespace Knapsack
{
    public class KnapsackNode
    {
        public KnapsackNode(int value, int weight, int estimatedValue = 0)
        {
            AccumulatedValue = value;
            AccumulatedWeight = weight;
            SelectedItems = new List<int>();
            EstimatedValue = estimatedValue;
        }

        public int AccumulatedValue { get; private set; }
        public int AccumulatedWeight { get; private set; }
        public List<int> SelectedItems { get; private set; }
        public int EstimatedValue { get; set; }
    }
}