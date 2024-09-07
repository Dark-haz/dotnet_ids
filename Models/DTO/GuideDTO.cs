using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models.DTO
{
    public class GuideDTO
    {
        public int ID { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public DateTime JoiningDate { get; set; }

        public required string Photo { get; set; }

        public required string Profession { get; set; }


    }
}