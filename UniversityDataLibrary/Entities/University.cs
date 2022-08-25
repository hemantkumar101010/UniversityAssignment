using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataLibrary.Entities
{
    public class University
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public int Id { get; set; }

        [Required]
        
        [StringLength(maximumLength:50, ErrorMessage = "Limit exceeded!")]
        [Column(TypeName = "Varchar(50)")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Limit exceeded!")]
        [Column(TypeName = "Varchar(50)")]
        public string Location { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Limit exceeded!")]
        [Column(TypeName = "Varchar(50)")]
        public string AffiliatedCollege { get; set; }


        [Required]
        public int EstablishedYear{ get; set; }
    }
}
