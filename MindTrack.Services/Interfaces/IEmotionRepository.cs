using MindTrack.Models;
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
    }
}
