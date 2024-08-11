using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnetids.Models.Entity;

namespace Solution.dotnet_ids.Models.DTO
{
    public class MemberDTO
    {
        public required int ID { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required DateOnly BirthDay { get; set; }

        public required string Gender { get; set; }

        public required DateTime JoiningDate { get; set; }

        public required string MobileNumber { get; set; }

        public required string EmergencyNumber { get; set; }

        public required string Photo { get; set; }

        public required string Profession { get; set; }

        public required string Nationality { get; set; }


    }
}