using System;
using System.Globalization;
using System.Linq;

namespace VehicleRoute
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No input...");
                return;
            }

            var file = args[0];
            var fileItems = System.IO.File.ReadAllLines(file);

            var parameters = fileItems[0].Split(' ');
            var vehicleCount = Int32.Parse(parameters[1]);
            var capacity = Int32.Parse(parameters[2]);

            var locations = fileItems.Skip(1).Select((p, i) =>
                {
                    var fileItem = p.Split(' ');
                    return new Location(i, Double.Parse(fileItem[1], NumberStyles.Float),
                                        Double.Parse(fileItem[2], NumberStyles.Float), Int32.Parse(fileItem[0]));
                }).ToArray();

            var startingPoint = locations.Single(l => l.Demand == 0);

            var vehicles =
                Enumerable.Range(0, vehicleCount).Select(i => new Vehicle(i, capacity, startingPoint)).ToArray();

            IVehicleSolver solver = new VehicleSolver01();
            solver.Execute(locations, vehicles);

            var valueSum = vehicles.Sum(f => f.Cost);
            Console.Out.WriteLine("{0} 0", valueSum);

            foreach (var vehicle in vehicles)
            {
                foreach (var location in vehicle.Locations)
                    Console.Out.Write("{0} ", location.Id);
                Console.WriteLine();
            }
        }
    }
}
