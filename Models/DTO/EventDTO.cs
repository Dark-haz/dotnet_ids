using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models.DTO
{
    public class EventDTO
    {
        public required int ID { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Category { get; set; }

        public required string Destination { get; set; }

        public required DateTime DateFrom { get; set; }

        public required DateTime DateTo { get; set; }

        public required float Cost { get; set; }

        public required string Status { get; set; }

        public ICollection<MemberDTO> Members { get; set; } = new List<MemberDTO>();


    }
}