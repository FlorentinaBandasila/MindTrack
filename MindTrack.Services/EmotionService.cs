using AutoMapper;
using MindTrack.Models.DTOs;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Services.Repositories;

namespace MindTrack.Services
{
    public class EmotionService : IEmotionService
    {
        private readonly IEmotionRepository _emotionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMoodSelectionRepository _moodSelectionRepository;
        private readonly IMapper _mapper;

        public EmotionService(IEmotionRepository emotionRepository, IMapper mapper, IUserRepository userRepository, IMoodSelectionRepository moodSelectionRepository)
        {
            _emotionRepository = emotionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _moodSelectionRepository = moodSelectionRepository;
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

        public async Task<List<JournalDTO>> GetMoodByUser(Guid userId)
        {
            return await _emotionRepository.GetMoodByUser(userId);
        }

        public async Task CreateEmotion(AddEmotionDTO emotionDTO)
        {
            var user = await _userRepository.GetUserById(emotionDTO.User_id);
            var mood = await _moodSelectionRepository.GetMoodSelectionById(emotionDTO.Mood_id);

            
            var existingEmotion = await _emotionRepository
                .GetEmotionByUserIdAndDate(user.User_id, emotionDTO.Date.Date); 

            if (existingEmotion != null)
            {
                await _emotionRepository.DeleteEmotion(existingEmotion.Emotion_id);
            }

            var emotionModel = new Emotion
            {
                Emotion_id = Guid.NewGuid(),
                User_id = user.User_id,
                Mood_id = mood.Mood_id,
                Reflection = emotionDTO.Reflection,
                Date = emotionDTO.Date
            };

            await _emotionRepository.CreateEmotion(emotionModel);
        }

        public async Task<Emotion?> GetEmotionByUserIdAndDate(Guid userId, DateTime date)
        {
            return await _emotionRepository.GetEmotionByUserIdAndDate(userId, date.Date);
        }



        public async Task<List<MoodCountDTO>> GetUserEmotionsGroupedByMood(Guid userId, int year, int month)
        {
            return await _emotionRepository.GetUserEmotionsGroupedByMood(userId, year, month);
        }

        public async Task<List<MoodDTO>> GetMoodByDay(Guid userId, int year, int month)
        {
            return await _emotionRepository.GetMoodByDay(userId, year, month);
        }

        public async Task DeleteEmotion(Guid id)
        {
            await _emotionRepository.DeleteEmotion(id);
        }
    }
}
