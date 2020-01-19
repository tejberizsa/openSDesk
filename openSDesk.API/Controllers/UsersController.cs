using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using openSDesk.API.Data;
using openSDesk.API.Dtos;
using openSDesk.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace openSDesk.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }
        public UsersController(IApplicationRepository repo, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _repo = repo;
            Configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            PageParams userParams = new PageParams();
            var users = await _repo.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            // if(String.IsNullOrEmpty(userToReturn.PhotoUrl))
            // {
            //     userToReturn.PhotoUrl = $"{Configuration.GetSection("AppSettings:Domain").Value}/api/users/{id}/photos/link/default";
            // }

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();
            
            throw new Exception($"Updating user {id} failed on save");
        }
    }
}