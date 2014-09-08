namespace se.vlovgr.thesis.project.core
{
    public class Driver
    {
        private readonly string _name;

        public Driver(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public string Drive(Car car)
        {
            return string.Format("{0} drives {1}", _name, car.GetDescription());
        }
    }
}
