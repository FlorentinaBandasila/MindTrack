using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class UserTaskDTO
    {
        public Guid Task_id { get; set; }

        public Guid User_id { get; set; }

        public Guid Category_id { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Created_date { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
    }
}
