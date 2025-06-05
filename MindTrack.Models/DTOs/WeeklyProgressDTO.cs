using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class WeeklyProgressDTO
    {
        public int Percentage { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
    }

}
