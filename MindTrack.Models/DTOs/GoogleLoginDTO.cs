using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindTrack.Models.DTOs
{
    public class GoogleLoginDTO
    {
        public string IdToken { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
    }
}
