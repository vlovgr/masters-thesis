namespace se.vlovgr.thesis.project.core
{
    public class Vehicle
    {
        private readonly int _id;

        public Vehicle(int id)
        {
            _id = id;
        }

        public int GetId()
        {
            return _id;
        }
    }
}