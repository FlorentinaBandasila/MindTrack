using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class AddEmotionDTO
    {
        public Guid User_id { get; set; }

        public Guid Mood_id { get; set; }
        public string Reflection { get; set; }
        public DateTime Date { get; set; }
    }
}
