using MindTrack.Models;
using MindTrack.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IEmotionRepository
    {
        Task<IEnumerable<Emotion>> GetAllEmotions();
        Task<Emotion> GetEmotionById(Guid id);
        Task CreateEmotion(Emotion emotion);
        Task<List<MoodCountDTO>> GetUserEmotionsGroupedByMood(Guid userId, int year, int month);
        Task<List<MoodDTO>> GetMoodByDay(Guid userId, int year, int month);

        Task DeleteEmotion(Guid id);
        Task<Emotion?> GetEmotionByUserIdAndDate(Guid userId, DateTime date);

    }
}
