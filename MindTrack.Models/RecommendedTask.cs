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
    public class RecommendedTask
    {
        [Key]
        [Required]
        [NotNull]
        public Guid Recommended_Task_Id { get; set; }

        public string Mood { get; set; }
       
        public string Title { get; set; }
        public string Priority { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Created_date { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }

        
    }
}
