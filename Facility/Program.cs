using System;
using System.Globalization;
using System.Linq;

namespace Facility
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No input...");
                return;
            }

            var file = args[0];
            var fileItems = System.IO.File.ReadAllLines(file);
            var firstLine = fileItems[0].Split(' ');
            int facilityCount = Int32.Parse(firstLine[0]);
            int customerCount = Int32.Parse(firstLine[1]);

            var facilities = fileItems.Skip(1).Take(facilityCount).Select((p, i) =>
            {
                var fileItem = p.Split(' ');
                return new Facility(i, Double.Parse(fileItem[0], NumberStyles.Float), Int32.Parse(fileItem[1]), Double.Parse(fileItem[2], NumberStyles.Float), Double.Parse(fileItem[3], NumberStyles.Float));
            }).ToArray();

            var customers = fileItems.Skip(1 + facilityCount).Take(customerCount).Select((p, i) =>
            {
                var fileItem = p.Split(' ');
                return new Customer(i, Int32.Parse(fileItem[0]), Double.Parse(fileItem[1], NumberStyles.Float), Double.Parse(fileItem[2], NumberStyles.Float));
            }).ToArray();

            //IFacilitySolver solver = new FacilitySolver01();
            IFacilitySolver solver = new FacilitySolver02();
            solver.Execute(facilities, customers);

            var valueSum = facilities.Sum(f => f.TotalCost);
            Console.Out.WriteLine("{0} 0", valueSum);

            foreach (var customer in customers)
                Console.Out.Write("{0} ", customer.AssignedFacility.Id);

            //new Fixer01().FixIt(facilities, customers);
        }

        public interface IFixer
        {
            void FixIt(Facility[] facilities, Customer[] customers);
        }

        public class Fixer01 : IFixer
        {
            public void FixIt(Facility[] facilities, Customer[] customers)
            {
                var sortedByAssignedCount = facilities.OrderBy(f => f.AssignedFacilityCount);

                Console.WriteLine("");
                Console.WriteLine("***********");
                foreach (var facility in sortedByAssignedCount)
                {
                    Console.WriteLine("{0} {1} {2} {3}", facility.Id, facility.AssignedFacilityCount, facility.Capacity, facility.AvailableCapacity);
                }
            }
        }
    }
}
