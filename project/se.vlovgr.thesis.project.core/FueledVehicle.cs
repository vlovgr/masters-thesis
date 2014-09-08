namespace se.vlovgr.thesis.project.core
{
    public class FueledVehicle : Vehicle
    {
        private readonly int _fuelCapacity;

        public FueledVehicle(int id, int fuelCapacity) : base(id)
        {
            _fuelCapacity = fuelCapacity;
        }

        public int GetFuelCapacity()
        {
            return _fuelCapacity;
        }
    }
}