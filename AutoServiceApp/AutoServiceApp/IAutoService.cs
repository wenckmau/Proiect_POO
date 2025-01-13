namespace AutoServiceApp
{
    public interface IAutoService
    {
        void AddUser(User user);
        User Authenticate(string email, string password);
        void SaveState(string filePath);
        void LoadState(string filePath);
        void AddPartOrder(CererePiese partOrder, User user);
        List<CererePiese> GetPartOrders(User user);
        void AddRequest(CerereRezolvare request, User user);
        List<CerereRezolvare> GetRequests(User user);
        void FinalizePartOrder(int partOrderId, User user);
    }
}