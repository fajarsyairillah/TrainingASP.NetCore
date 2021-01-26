using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OverviewASPNetCore.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        [ForeignKey("University")]
        public int University_Id { get; set; }
        public bool IsAvailable { get; set; }
        public virtual University University { get; set; }
        public string Name { get; internal set; }
    }
}
