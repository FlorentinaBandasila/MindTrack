using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models
{
    public class QuizResults
    {
        [Key]
        [Required]
        [NotNull]
        public Guid QuizResult_id { get; set; }

        [ForeignKey("User_id")]
        [Required]
        [NotNull]
        public Guid User_id { get; set; }
        public string Title { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }

}
