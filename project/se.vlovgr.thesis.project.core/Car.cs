namespace se.vlovgr.thesis.project.core
{
    public class Car : FueledVehicle
    {
        private readonly string _description;

        public Car(int id, int fuelCapacity, string description) : base(id, fuelCapacity)
        {
            _description = description;
        }

        public string GetDescription()
        {
            return _description;
        }
    }
}