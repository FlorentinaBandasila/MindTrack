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
    public class TaskCategory
    {
        [Key] [Required] [NotNull]
        public Guid Category_id { get; set; }
        public string Category_name { get; set; }

        public UserTask UserTask { get; set; }
    }
}
