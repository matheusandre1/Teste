using TesteNivelamento.Models;

namespace TesteNivelamento.Interfaces
{
    public interface IUserService
    {
        User Authetificate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password);        
    }
}
