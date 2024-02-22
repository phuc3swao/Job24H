using viecLam24hBE.Commons;
using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public interface UserService
    {
        public User GetUserByEmailAndPassword(User user);


        List<User> GetUsers();

        public User GetUserById(int id);

        public void UpdateUser(User user);

        public void InsertUser(User user);
    }
}
