using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class AnswerQuizDTO
    {
        public Guid Answer_id { get; set; }
        public string Answer_name { get; set; }
        public int Points { get; set; }
    }
}
