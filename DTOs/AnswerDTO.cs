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
    public class AnswerDTO
    {
        public Guid Answer_id { get; set; }

        public Guid Question_id { get; set; }
        public string Answer_name { get; set; }
        public int Points { get; set; }
        public DateTime Created_time { get; set; }
    }
}
