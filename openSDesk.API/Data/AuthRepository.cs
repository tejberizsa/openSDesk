using System;
using System.Linq;
using System.Threading.Tasks;
using openSDesk.API.Dtos;
using openSDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace openSDesk.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private Random random;
        public AuthRepository(DataContext context)
        {
            _context = context;
            random = new Random();
        }

        private string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            // return new string(Enumerable.Repeat(chars, length)
            //                                         .Select(s => s[random.Next(s.Length)]).ToArray());
            return new string(Enumerable.Range(1, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
        
        public async Task<User> Login(string username, string password)
        {
            var users = await _context.Users.Where(x => x.Username == username.ToLower()).ToListAsync();

            if (users == null)
                return null;

            foreach (var user in users) 
            {
                if(VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return user;
            }

            return null;
        }

        public async Task<User> ConfirmEmail(UserForConfirmDto userToConfirm)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Int32.Parse(userToConfirm.Id));
            if (user == null)
                return null;
            
            if (user.EMailConfirmed)
                return null;

            if (user.ConfirmKey != userToConfirm.ConfirmKey)
                return null;

            user.EMailConfirmed = true;
            await _context.SaveChangesAsync();
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ConfirmKey = randomString(12);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        public async Task<bool> EMailExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.EMail == email))
                return true;

            return false;
        }
    }
}