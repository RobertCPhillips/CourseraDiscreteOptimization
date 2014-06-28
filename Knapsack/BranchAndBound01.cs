using System.Linq;

namespace Knapsack
{
    public class BranchAndBound01 : IKnapSackSolver
    {
        private KnapsackNode _bestNode;
        private int _capacity;

        public void Execute(int capacity, KnapsackItem[] ksItems)
        {
            _bestNode = new KnapsackNode(0, 0);
            _capacity = capacity;

            for (var i = 0; i < ksItems.Length; i++)
            {
                var itemsToProcess = ksItems.Skip(i).ToArray();
                var firstItem = itemsToProcess.First();
                
                var firstNode = new KnapsackNode(firstItem.Value, firstItem.Weight);
                firstNode.SelectedItems.Add(firstItem.Id);

                var remaining = itemsToProcess.Skip(1).ToArray();
                BuildTree(firstNode, remaining);
            }

            foreach (var i in _bestNode.SelectedItems)
            {
                var ksItem = ksItems.Single(p => p.Id == i);
                ksItem.Selected = 1;
            }
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
            if (rest.Any(k => k.Weight + node.AccumulatedWeight <= _capacity) && (rest.Sum(k => k.Value) + node.AccumulatedValue) > _bestNode.AccumulatedValue)
            {
                BuildTree(node, rest);
            }
        }
    }
}