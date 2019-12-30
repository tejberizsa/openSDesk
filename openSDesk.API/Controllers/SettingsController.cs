using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using openSDesk.API.Data;
using openSDesk.API.Helpers;
using openSDesk.API.Models;

namespace openSDesk.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository _setRepo;

        public SettingsController(ISettingsRepository setRepo)
        {
            _setRepo = setRepo;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, string text)
        {
            if (await _setRepo.UpdateCategory(id, text))
            {
                if (await _setRepo.SaveAll())
                    return Ok($"Category {id} updated");
            
                throw new Exception($"Updating category {id} failed on save");
            }
            throw new Exception($"Category {id} not found, failed to update");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, string text)
        {
            if (await _setRepo.UpdateStatus(id, text))
            {
                if (await _setRepo.SaveAll())
                    return Ok($"Status {id} updated");
            
                throw new Exception($"Updating status {id} failed on save");
            }
            throw new Exception($"Status {id} not found, failed to update");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateSubStatus(int id, string text)
        {
            if (await _setRepo.UpdateSubStatus(id, text))
            {
                if (await _setRepo.SaveAll())
                    return Ok($"Sub-status {id} updated");
            
                throw new Exception($"Updating sub-status {id} failed on save");
            }
            throw new Exception($"Sub-status {id} not found, failed to update");
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AssignUserToGroup(int userId, int groupId)
        {
            await _setRepo.AssignUserToGroup(userId, groupId);
            if (await _setRepo.SaveAll())
                    return Ok($"User assigned to group");
            
            throw new Exception($"Update failed on save");
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> RemoveUserFromGroup(int userId, int groupId)
        {
            await _setRepo.RemoveUserFromGroup(userId, groupId);
            if (await _setRepo.SaveAll())
                    return Ok($"User unassigned from group");
            
            throw new Exception($"Update failed on save");
        }

        [HttpPost]
        public async Task<IActionResult> AddUserGroup(string groupName)
        {
            if (await _setRepo.CheckGroupExist(groupName))
                return BadRequest("Group name already exist");
            
            var groupToAdd = new UserGroup()
            {
                Name = groupName
            };
            _setRepo.Add(groupToAdd);

            if (await _setRepo.SaveAll())
                    return Ok($"New group created");

            throw new Exception($"Creation {groupName} group is failed on save");
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> RemoveUserGroup(int groupId)
        {
            var groupToDelete = await _setRepo.GetUserGroup(groupId);
            if (groupToDelete == null)
                return BadRequest("Group not found");
            
            var assignmentsToDelete = await _setRepo.GetGroupAssignemntsById(groupId);
            foreach (var assignmentToDelete in assignmentsToDelete)
            {
                _setRepo.Delete(assignmentToDelete);
            }
            _setRepo.Delete(groupToDelete);

            if (await _setRepo.SaveAll())
                    return Ok($"Group deleted");

            throw new Exception($"Remove {groupToDelete.Name} group is failed on save");
        }
    }
}