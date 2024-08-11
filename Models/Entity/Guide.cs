
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dotnetids.Models.Entity
{


    public class Guide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [MinLength(5), MaxLength(20)]
        public required string Password { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public required string Photo { get; set; }

        [MaxLength(20)]
        public required string Profession { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();


    }

}