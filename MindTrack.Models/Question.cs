using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models
{
    public class Question
    {
        [Key] [Required] [NotNull]
        public Guid Question_id { get; set; }
        public string Title { get; set; }
        public DateTime Created_date { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
