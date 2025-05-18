using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class UserLoginDTO
    {
        public string UserName { get; set; }
        public string PlainPassword { get; set; }
    }
}
