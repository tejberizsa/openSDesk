using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using openSDesk.API.Data;
using openSDesk.API.Dtos;
using openSDesk.API.Helpers;
using openSDesk.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Microsoft.Extensions.Configuration;

namespace openSDesk.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class UserPhotoController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }
        public UserPhotoController(IApplicationRepository repo, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _repo = repo;
            Configuration = configuration;
        }

        [HttpGet("{id}", Name = "GetUserPhoto")]
        public async Task<IActionResult> GetUserPhoto(int id)
        {
            var photoFromRepo = await _repo.GetUserPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        [ServiceFilter(typeof(LogUserActivity))]
        public async Task<IActionResult> AddPhoto(int userId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var file = photoForCreationDto.File;
            var path = $"{Configuration.GetSection("AppSettings:AppStorage").Value}";
            var filename = $"{userId}_{DateTime.Now.ToString("yyMMddHHmmssff")}";
            var extension = ".jpg";

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    using (Image<Rgba32> image = Image.Load(stream))
                    {
                        image.Mutate(x => x.Resize(600, 0));
                        image.Save(path + filename + extension);
                    }
                }
            }
            
            photoForCreationDto.Url = $"{Configuration.GetSection("AppSettings:Domain").Value}/api/users/{userId}/photos/link/{filename}";
            var photo = _mapper.Map<UserPhoto>(photoForCreationDto);

            if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

            userFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetUserPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Failed to upload");
        }

        [HttpGet("link/{url}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhoto(string url)
        {
            var path = $"{Configuration.GetSection("AppSettings:AppStorage").Value}";
            var extension = ".jpg";
            var image = await Task.Run(() => System.IO.File.OpenRead(path + url + extension));
            return File(image, "image/jpeg");
        }

        [HttpPost("{id}/setMain")]
        [ServiceFilter(typeof(LogUserActivity))]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if(!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetUserPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("Already primary");
            
            var currentMainPhoto = await _repo.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;
            
            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to set primary photo");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if(!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _repo.GetUserPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This is primary");

            var path = $"{Configuration.GetSection("AppSettings:AppStorage").Value}";
            var extension = ".jpg";
            var fileName = photoFromRepo.Url.Split("/").Last();
            await Task.Run(() => System.IO.File.Delete(path + fileName + extension));

            _repo.Delete(photoFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete");
        }
    }
}