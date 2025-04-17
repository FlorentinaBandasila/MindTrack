using MindTrack.Models.Data;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    }
}
