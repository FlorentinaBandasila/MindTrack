using Microsoft.AspNetCore.JsonPatch;
using MindTrack.Models;
using MindTrack.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MindTrack.Services.UserService;

namespace MindTrack.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserByUsername(string username);
        Task<UserDTO> GetUserById(Guid id);
        Task<RegisterResult> CreateUser(UserRegisterDTO request);
        Task<string> Login(LoginDTO request);
        Task DeleteUser(Guid id);
        Task<bool> ForgotPasswordWithCode(string email);
        Task<bool> ResetPasswordWithCode(string email, string code, string newPassword);
        Task<bool> ConfirmAccount(string email);
        Task<bool> AccountConfirmationWithCode(string email, string code);
        public string LoginWithGoogle(User user);


    }
}
