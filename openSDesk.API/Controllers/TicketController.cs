using System.Collections.Generic;
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

        [HttpPost("{ticketId}/AddNote")]
        public async Task<IActionResult> AddNote(int ticketId, NoteForAddDto noteForAddDto)
        {
            var ownerFromRepo = await _appRepo.GetUser(noteForAddDto.OwnerId);

            if (ownerFromRepo == null)
                return BadRequest("Owner user not exist");
            
            if (noteForAddDto.OwnerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var ticketFromRepo = await _ticketRepo.GetTicket(ticketId);
            if (ticketFromRepo == null)
                return BadRequest("Posz vagy válasz nem elérhető");

            var noteToCreate = _mapper.Map<Note>(noteForAddDto);

            _ticketRepo.Add(noteToCreate);
            noteToCreate.Owner = ownerFromRepo;
            noteToCreate.Ticket = ticketFromRepo;

            if (await _ticketRepo.SaveAll())
            {
                var noteToReturn = _mapper.Map<NoteForDetailedDto>(noteToCreate);
                return Ok(noteToReturn);
            }

            throw new System.Exception("Failed to save note");
        }

        [HttpPost("{ticketId}/AssignToGroup/{groupId}")]
        public async Task<IActionResult> AssignToGroup(int ticketId, int groupId)
        {
            var ticketFromRepo = await _ticketRepo.GetTicket(ticketId);

            if (ticketFromRepo == null)
                return BadRequest("Ticket not exist");
            
            if (groupId == 0)
                ticketFromRepo.AssignmentGroupId = null;
            else
                ticketFromRepo.AssignmentGroupId = groupId;

            if (await _ticketRepo.SaveAll())
                return Ok("Assignment group updated");

            throw new System.Exception("Failed to save");
        }

        [HttpPost("{ticketId}/AssignToUser/{userId}")]
        public async Task<IActionResult> AssignToUser(int ticketId, int userId)
        {
            var ticketFromRepo = await _ticketRepo.GetTicket(ticketId);

            if (ticketFromRepo == null)
                return BadRequest("Ticket not exist");

            var userFromRepo = await _appRepo.GetUser(userId);

            if (userFromRepo == null)
                return BadRequest("User not exist");

            foreach (var group in userFromRepo.Groups)
            {
                if (group.UserGroupId == ticketFromRepo.AssignmentGroupId)
                {
                    if (userId == 0)
                        ticketFromRepo.AssignedToId = null;
                    else
                        ticketFromRepo.AssignedToId = userId;

                    if (await _ticketRepo.SaveAll())
                        return Ok($"Assigned to {userFromRepo.Username}");
                    else
                        throw new System.Exception("Failed to save");
                }
            }
            return BadRequest($"Incorrect group {ticketFromRepo.AssignmentGroup.Name} for user {userFromRepo.Username}");
        }

        [HttpPost("{ticketId}/UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int ticketId, TicketForUpdateDto ticketForUpdateDto)
        {
            var ticketFromRepo = await _ticketRepo.GetTicket(ticketId);

            if (ticketFromRepo == null)
                return BadRequest("Ticket not exist");

            ticketFromRepo.Priority = ticketForUpdateDto.Priority;
            ticketFromRepo.StatusId = ticketForUpdateDto.StatusId;
            ticketFromRepo.CategoryId = ticketForUpdateDto.CategoryId;
            ticketFromRepo.ResolvedAt = ticketForUpdateDto.ResolvedAt ?? ticketFromRepo.ResolvedAt;
            ticketFromRepo.ClosedAt = ticketForUpdateDto.ClosedAt ?? ticketFromRepo.ClosedAt;
            ticketFromRepo.InvoicedAt = ticketForUpdateDto.InvoicedAt ?? ticketFromRepo.InvoicedAt;
            ticketFromRepo.SubStatusId = ticketForUpdateDto.SubStatusId ?? ticketFromRepo.SubStatusId;

            if (await _ticketRepo.SaveAll())
                return Ok($"Ticket updated");
            else
                throw new System.Exception("Failed to save");
        }

        [HttpPost("{ticketId}/ResolveTicket")]
        public async Task<IActionResult> ResolveTicket(int ticketId, ResolutionForAddDto resolutionForAddDto)
        {
            var ownerFromRepo = await _appRepo.GetUser(resolutionForAddDto.OwnerId);

            if (ownerFromRepo == null)
                return BadRequest("Owner user not exist");
            
            if (resolutionForAddDto.OwnerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var ticketFromRepo = await _ticketRepo.GetTicket(ticketId);

            if (ticketFromRepo == null)
                return BadRequest("Ticket not exist");

            var resolutionToCreate = _mapper.Map<Resolution>(resolutionForAddDto);

            ticketFromRepo.Resolutions.Add(resolutionToCreate);

            ticketFromRepo.ResolvedAt = resolutionForAddDto.CreatedAt;

            if (await _ticketRepo.SaveAll())
                return Ok("Resolution saved");

            throw new System.Exception("Failed to save resolution");
        }

        [HttpPost("{ticketId}/SurveyTicket")]
        public async Task<IActionResult> SurveyTicket(int ticketId, SurveyForAddDto surveyForAddDto)
        {
            var ticketFromRepo = await _ticketRepo.GetTicket(ticketId);

            if (ticketFromRepo == null)
                return BadRequest("Ticket not exist");

            var survayToCreate = _mapper.Map<Survey>(surveyForAddDto);
            ticketFromRepo.Surveys.Add(survayToCreate);

            if (await _ticketRepo.ResponseIsRefusal(surveyForAddDto.ResponseId))
            {
                ticketFromRepo.StatusId = 1; //return to assigned status
            }

            if (await _ticketRepo.SaveAll())
                return Ok("Survey saved");

            throw new System.Exception("Failed to save survey");
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            Ticket ticket = await _ticketRepo.GetTicket(id);

            var ticketToReturn = _mapper.Map<TicketForDetailedDto>(ticket);

            return Ok(ticketToReturn);
        }

        [HttpGet("GetTicketThread")]
        public async Task<IActionResult> GetTicketThread(TicketParams ticketParams)
        {
            var ticketsToReturn = await _ticketRepo.GetTicketThread(ticketParams);
            return Ok(ticketParams);
        }

        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var usersFromRepo = await _ticketRepo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForSelectDto>>(usersFromRepo);
            return Ok(usersToReturn);
        }
    }
}