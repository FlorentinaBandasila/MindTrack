using MindTrack.Models.DTOs;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IEmotionService
    {
        Task<IEnumerable<Emotion>> GetAllEmotions();
        Task<EmotionDTO> GetEmotionById(Guid id);
        Task CreateEmotion(AddEmotionDTO emotionDTO);

        Task<List<MoodCountDTO>> GetUserEmotionsGroupedByMood(Guid userId, int year, int month);
        Task<List<MoodDTO>> GetMoodByDay(Guid userId, int year, int month);
        Task DeleteEmotion(Guid id);
        Task<Emotion?> GetEmotionByUserIdAndDate(Guid userId, DateTime date);
    }
}
