using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models.Request
{
    public class LoginResponseDTO
    {
        public  Object ? User { get; set; } 
        public required String Token { get; set; } 
    }
}