using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using openSDesk.API.Data;
using openSDesk.API.Dtos;
using openSDesk.API.Helpers;
using openSDesk.API.Models;

namespace openSDesk.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly IApplicationRepository _appRepo;
        private readonly IMapper _mapper;

        public TicketController(ITicketRepository ticketRepo, IApplicationRepository appRepo, IMapper mapper)
        {
            _ticketRepo = ticketRepo;
            _appRepo = appRepo;
            _mapper = mapper;
        }

        [HttpPost("AddTicket")]
        public async Task<IActionResult> AddTicket(TicketForAddDto ticketForAddDto)
        {
            var requesterFromRepo = await _appRepo.GetUser(ticketForAddDto.RequesterId);

            if (requesterFromRepo == null)
                return BadRequest("Requester not exist");
            
            if (ticketForAddDto.RequesterId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) && User.IsInRole("User"))
                return Unauthorized();
            
            var ticketToCreate = _mapper.Map<Ticket>(ticketForAddDto);

            _ticketRepo.Add(ticketToCreate);

            if (await _ticketRepo.SaveAll()) 
            {
                var ticketToReturn = _mapper.Map<TicketForDetailedDto>(ticketToCreate);
                return CreatedAtRoute("GetTicket", new {id = ticketToCreate.Id}, ticketToReturn);
            }

            throw new System.Exception("Failed to save ticket");
        }

        [HttpGet("{id}", Name = "GetTicket")]
        public async Task<IActionResult> GetTicket(int id)
        {
            Post post = await _repo.GetPostByID(id, true);

            var postToReturn = _mapper.Map<PostForDetailedDto>(post);

            if (User.Identity.IsAuthenticated)
                postToReturn.IsFollowedByCurrentUser = post.PostFollower.Any(f => f.FollowerId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            return Ok(postToReturn);
        }
    }
}