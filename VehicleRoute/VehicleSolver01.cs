using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleRoute
{
    public class VehicleSolver01 : IVehicleSolver
    {
        private List<Location> _remainingLocations;

        public void Execute(Location[] locations, Vehicle[] vehicles)
        {
            _remainingLocations = new List<Location>(locations.Where(l => l.Id != 0));

            foreach (var vehicle in vehicles)
            {
                 if (_remainingLocations.Count > 0)
                 {
                     SendVehicle(vehicle);
                 }
                 vehicle.SendHome();

                if (_remainingLocations.Sum(r => r.Demand) > vehicles.Count(v => v.Cost == 0.0)*vehicle.Capacity)
                    Console.WriteLine("too much capacity remaning");
            }

            if (_remainingLocations.Count > 0)
            {
                foreach (var remaining in _remainingLocations)
                    Console.WriteLine("{0} {1}", remaining.Id, remaining.Demand);
            }
        }

        private void SendVehicle(Vehicle vehicle)
        {
            var first = _remainingLocations.FirstOrDefault(vehicle.CanAssign);
            if (first == null) return;

            var nextLocation = _remainingLocations.Aggregate(first, (curr, next) =>
            {
                if (!vehicle.CanAssign(next)) return curr;
                if (next.DistanceFrom(vehicle.CurrentLocation) < curr.DistanceFrom(vehicle.CurrentLocation))
                    return next;
                return curr;
            });

            if (nextLocation == null) throw new Exception("something else went wrong");

            vehicle.Assign(nextLocation);
            _remainingLocations.Remove(nextLocation);
            SendVehicle(vehicle);
        }
    }
}