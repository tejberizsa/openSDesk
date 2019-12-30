using System.Collections.Generic;
using System.Threading.Tasks;
using openSDesk.API.Helpers;
using openSDesk.API.Models;

namespace openSDesk.API.Data
{
    public interface ITicketRepository
    {
        void Add<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Ticket>> GetTicketThread(TicketParams ticketParams);
        Task<Ticket> GetTicket(int ticketId);
        Task<bool> AssignToUser(int ticketId, int userId);
        Task<bool> AssignToGroup(int ticketId, int groupId);
        Task<bool> UpdateStatus(int ticketId, int statusId, int subStatusId);
        Task<bool> ResolveTicket(int ticketId, Resolution resolution);
        Task<bool> SurveyTicket(int ticketId, Survey survey);
    }
}