using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.dotnet_ids.Models.DTO
{
    public class AdminUpdateDTO
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

        [RegularExpression(@"^.{5,}$", ErrorMessage = "Password must be at least 5 characters long.")]
        public string? Password { get; set; }

    }
}