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
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Phone]
        public string EmergencyNumber { get; set; }

        public string Photo { get; set; }

        [MaxLength(20)]
        public string Profession { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nationality { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();

    }

}