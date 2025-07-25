﻿using MindTrack.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Services.Interfaces
{
    public interface IQuizResultsService
    {
        Task<QuizResultsDTO> GetQuizResultsByUser(Guid id);
    }
}
