using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MindTrack.Models;
using MindTrack.Models.Data;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MindTrackContext _mindTrackContext;

        public UserRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _mindTrackContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _mindTrackContext.Users.FirstOrDefaultAsync(r => r.Username == username);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _mindTrackContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetUserById(Guid id)
        {
            return await _mindTrackContext.Users.FirstOrDefaultAsync(r => r.User_id == id);
        }

        public async Task CreateUser(User user)
        {
            await _mindTrackContext.Users.AddAsync(user);
            await _mindTrackContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByUsernameOrEmail(string username, string email)
        {
            return await _mindTrackContext.Users
                .FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
        }


        public async Task DeleteUser(Guid id)
        {
            var user = await _mindTrackContext.Users.FindAsync(id);

            if (user != null) _mindTrackContext.Users.Remove(user);

            await _mindTrackContext.SaveChangesAsync();

        }

       
    }
}
