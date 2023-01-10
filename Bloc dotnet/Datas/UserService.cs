using Bloc_dotnet.Models;
using Bloc_dotnet.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;


namespace Bloc_dotnet.Datas
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public User AddUser(User user)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] bytes = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                user.Password = builder.ToString();
            }      
        _context.Users.Add(user);
            if (_context.SaveChanges() > 0)
            {
                return user;
            }
            return new User();
        }

        public User GetUserById(int idUser)
        {
            return _context.Users.FirstOrDefault(site => site.IdUser == idUser);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool RemoveUser(int idUser)
        {
            User user = _context.Users.FirstOrDefault(x => x.IdUser == idUser);
            if (user == null)
            {
                return false;
            }else
            {
                _context.Users.Remove(user);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            
        }

        public User UpdateUser(User updatedUser)
        {
            User user = _context.Users.FirstOrDefault(x => x.IdUser ==  updatedUser.IdUser);
            if (user == null)
            {
                return new User();
                
            }
            else
            {
                user.Login = updatedUser.Login;
                if(updatedUser.Password!= null && updatedUser.Password != "")
                {
                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        byte[] bytes = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(updatedUser.Password));
                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            builder.Append(bytes[i].ToString("x2"));
                        }                        
                        user.Password = builder.ToString();
                    }                   
                }               
                _context.Users.Update(user);
                if(_context.SaveChanges() > 0)
                {
                    return user;
                }
                return new User();
            }
        }

        public bool Login(User user)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] bytes = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                string hashedPassword = builder.ToString();
                User connectedUser = _context.Users.Where(u => u.Login == user.Login).Where(u => u.Password == hashedPassword).FirstOrDefault();
                if (connectedUser != null)
                {
                    return true;
                }
                return false;
            }
            
        }
        public bool IsUser()
        {
            var userCookie = _httpContextAccessor.HttpContext.Request.Cookies["Role"];
            if (userCookie != null)
            {
                return true;
            }
            return false;
        }
    }
}
