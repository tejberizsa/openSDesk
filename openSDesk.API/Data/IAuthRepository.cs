using System.Threading.Tasks;
using openSDesk.API.Dtos;
using openSDesk.API.Models;

namespace openSDesk.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
         Task<bool> EMailExists(string email);
         Task<User> ConfirmEmail(UserForConfirmDto userToConfirm);
    }
}