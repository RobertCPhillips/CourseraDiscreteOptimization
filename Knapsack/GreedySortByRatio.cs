using System;
using System.Linq;

namespace Knapsack
{
    public class GreedySortByRatio : IKnapSackSolver
    {
        public void Execute(int capacity, KnapsackItem[] ksItems)
        {
            var collectedWeight = 0;
            var itemsToUpdate = ksItems.OrderByDescending(k => Convert.ToDouble(k.Value) / Convert.ToDouble(k.Weight))
                                       .ToArray();

            foreach (var itemToUpdate in itemsToUpdate)
            {
                if (itemToUpdate.Weight + collectedWeight <= capacity)
                {
                    collectedWeight += itemToUpdate.Weight;
                    itemToUpdate.Selected = 1;
                }
                if (collectedWeight == capacity) break;
            }
        }
    }
}