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
        Task<UserDTO> GetUserById(Guid id);
        Task<User> CreateUser(UserRegisterDTO request);
        Task<string> Login(LoginDTO request);
        Task DeleteUser(Guid id);
        Task<bool> ForgotPasswordWithCodeAsync(string email);
        Task<bool> ResetPasswordWithCodeAsync(string email, string code, string newPassword);

    }
}
