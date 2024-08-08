using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dotnetids.Models
{
    public class Event
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(25)]
        public string Category { get; set; }

        [Required]
        [MaxLength(100)]
        public string Destination { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public float Cost { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        public ICollection<Guide> Guides { get; set; } = new List<Guide>();

        public ICollection<Member> Members { get; set; } = new List<Member>();

    }
}