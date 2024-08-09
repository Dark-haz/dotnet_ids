using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnetids.Models
{

    public class Member
    {
        [Key]
        public int ID { get; set; }

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

        [Required]
        public DateTime JoiningDate { get; set; }

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

        public ICollection<Event> Events { get; set; } = new List<Event>();

    }

}