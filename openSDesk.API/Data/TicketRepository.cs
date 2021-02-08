using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using openSDesk.API.Helpers;
using openSDesk.API.Models;

namespace openSDesk.API.Data
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Ticket> GetTicket(int ticketId)
        {
            var ticket = await _context.Tickets.Include(t => t.Source)
                                               .Include(t => t.Status)
                                               .Include(t => t.SubStatus)
                                               .Include(t => t.Category)
                                               .Include(t => t.Notes).ThenInclude(n => n.Owner)
                                               .Include(t => t.Resolutions).ThenInclude(r => r.Owner)
                                               .Include(t => t.Surveys).ThenInclude(s => s.Response)
                                               .Include(t => t.Requester)
                                               .Include(t => t.AssignmentGroup)
                                               .Include(t => t.AssignedTo)
                                               .FirstOrDefaultAsync(t => t.Id == ticketId);

            return ticket;
        }

        // public async Task<bool> ResolveTicket(int ticketId, Resolution resolution)
        // {
        //     var ticketToResolve = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        //     if (ticketToResolve == null)
        //     {
        //         return false;
        //     }
        //     ticketToResolve.Resolutions.Add(resolution);
        //     return await SaveAll();
        // }

        // public async Task<bool> SurveyTicket(int ticketId, Survey survey)
        // {
        //     var ticketToSurvay = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        //     if (ticketToSurvay == null)
        //     {
        //         return false;
        //     }
        //     ticketToSurvay.Surveys.Add(survey);
        //     if (survey.Response.Refusal)
        //     {
        //         foreach(var resolution in ticketToSurvay.Resolutions)
        //         {
        //             resolution.Refused = true;
        //         }
        //     }
        //     return await SaveAll();
        // }

        public async Task<bool> UpdateStatus(int ticketId, int statusId, int subStatusId)
        {
            var ticketToUpdate = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticketToUpdate == null)
            {
                return false;
            }
            ticketToUpdate.StatusId = statusId;
            ticketToUpdate.SubStatusId = subStatusId;
            return await SaveAll();
        }

        public async Task<bool> AssignToGroup(int ticketId, int groupId)
        {
            var ticketToAssign = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticketToAssign == null)
            {
                return false;
            }
            ticketToAssign.AssignedTo = null;
            ticketToAssign.AssignmentGroupId = groupId;
            return await SaveAll();
        }

        public async Task<bool> AssignToUser(int ticketId, int userId)
        {
            var ticketToAssign = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticketToAssign == null)
            {
                return false;
            }
            ticketToAssign.AssignedToId = userId;
            return await SaveAll();
        }

        public async Task<IEnumerable<Ticket>> GetTicketThread(TicketParams ticketParams)
        {
            var tickets = _context.Tickets.Include(t => t.Category)
                                          .Include(t => t.Requester)
                                          .Include(t => t.AssignmentGroup)
                                          .Include(t => t.AssignedTo)
                                          .Include(t => t.Status)
                                          .Include(t => t.SubStatus)
                                          .Include(t => t.Resolutions)
                                          .Include(t => t.Surveys).ThenInclude(s => s.Response)
                                          .AsQueryable();
            
            User user;
            switch (ticketParams.TicketContainer)
            {
                case "Unassigned": // clerk group tickets to be assigned
                    user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ticketParams.UserId.Value);
                    tickets = tickets.Where(t => t.AssignedTo == null && 
                                                 user.Groups.Any(g => g.UserGroupId == t.AssignmentGroupId &&
                                                 (t.Resolutions == null ||
                                                 t.Resolutions != null && t.Resolutions.Any(r => r.Refused == true))));
                    break;
                case "AssignedToGroup": //  assigned to somebody in clerk group
                    user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ticketParams.UserId.Value);
                    tickets = tickets.Where(t => t.AssignedTo != null && 
                                                 user.Groups.Any(g => g.UserGroupId == t.AssignmentGroupId &&
                                                 (t.Resolutions == null ||
                                                 t.Resolutions != null && t.Resolutions.Any(r => r.Refused == true))));
                    break;
                case "AssignedTo": // assigned to current clerk
                    tickets = tickets.Where(t => t.AssignedToId == ticketParams.UserId.Value && 
                                                 (t.Resolutions == null ||
                                                 t.Resolutions != null && t.Resolutions.Any(r => r.Refused == true)));
                    break;
                case "ResolvedByGroup": // resolved by clerk group
                    user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ticketParams.UserId.Value);
                    tickets = tickets.Where(t => t.AssignedTo != null && 
                                                 user.Groups.Any(g => g.UserGroupId == t.AssignmentGroupId &&
                                                 (t.Resolutions != null && t.Resolutions.Any(r => r.Refused == false))));
                    break;
                case "ResolvedBy": // resolved by current clerk
                    tickets = tickets.Where(t => t.AssignedToId == ticketParams.UserId.Value && 
                                                 (t.Resolutions != null && t.Resolutions.Any(r => r.Refused == false)));
                    break;
                case "Requests": // not resolved jet requested by current user
                    tickets = tickets.Where(t => t.RequesterId == ticketParams.UserId.Value &&
                                                 (t.Resolutions == null ||
                                                 t.Resolutions != null && t.Resolutions.Any(r => r.Refused == true)));
                    break;
                default: // case "Resolved" resolved requests of current user
                    tickets = tickets.Where(t => t.RequesterId == ticketParams.UserId.Value &&
                                                 t.Resolutions != null && t.Resolutions.Any(r => r.Refused == false));
                    break;
            }
            
            tickets = tickets.OrderByDescending(t => t.CreatedAt);
            return await PagedList<Ticket>.CreateAsync(tickets, ticketParams.PageNumber, ticketParams.PageSize);
        }

        public async Task<bool> ResponseIsRefusal(int surveyResponseId)
        {
            var response = await _context.SurvayResponses.FirstOrDefaultAsync(sr => sr.Id == surveyResponseId);
            return response.Refusal;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users
                                        .Where(u => u.Deleted == false)
                                        .OrderBy(u => u.Username)
                                        .ToListAsync();
            return users;
        }
    }
}