using System;
using System.Linq;

namespace Tsp
{
    public class Fixer01 : IFixer
    {
        public void FixIt(TspSolution solution)
        {
            var route = solution.Route;

            double largestDistance = 0;
            TsPoint largest1 = null;
            TsPoint largest2 = null;
            for (var i = 0; i < route.Count; ++i)
            {
                TsPoint last;

                if (i == 0)
                {
                    last = solution.Route[i];
                    ++i;
                }
                else
                {
                    last = solution.Route[i - 1];
                }
                TsPoint current = solution.Route[i];
                var distance = last.DistanceFrom(current);
                if (distance > largestDistance)
                {
                    largestDistance = distance;
                    largest1 = last;
                    largest2 = current;
                }
            }

            Console.WriteLine("largest distance is between {0} and {1} with distance {2}", largest1, largest2, largestDistance);

            var closest1 = solution.Route.Aggregate(solution.Route.First(p => p != largest1),
                                                    (min, curr) =>
                                                    curr != largest1 && (curr.DistanceFrom(largest1) < min.DistanceFrom(largest1)) ? curr : min);

            Console.WriteLine("closest to {0} is {1} with distance of {2}", largest1, closest1, closest1.DistanceFrom(largest1));

            var closest2 = solution.Route.Aggregate(solution.Route.First(p => p != largest2),
                                                    (min, curr) =>
                                                    curr != largest2 && (curr.DistanceFrom(largest2) < min.DistanceFrom(largest2)) ? curr : min);

            Console.WriteLine("closest to {0} is {1} with distance of {2}", largest2, closest2, closest2.DistanceFrom(largest2));


            TsPoint c1Left = null;
            TsPoint c1Right = null;

            for (int i = 0; i < solution.Route.Count; i++)
            {
                if (solution.Route[i].Id != closest1.Id) continue;

                c1Left = solution.Route[i - 1];
                c1Right = solution.Route[i + 1];
            }
            if (c1Left == null || c1Right == null)
                Console.WriteLine("didnt find for c1");
            else
                Console.WriteLine("{0} has left {1} with distance of {2} and right {3} with distance of {4}", closest1, c1Left, closest1.DistanceFrom(c1Left), c1Right, closest1.DistanceFrom(c1Right));
        }
    }
}