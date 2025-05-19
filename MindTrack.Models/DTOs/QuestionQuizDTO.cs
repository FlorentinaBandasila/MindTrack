using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class QuestionQuizDTO
    {
        public Guid Question_id { get; set; }
        public string Title { get; set; }
        public List<AnswerQuizDTO> Answers { get; set; }
    }
}
