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
    public class Answer
    {
        [Key] [Required] [NotNull]
        public Guid Answer_id { get; set; }

        [ForeignKey("Question_id")] [Required] [NotNull]
        public Guid Question_id { get; set; }
        public string Answer_name {  get; set; }
        public int Points {  get; set; }
        public DateTime Created_time { get; set; }

        public Question Question { get; set; }
    }
}
