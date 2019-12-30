using System.Collections.Generic;
using System.Threading.Tasks;
using openSDesk.API.Models;

namespace openSDesk.API.Data
{
    public interface ISettingsRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<bool> UpdateCategory(int categoryId, string text);
        Task<bool> AssignUserToGroup(int userId, int groupId);
        Task<bool> RemoveUserFromGroup(int userId, int groupId);
        Task<bool> UpdateStatus(int statusId, string text);
        Task<bool> UpdateSubStatus(int subStatusId, string text);
        Task<bool> CheckGroupExist(string groupName);
        Task<UserGroup> GetUserGroup(int groupId);
        Task<IEnumerable<UserGroupAssingment>> GetGroupAssignemntsById(int groupId);
    }
}