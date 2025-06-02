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
    public class MoodSelectionService : IMoodSelectionService
    {
        private readonly IMoodSelectionRepository _moodSelectionRepository;
        private readonly IMapper _mapper;

        public MoodSelectionService(IMoodSelectionRepository moodSelectionRepository, IMapper mapper)
        {
            _moodSelectionRepository = moodSelectionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MoodSelection>> GetAllMoodSelections()
        {
            var moodSelections = await _moodSelectionRepository.GetAllMoodSelections();
            return _mapper.Map<IEnumerable<MoodSelection>>(moodSelections);
        }

        public async Task<MoodSelectionDTO> GetMoodSelectionById(Guid id)
        {
            var moodSelection = await _moodSelectionRepository.GetMoodSelectionById(id);
            return _mapper.Map<MoodSelectionDTO>(moodSelection);
        }
        public async Task CreateMoodSelection(MoodSelection moodSelection)
        {
            await _moodSelectionRepository.CreateMoodSelection(moodSelection);
        }

        public async Task DeleteMoodSelection(Guid id)
        {
            await _moodSelectionRepository.DeleteMoodSelection(id);
        }
    }
}
