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
        [Key]
        [Required]
        public Guid Task_id { get; set; }

        [Required]
        public Guid User_id { get; set; }

        [Required]
        public Guid Category_id { get; set; }

        public Guid? Recommended_Task_Id { get; set; }

        public string Title { get; set; }
        public string Priority { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Created_date { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }

        // ✅ Navigații corect configurate
        [ForeignKey(nameof(User_id))]
        public User User { get; set; }

        [ForeignKey(nameof(Category_id))]
        public TaskCategory TaskCategory { get; set; }

        [ForeignKey(nameof(Recommended_Task_Id))]
        public RecommendedTask RecommendedTask { get; set; }
    }

}