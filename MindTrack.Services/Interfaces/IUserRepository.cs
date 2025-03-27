using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByUsername(string username);
        Task CreateUser(User user);
        Task DeleteUser(Guid id);
        Task UpdateUser(User user);
    }
}
