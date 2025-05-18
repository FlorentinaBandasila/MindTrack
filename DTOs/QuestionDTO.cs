using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class QuestionDTO
    {
        public Guid Question_id { get; set; }
        public string Title { get; set; }
        public DateTime Created_date { get; set; }
    }
}
