using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models.DTO
{
    public class MemberCreateDTO
    {
        [Required]
        [MaxLength(20)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(5)]
        public required string Password { get; set; }

        [Required]
        public DateOnly BirthDay { get; set; }

        [Required]
        [MaxLength(10)]
        public required string Gender { get; set; }

        public DateTime JoiningDate { get; } = DateTime.Now;

        [Required]
        [Phone]
        public required string MobileNumber { get; set; }

        [Phone]
        public required string EmergencyNumber { get; set; }

        public required string Photo { get; set; }

        [MaxLength(20)]
        public required string Profession { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Nationality { get; set; }
    }
}