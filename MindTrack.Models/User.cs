using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models
{
    public class User
    {
        [Key] [Required] [NotNull]
        public Guid User_id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Full_name { get; set; }
        public DateTime Created { get; set; }
        public string Avatar {  get; set; }



        public ICollection<Emotion> Emotions { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
        public ICollection<QuizResults> QuizResults { get; set; }
    }
}
