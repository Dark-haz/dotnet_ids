using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSucess { get; set; } = true ; 
        public List<String> ErrorMessages { get; set; }
        public object ? Result { get; set; } 

    }
}

