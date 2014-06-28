using System;
using System.Linq;

namespace Knapsack
{
    public class BranchAndBound02 : IKnapSackSolver
    {
        private KnapsackNode _bestNode;
        private int _capacity;

        public void Execute(int capacity, KnapsackItem[] ksItems)
        {
            _capacity = capacity;

            for (var i = 0; i < ksItems.Length; i++)
            {
                var itemsToProcess = ksItems.Skip(i).ToArray();
                var estimatedValue = GetEstimatedValue(itemsToProcess);
                var firstItem = itemsToProcess.First();
                
                var firstNode = new KnapsackNode(firstItem.Value, firstItem.Weight, estimatedValue);
                firstNode.SelectedItems.Add(firstItem.Id);
                if (_bestNode == null) _bestNode = firstNode;

                if (estimatedValue <= _bestNode.AccumulatedValue) continue;

                var remaining = itemsToProcess.Skip(1).ToArray();
                BuildTree(firstNode, remaining);
            }

            foreach (var i in _bestNode.SelectedItems)
            {
                var ksItem = ksItems.Single(p => p.Id == i);
                ksItem.Selected = 1;
            }
        }

        private int GetEstimatedValue(KnapsackItem[] ksItems)
        {
            var collectedWeight = 0;
            var estimatedValue = 0;
            var itemsByRatio = ksItems.OrderByDescending(k => Convert.ToDouble(k.Value) / Convert.ToDouble(k.Weight));

            foreach (var item in ksItems) item.EstimatedValue = item.Value;

            foreach (var item in itemsByRatio)
            {
                if (collectedWeight + item.Weight <= _capacity)
                {
                    collectedWeight += item.Weight;
                    estimatedValue += item.Value;
                    if (collectedWeight == _capacity) break;
                }
                else
                {
                    var percentOfItem = (_capacity - collectedWeight) / Convert.ToDouble(item.Weight);
                    var partialValue = Convert.ToInt32(percentOfItem*item.Value);
                    estimatedValue += partialValue;
                    item.EstimatedValue = partialValue;
                    break;
                }
            }
            return estimatedValue;
        }

        private void BuildTree(KnapsackNode node, KnapsackItem[] ksItems)
        {
            //if possible add contents of next ksitem to node
            //  if so recruse with remaining items
            //  if not determine if best and set if so

            //also, recurse with unchanged node items and remaining items

            if (ksItems.Length == 0) return;

            var item = ksItems.First();
            var newValue = node.AccumulatedValue + item.Value;
            var newWeight = node.AccumulatedWeight + item.Weight;
            var remainingCapacity = _capacity - newWeight;

            var rest = ksItems.Skip(1).ToArray();

            //if i was able to add something
            if (remainingCapacity >= 0)
            {
                var newNode = new KnapsackNode(newValue, newWeight);
                newNode.SelectedItems.AddRange(node.SelectedItems);
                newNode.SelectedItems.Add(item.Id);

                if (remainingCapacity > 0) BuildTree(newNode, rest);
                if (newNode.AccumulatedValue > _bestNode.AccumulatedValue) _bestNode = newNode;
            }

            //this is to use first node but not adding current item
            //if (rest.Any(k => k.Weight + node.AccumulatedWeight <= _capacity) && (rest.Sum(k => k.Value) + node.AccumulatedValue) > _bestNode.AccumulatedValue)
            //{
            var newEstimatedValue = node.EstimatedValue - item.EstimatedValue;
            if (newEstimatedValue > _bestNode.AccumulatedValue)
            {
                node.EstimatedValue = newEstimatedValue;
                BuildTree(node, rest);
            }
            //}
        }
    }
}