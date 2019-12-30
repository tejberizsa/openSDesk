using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using openSDesk.API.Models;

namespace openSDesk.API.Data
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly DataContext _context;
        public SettingsRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AssignUserToGroup(int userId, int groupId)
        {
            await _context.AddAsync(new UserGroupAssingment() {
                UserId = userId,
                UserGroupId = groupId
            });
            return true;
        }

        public async Task<bool> RemoveUserFromGroup(int userId, int groupId)
        {
            var groupAssignment = await _context.UserGroupAssingments
                                                .FirstOrDefaultAsync(uga => uga.UserId == userId && 
                                                                     uga.UserGroupId == groupId);
            if (groupAssignment == null)
                return false;

            _context.Remove(groupAssignment);
            return true;
        }

        public async Task<bool> UpdateCategory(int categoryId, string text)
        {
            var categoryToUpdate = await _context.Categories
                                                 .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (categoryToUpdate == null)
                return false;

            categoryToUpdate.Text = text;
            return true;
        }

        public async Task<bool> UpdateStatus(int statusId, string text)
        {
            var statusToUpdate = await _context.Statuses
                                               .FirstOrDefaultAsync(s => s.Id == statusId);
            if (statusToUpdate == null)
                return false;

            statusToUpdate.Text = text;
            return true;
        }

        public async Task<bool> UpdateSubStatus(int subStatusId, string text)
        {
            var subStatusToUpdate = await _context.SubStatuses
                                               .FirstOrDefaultAsync(s => s.Id == subStatusId);
            if (subStatusToUpdate == null)
                return false;

            subStatusToUpdate.Text = text;
            return true;
        }

        public async Task<bool> CheckGroupExist(string groupName)
        {
            if (await _context.UserGroups.AnyAsync(g => g.Name.ToLower() == groupName.ToLower()))
                return true;
            else
                return false;
        }

        public async Task<UserGroup> GetUserGroup(int groupId)
        {
            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(ug => ug.Id == groupId);
            return userGroup;
        }

        public async Task<IEnumerable<UserGroupAssingment>> GetGroupAssignemntsById(int groupId)
        {
            var groupAssignments = await _context.UserGroupAssingments
                                                 .Where(uga => uga.UserGroupId == groupId)
                                                 .ToListAsync();

            return groupAssignments;
        }
    }
}