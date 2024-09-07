using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models.DTO
{
    public class AdminCreateDTO
    {
        [Required]
        [MaxLength(20)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public required string LastName { get; set; }

        [Required]
        public DateOnly Birthday { get; set; }

        [Required]
        [MaxLength(10)]
        public required string Gender { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(5)]
        public required string Password { get; set; }
    }
}