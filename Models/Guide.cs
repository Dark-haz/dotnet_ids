
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dotnetids.Models
{


    public class Guide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [MinLength(5), MaxLength(20)]
        public string Password { get; set; }

        [Required]
        public DateOnly JoiningDate { get; set; }

        public string Photo { get; set; }

        [MaxLength(20)]
        public string Profession { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();


    }

}