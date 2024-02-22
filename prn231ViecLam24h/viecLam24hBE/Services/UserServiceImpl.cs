using Microsoft.EntityFrameworkCore;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public class UserServiceImpl : UserService
    {
        private readonly MyDbContext _context;

        public UserServiceImpl(MyDbContext context)
        {
            _context = context;
        }
        public User GetUserByEmailAndPassword(User user)
        {
            var findUser = _context.Users
                .FirstOrDefault(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password));

            return findUser;           
        }

        public User GetUserById(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);

                return user;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public List<User> GetUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi trong hàm Get Users, chi tiết lỗi: " + ex.Message);
                return null;
            }
        }

        public void InsertUser(User user)
        {
            try
            {
                if (user == null) return; 
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi trong hàm Insert User, chi tiết lỗi: " + ex.Message);
                return;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                if (user == null) return;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;

            }
        }
    }
}
