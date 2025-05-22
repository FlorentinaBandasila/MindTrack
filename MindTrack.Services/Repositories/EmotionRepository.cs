using MindTrack.Models.Data;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MindTrack.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MindTrack.Services.Repositories
{
    public class EmotionRepository : IEmotionRepository
    {
        private readonly MindTrackContext _mindTrackContext;
        public EmotionRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<Emotion>> GetAllEmotions()
        {
            return await _mindTrackContext.Emotions.ToListAsync();
        }

        public async Task<Emotion> GetEmotionById(Guid id)
        {
            return await _mindTrackContext.Emotions.FirstOrDefaultAsync(u => u.Emotion_id == id);
        }

        public async Task CreateEmotion(Emotion emotion)
        {
            await _mindTrackContext.Emotions.AddAsync(emotion);
            await _mindTrackContext.SaveChangesAsync();
        }

        public async Task<List<MoodCountDTO>> GetUserEmotionsGroupedByMood(Guid userId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var result = await _mindTrackContext.Emotions
                .Where(e => e.User_id == userId && e.Date >= startDate && e.Date < endDate)
                .Include(e => e.Mood_selection)
                .GroupBy(e => e.Mood_selection.Mood)
                .Select(g => new MoodCountDTO
                {
                    MoodName = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return result;
        }


        public async Task<List<MoodDTO>> GetMoodByDay(Guid userId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var result = await _mindTrackContext.Emotions
                .Where(e => e.User_id == userId && e.Date >= startDate && e.Date < endDate)
                .Include(e => e.Mood_selection)
                .Select(g => new MoodDTO
                {
                    Mood_Name = g.Mood_selection.Mood,
                    Date = g.Date,
                })
                .ToListAsync();
            return result;
        }
    }
}
