using System.Threading.Tasks;
using openSDesk.API.Models;

namespace openSDesk.API.Data
{
    public interface ISettingsRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<bool> UpdateCategory(Category category, string text);
        Task<bool> AssignUserToGroup(int userId, int groupId);
        Task<bool> RemoveUserFromGroup(int userId, int groupId);
        Task<bool> UpdateSubStatus(int subStatusId, string text);
    }
}