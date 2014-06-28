using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tsp
{
    public class TspSolution
    {
        private readonly List<TsPoint> _points;
 
        public TspSolution(TsPoint firstPoint)
        {
            _points = new List<TsPoint> { firstPoint };
        }

        public double Distance { get; private set; }
        public IList<TsPoint> Route { get { return _points; } }
        public TsPoint LastItem { get { return _points.Last(); } }

        public void AddNext(TsPoint point)
        {
            var distanceFromLast = point.DistanceFrom(_points.Last());
            Distance += distanceFromLast;
            _points.Add(point);
        }

        public void AddLast(TsPoint point)
        {
            AddNext(point);
            var distanceToBeginning = point.DistanceFrom(_points.First());
            Distance += distanceToBeginning;
        }

        public void Close()
        {
            var distanceToBeginning = _points.Last().DistanceFrom(_points.First());
            Distance += distanceToBeginning;
        }

        public void OutputToDebug()
        {
            Debug.WriteLine("--------------------------------");
            foreach (var p in Route)
            {
                Debug.WriteLine(p);
            }
            Debug.WriteLine("--------------------------------");
        }
    }
}