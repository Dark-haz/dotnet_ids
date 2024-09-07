using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models.DTO
{
    public class EventUpdateDTO
    {
        [Required]
        [MaxLength(20)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public required string Description { get; set; }

        [Required]
        [MaxLength(25)]
        public required string Category { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Destination { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public float Cost { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Status { get; set; }
    }
}