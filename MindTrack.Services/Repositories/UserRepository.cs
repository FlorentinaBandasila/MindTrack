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

        public async Task CreateUser(User user)
        {
            await _mindTrackContext.Users.AddAsync(user);
            await _mindTrackContext.SaveChangesAsync();
        }
    }
}
