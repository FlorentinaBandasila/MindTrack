using MindTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IMoodSelectionRepository
    {
        Task<IEnumerable<MoodSelection>> GetAllMoodSelections();
        Task<MoodSelection> GetMoodSelectionById(Guid id);
        Task CreateMoodSelection(MoodSelection moodSelection);
        Task DeleteMoodSelection(Guid id);
    }
}
