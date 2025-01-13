namespace AutoServiceApp
{
    public class State
    {
        private List<User> _users;
        private List<CerereRezolvare> _cereri;
        private List<CererePiese> _partOrders;
        public List<User> Users { get=>_users; set=>_users=value; }
        public List<CerereRezolvare> Cereri { get=>_cereri; set=>_cereri=value; }
        public List<CererePiese> PartOrders { get=>_partOrders; set=>_partOrders=value; }
    }
}
