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
    public class UserTask
    {
        [Key] [Required] [NotNull]
        public Guid Task_id { get; set; }

        [ForeignKey("User_id")] [Required] [NotNull]
        public Guid User_id { get; set; }

        [ForeignKey("Category_id")] [Required] [NotNull]
        public Guid Category_id { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Created_date { get; set; }
        public string Details { get;set; }
        public string Status { get; set; }

        public User User { get; set; }
        public ICollection<TaskCategory> Task_categories { get; set; }
    }
}
