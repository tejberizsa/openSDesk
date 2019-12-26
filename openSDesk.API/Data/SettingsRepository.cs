using System.Threading.Tasks;
using openSDesk.API.Models;

namespace openSDesk.API.Data.SeedData
{
    public class SettingsRepository : ISettingsRepository
    {
        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AssignUserToGroup(int userId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveUserFromGroup(int userId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCategory(Category category, string text)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateSubStatus(int subStatusId, string text)
        {
            throw new System.NotImplementedException();
        }
    }
}