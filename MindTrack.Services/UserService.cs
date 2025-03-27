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

namespace MindTrack.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

        public async Task CreateUser(User user)
        {
            await _userRepository.CreateUser(user);
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
