using Microsoft.AspNetCore.JsonPatch;
using MindTrack.Models;
using MindTrack.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserByUsername(string username);
        Task CreateUser(User user);
        Task DeleteUser(Guid id);
        Task<User> UpdateUser(string username, JsonPatchDocument<UserDTO> patchDoc);
    }
}
