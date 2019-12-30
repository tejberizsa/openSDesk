using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using openSDesk.API.Data;
using openSDesk.API.Helpers;

namespace openSDesk.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly IMapper _mapper;

        public TicketController(ITicketRepository ticketRepo, IMapper mapper)
        {
            _ticketRepo = ticketRepo;
            _mapper = mapper;
        }
    }
}