using System;
using System.Linq;

namespace Facility
{
    public class FacilitySolver01 : IFacilitySolver
    {
        public void Execute(Facility[] facilities, Customer[] customers)
        {
            foreach (var customer in customers)
            {
                var first = facilities.FirstOrDefault(f => f.CanAssignCustomer(customer));
                if (first == null) throw new Exception("something went wrong");

                var actual = facilities.Aggregate(first, (curr, next) =>
                    {
                        if (!next.CanAssignCustomer(customer)) return curr;
                        if (next.Location.DistanceFrom(customer.Location) < curr.Location.DistanceFrom(customer.Location))
                            return next;
                        return curr;
                    });

                if (actual == null) throw new Exception("something else went wrong");
                if (!actual.CanAssignCustomer(customer)) throw new Exception("couldn't find one...");

                actual.AssignCustomer(customer);
            }
        }
    }

    public class FacilitySolver02 : IFacilitySolver
    {
        public void Execute(Facility[] facilities, Customer[] customers)
        {
            foreach (var customer in customers)
            {
                var first = facilities.FirstOrDefault(f => f.CanAssignCustomer(customer));
                if (first == null) throw new Exception("something went wrong");

                var actual = facilities.Aggregate(first, (curr, next) =>
                {
                    if (!next.CanAssignCustomer(customer)) return curr;
                    var nextCost = next.Location.DistanceFrom(customer.Location) + next.CostToAssign;
                    var currCost = curr.Location.DistanceFrom(customer.Location) + curr.CostToAssign;
                    if (nextCost < currCost)
                        return next;
                    return curr;
                });

                if (actual == null) throw new Exception("something else went wrong");
                if (!actual.CanAssignCustomer(customer)) throw new Exception("couldn't find one...");

                actual.AssignCustomer(customer);
            }
        }
    }
}