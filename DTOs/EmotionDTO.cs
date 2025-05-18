using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class EmotionDTO
    {
        public Guid Emotion_id { get; set; }

        public Guid User_id { get; set; }

        public Guid Mood_id { get; set; }
        public string Reflection { get; set; }
        public DateTime Date { get; set; }
    }
}
