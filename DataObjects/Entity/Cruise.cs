using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Cruise
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ship's name cannot be empty!")]
        [StringLength(40)]
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date of cruise cannot be empty!")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End date of cruise cannot be empty!")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Star Rating")]
        [Range(0.00, 5.00, ErrorMessage = "Star Rating must be between 0.00 and 5.00!")]
        public double StarRating { get; set; }

        [Display(Name = "Seating")]
        [Range(250, 10000, ErrorMessage = "Seating must be in range from 250 to 10000!")]
        public int Seating { get; set; }

    }
}
