using System.Collections.Generic;
using System.Threading.Tasks;
using openSDesk.API.Helpers;
using openSDesk.API.Models;

namespace openSDesk.API.Data
{
    public interface IApplicationRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(PageParams userParams);
         Task<User> GetUser(int id);
         Task<Message> GetMessage(int id);
         Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
         Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
         Task<UserPhoto> GetUserPhoto(int id);
         Task<UserPhoto> GetMainPhotoForUser(int userId);
    }
}