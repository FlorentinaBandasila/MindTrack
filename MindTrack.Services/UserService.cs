using AutoMapper;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MindTrack.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MindTrack.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly MindTrackContext _mindTrackContext;
        private readonly IMemoryCache _cache;
        private readonly PasswordHasher<string> _passwordHasher = new();

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, MindTrackContext
            mindTrackContext, IMemoryCache cache)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _mindTrackContext = mindTrackContext;
            _cache = cache;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<User> CreateUser(UserRegisterDTO request)
        {
            var user = new User {
                User_id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                Full_name = request.Full_name,
                Created = DateTime.Now,
                Avatar = string.Empty,
            };

            var hashedPassword = new PasswordHasher<User>()
                 .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.Password = hashedPassword;
            
            await _userRepository.CreateUser(user);
            return user;
        }

        public async Task<string?> Login(LoginDTO request)
        {
            var user = await _mindTrackContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Identifier || u.Username == request.Identifier);

            if (user is null) return null;

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password) ==
                PasswordVerificationResult.Failed)
                return null;

            return CreateToken(user);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _mindTrackContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;


            var token = CreateToken(user, expiresInMinutes: 15);

            var resetLink = $"https://localhost:60126/api/User/reset-password?token={token}";
            var subject = "Reset Your Password";
            var body = $@"
    <html>
        <body>
            <p>Click the link below to reset your password:</p>
            <p><a href=""{resetLink}"" target=""_blank"">{resetLink}</a></p>
        </body>
    </html>";


            var emailSender = new EmailSender(_configuration);
            await emailSender.SendEmailAsync(email, subject, body);

            return true;
        }

        public async Task<bool> ForgotPasswordWithCodeAsync(string email)
        {
            var user = await _mindTrackContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            var code = new Random().Next(100000, 999999).ToString();

           
            _cache.Set($"pwd-reset:{email}", code, TimeSpan.FromMinutes(10));

            var body = $@"
        <html>
            <body>
                <p>Your password reset code is: <strong>{code}</strong></p>
            </body>
        </html>";

            var emailSender = new EmailSender(_configuration);
            await emailSender.SendEmailAsync(email, "Your Reset Code", body);

            return true;
        }

        public async Task<bool> ResetPasswordWithCodeAsync(string email, string code, string newPassword)
        {
            if (!_cache.TryGetValue($"pwd-reset:{email}", out string? cachedCode))
                return false;

            if (cachedCode != code)
                return false;

            var user = await _mindTrackContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            string hashedPassword = _passwordHasher.HashPassword(null, newPassword);

            user.Password = hashedPassword;

            await _mindTrackContext.SaveChangesAsync();

           
            _cache.Remove($"pwd-reset:{email}");

            return true;
        }


        private string CreateToken(User user, int expiresInMinutes = 60 * 24)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.User_id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiresInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task DeleteUser(Guid id)
        {
            await _userRepository.DeleteUser(id);
        }

        
    }
}
