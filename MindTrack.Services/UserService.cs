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

namespace MindTrack.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly MindTrackContext _mindTrackContext;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, MindTrackContext
            mindTrackContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _mindTrackContext = mindTrackContext;
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

        public async Task<User> CreateUser(UserDTO request)
        {
            var user = new User {
                User_id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                Full_name = request.Full_name,
                Created = DateTime.Now
            };

            var hashedPassword = new PasswordHasher<User>()
                 .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.Password = hashedPassword;
            
            await _userRepository.CreateUser(user);
            return user;
        }

        public async Task<string?> Login(UserDTO request)
        {
            var user = await _mindTrackContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null) return null;
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password) ==
                PasswordVerificationResult.Failed) return null;

            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.User_id.ToString())
             };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task DeleteUser(Guid id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<User> UpdateUser(string username, JsonPatchDocument<UserDTO> patchDoc)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new Exception($"User not found with username: {username}");
            }

            var userDto = _mapper.Map<UserDTO>(user);

            patchDoc.ApplyTo(userDto);
            _mapper.Map(userDto, user);

            await _userRepository.UpdateUser(user);
            return user;
        }
    }
}
