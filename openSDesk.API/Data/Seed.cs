using System.Collections.Generic;
using openSDesk.API.Models;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace openSDesk.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedEntities()
        {
            // seed initial users
            if (_context.Users.Count() < 1)
            {
                // seed initial admin user
                var userData = System.IO.File.ReadAllText("Data/SeedData/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("Password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    _context.Users.Add(user);
                }
                _context.SaveChanges();
                
                // seed initial user group
                var groupData = System.IO.File.ReadAllText("Data/SeedData/UserGroupData.json");
                var groups = JsonConvert.DeserializeObject<List<UserGroup>>(groupData);
                foreach (var group in groups)
                {
                    _context.UserGroups.Add(group);
                }
                _context.SaveChanges();

                // seed initial user assignment to group
                var assignmentData = System.IO.File.ReadAllText("Data/SeedData/UserGroupAssignmentData.json");
                var assignments = JsonConvert.DeserializeObject<List<UserGroupAssingment>>(assignmentData);
                foreach (var assignment in assignments)
                {
                    _context.UserGroupAssingments.Add(assignment);
                }
                _context.SaveChanges();
            }

            // seed initial resolution codes
            if (_context.ResolutionCodes.Count() < 1)
            {
                var resolutionData = System.IO.File.ReadAllText("Data/SeedData/ResolutionCodeData.json");
                var resolutions = JsonConvert.DeserializeObject<List<ResolutionCode>>(resolutionData);
                foreach (var resolution in resolutions)
                {
                    _context.ResolutionCodes.Add(resolution);
                }
                _context.SaveChanges();
            }

            // seed initial ticket statuses
            if (_context.Statuses.Count() < 1)
            {
                var statusData = System.IO.File.ReadAllText("Data/SeedData/StatusData.json");
                var statuses = JsonConvert.DeserializeObject<List<Status>>(statusData);
                foreach (var status in statuses)
                {
                    _context.Statuses.Add(status);
                }
                _context.SaveChanges();
            }

            // seed initial survey responses
            if (_context.SurvayResponses.Count() < 1)
            {
                var surveyData = System.IO.File.ReadAllText("Data/SeedData/SurveyResponseData.json");
                var surveys = JsonConvert.DeserializeObject<List<SurveyResponse>>(surveyData);
                foreach (var survey in surveys)
                {
                    _context.SurvayResponses.Add(survey);
                }
                _context.SaveChanges();
            }

            // seed initial ticket sources
            if (_context.Sources.Count() < 1)
            {
                var sourceData = System.IO.File.ReadAllText("Data/SeedData/SourceData.json");
                var sources = JsonConvert.DeserializeObject<List<Source>>(sourceData);
                foreach (var source in sources)
                {
                    _context.Sources.Add(source);
                }
                _context.SaveChanges();
            }

            // seed initial categories
            if (_context.Categories.Count() < 1)
            {
                var sourceData = System.IO.File.ReadAllText("Data/SeedData/CategoryData.json");
                var sources = JsonConvert.DeserializeObject<List<Category>>(sourceData);
                foreach (var source in sources)
                {
                    _context.Categories.Add(source);
                }
                _context.SaveChanges();
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }
    }
}