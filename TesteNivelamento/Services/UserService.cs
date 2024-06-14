using TesteNivelamento.Data;
using TesteNivelamento.Interfaces;
using TesteNivelamento.Models;

namespace TesteNivelamento.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;

        }
        


       public User Authetificate(string username, string password)
        {
            return _context.Users.SingleOrDefault(x => x.UserName.Equals(username) && x.Password.Equals(password));
        }

        public User Create(User user, string password)
        {
            user.Password = password;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User userParam, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userParam.Id);

            if (user != null)
            {
                user.UserName = userParam.UserName;
                if (!string.IsNullOrWhiteSpace(password))
                {
                    user.Password = password;
                }
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }
    }
}
