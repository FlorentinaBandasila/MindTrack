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
        Task<User> GetUserById(Guid id);
        Task CreateUser(User user);
        Task DeleteUser(Guid id);
        Task<User?> GetUserByUsernameOrEmail(string username, string email);

        Task<User?> GetUserByEmail(string email);
    }
}
