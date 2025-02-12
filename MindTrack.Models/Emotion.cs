using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models
{
    public class Emotion
    {
        [Key] [Required] [NotNull]
        public Guid Emotion_id { get; set; }

        [ForeignKey("User_id")] [Required] [NotNull]
        public Guid User_id { get; set; }

        [ForeignKey("Mood_id")] [Required] [NotNull]
        public Guid Mood_id { get; set; }
        public string Reflection { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
        public MoodSelection Mood_selection { get; set; }
    }
}
