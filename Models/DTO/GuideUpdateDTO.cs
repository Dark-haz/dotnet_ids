using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models.DTO
{
    public class GuideUpdateDTO
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

        // [Required]
        // [MinLength(5)]
        // public required string Password { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public required string Photo { get; set; }

        [MaxLength(100)]
        public required string Profession { get; set; }
    }
}