using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models
{
    public class MoodSelection
    {
        [Key] [Required] [NotNull]
        public Guid Mood_id {  get; set; }
        public string Mood {  get; set; }


        public ICollection<Emotion> Emotions { get; set; }
    }
}
