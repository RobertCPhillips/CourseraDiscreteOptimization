namespace Facility
{
    public class Facility
    {
        private double _distanceSum;

        public Facility(int id, double setupCost, int capacity, double x, double y)
        {
            Id = id;
            SetupCost = setupCost;
            Capacity = capacity;
            Location = new Location(id, x, y);

            AvailableCapacity = capacity;
        }

        public int Id { get; set; }
        public double SetupCost { get; set; }
        public int Capacity { get; set; }
        public Location Location { get; set; }

        public double TotalCost
        {
            get
            {
                return (_distanceSum > 0) ?_distanceSum + SetupCost : 0;
            }
        }

        public int AssignedFacilityCount { get; private set; }

        public int AvailableCapacity { get; private set; }

        public double CostToAssign
        {
            get { return AssignedFacilityCount > 0 ? 0 : SetupCost; }
        }

        public bool CanAssignCustomer(Customer customer)
        {
            return AvailableCapacity >= customer.Demand;
        }

        public void AssignCustomer(Customer customer)
        {
            customer.AssignedFacility = this;
            AvailableCapacity -= customer.Demand;
            _distanceSum += Location.DistanceFrom(customer.Location);
            AssignedFacilityCount += 1;
        }
    }
}