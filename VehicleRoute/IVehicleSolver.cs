namespace VehicleRoute
{
    public interface IVehicleSolver
    {
        void Execute(Location[] locations, Vehicle[] vehicles);
    }
}