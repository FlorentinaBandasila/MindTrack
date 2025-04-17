using AutoMapper;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services
{
    public class EmotionService : IEmotionService
    {
        private readonly IEmotionRepository _emotionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public EmotionService(IEmotionRepository emotionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _emotionRepository = emotionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Emotion>> GetAllEmotions()
        {
            var emotions = await _emotionRepository.GetAllEmotions();
            return _mapper.Map<IEnumerable<Emotion>>(emotions);
        }

        public async Task<EmotionDTO> GetEmotionById(Guid id)
        {
            var emotion = await _emotionRepository.GetEmotionById(id);
            return _mapper.Map<EmotionDTO>(emotion);
        }
        public async Task CreateEmotion(EmotionDTO emotionDTO)
        {
            var user = await _userRepository.GetUserById(emotionDTO.User_id);
            Guid moodId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa4");
            var emotionModel = new Emotion
            {
                Emotion_id = Guid.NewGuid(),
                User_id = user.User_id,
                Mood_id = moodId, //add later

                Reflection = emotionDTO.Reflection,

                Date = DateTime.Now
            };

            await _emotionRepository.CreateEmotion(emotionModel);
        }
    }
}
