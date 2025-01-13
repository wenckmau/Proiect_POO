using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoServiceApp
{
    public class AutoService : IAutoService
    {
        private List<User> Users { get; set; } = new List<User>();
        private List<CerereRezolvare> Cereri { get; set; } = new List<CerereRezolvare>();
        private List<CererePiese> PartOrders { get; set; } = new List<CererePiese>();

        private readonly StateManager _stateManager;

        public AutoService()
        {
            _stateManager = new StateManager();
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public User Authenticate(string email, string password)
        {
            return Users.FirstOrDefault(u => u.Email == email && u.Parola == password);
        }

        public void SaveState(string filePath)
        {
            var state = new State
            {
                Users = Users,
                Cereri = Cereri,
                PartOrders = PartOrders
            };
            _stateManager.SaveState(filePath, state);
        }

        public void LoadState(string filePath)
        {
            var state = _stateManager.LoadState<State>(filePath);
            Users = state.Users;
            Cereri = state.Cereri;
            PartOrders = state.PartOrders;
        }

        public void AddPartOrder(CererePiese partOrder, User user)
        {
            if (user is Mechanic)
            {
                PartOrders.Add(partOrder);
            }
            else
            {
                throw new UnauthorizedAccessException("Only mechanics can add part orders.");
            }
        }

        public List<CererePiese> GetPartOrders(User user)
        {
            if (user is Administrator)
            {
                return PartOrders;
            }
            else
            {
                throw new UnauthorizedAccessException("Only administrators can view part orders.");
            }
        }

        public void AddRequest(CerereRezolvare request, User user)
        {
           
        }

        public List<CerereRezolvare> GetRequests(User user)
        {
            if (user is Administrator)
            {
                return Cereri;
            }
            else
            {
                throw new UnauthorizedAccessException("Only administrators can view requests.");
            }
        }

        public void FinalizePartOrder(int partOrderId, User user)
        {
           
        }

        public List<CerereRezolvare> GetCereri()
        {
            return Cereri;
        }

        public List<CererePiese> GetPartOrders()
        {
            return PartOrders;
        }
    }
}