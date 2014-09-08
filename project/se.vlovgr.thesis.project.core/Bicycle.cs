namespace se.vlovgr.thesis.project.core
{
    public class Bicycle : NonFueledVehicle
    {
        private readonly string _description;

        public Bicycle(int id, string description) : base(id)
        {
            _description = description;
        }

        public string GetDescription()
        {
            return _description;
        }
    }
}