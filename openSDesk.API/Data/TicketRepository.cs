using System.Collections.Generic;
using System.Threading.Tasks;
using openSDesk.API.Helpers;
using openSDesk.API.Models;

namespace openSDesk.API.Data
{
    public class TicketRepository : ITicketRepository
    {
        void ITicketRepository.Add<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> ITicketRepository.SaveAll()
        {
            throw new System.NotImplementedException();
        }

        Task<bool> ITicketRepository.AssignToGroup(int ticketId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> ITicketRepository.AssignToUser(int ticketId, int userId)
        {
            throw new System.NotImplementedException();
        }

        Task<Ticket> ITicketRepository.GetTicket(int ticketId)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<Ticket>> ITicketRepository.GetTicketThread(TicketParams ticketParams)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> ITicketRepository.ResolveTicket(int ticketId, Resolution resolution)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> ITicketRepository.SurveyTicket(int ticketId, Survey survey)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> ITicketRepository.UpdateStatus(int ticketId, Status status)
        {
            throw new System.NotImplementedException();
        }
    }
}