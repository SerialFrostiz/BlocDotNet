using Bloc_dotnet.Models;

namespace Bloc_dotnet.Datas
{
    public interface IUserService
    {
        public User AddUser(User user);
        public User GetUserById(int idUser);
        public List<User> GetUsers();
        public User UpdateUser(User user);
        public bool RemoveUser(int idUser);
        public bool Login(User user);
        public bool IsUser();
    }
}
