using System.Collections.Generic;
using System.Linq;

namespace VehicleRoute
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public List<Location> Locations { get; private set; }
        public double Cost { get; private set; }

        private int _availableCapacity;

        public Vehicle(int id, int capacity, Location startingPoint)
        {
            Id = id;
            Capacity = capacity;

            Locations = new List<Location> {startingPoint};
            _availableCapacity = capacity;
            Cost = 0;
        }

        public bool CanAssign(Location location)
        {
            return location.Demand <= _availableCapacity;
        }

        public void Assign(Location location)
        {
            _availableCapacity -= location.Demand;
            Cost += CurrentLocation.DistanceFrom(location);
            Locations.Add(location);
        }

        public Location CurrentLocation
        {
            get { return Locations.Last(); }
        }

        public void SendHome()
        {
            Assign(Locations.First());
        }
    }
}