using MindTrack.Models.DTOs;
using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IMoodSelectionService
    {
        Task<IEnumerable<MoodSelection>> GetAllMoodSelections();
        Task<MoodSelectionDTO> GetMoodSelectionById(Guid id);
        Task CreateMoodSelection(MoodSelection moodSelection);
    }
}
