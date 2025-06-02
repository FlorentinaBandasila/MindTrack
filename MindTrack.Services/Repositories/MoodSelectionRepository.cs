using MindTrack.Models.Data;
using MindTrack.Models;
using MindTrack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MindTrack.Services.Repositories
{
    public class MoodSelectionRepository : IMoodSelectionRepository
    {
        private readonly MindTrackContext _mindTrackContext;
        public MoodSelectionRepository(MindTrackContext mindTrackContext)
        {
            _mindTrackContext = mindTrackContext;
        }

        public async Task<IEnumerable<MoodSelection>> GetAllMoodSelections()
        {
            return await _mindTrackContext.MoodSelections.ToListAsync();
        }

        public async Task<MoodSelection> GetMoodSelectionById(Guid id)
        {
            return await _mindTrackContext.MoodSelections.FirstOrDefaultAsync(a => a.Mood_id == id);
        }

        public async Task CreateMoodSelection(MoodSelection moodSelection)
        {
            await _mindTrackContext.MoodSelections.AddAsync(moodSelection);
            await _mindTrackContext.SaveChangesAsync();
        }

        public async Task DeleteMoodSelection(Guid id)
        {
            var mood = await _mindTrackContext.MoodSelections.FindAsync(id);

            if (mood != null) _mindTrackContext.MoodSelections.Remove(mood);

            await _mindTrackContext.SaveChangesAsync();

        }
    }
}
