namespace Facility
{
    public class Customer
    {
        public Customer(int id, int demand, double x, double y)
        {
            Id = id;
            Demand = demand;
            Location = new Location(id, x, y);
        }
        public int Id { get; set; }
        public int Demand { get; set; }
        public Location Location { get; set; }

        public Facility AssignedFacility { get; set; }
    }
}