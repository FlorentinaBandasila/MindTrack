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
        Task CreateEmotion(EmotionDTO emotionDTO);
    }
}
