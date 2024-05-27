using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Price field cannot be empty!")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "The price cannot be zero or a negative number!")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Desciption cannot be empty!")]
        [StringLength(200)]
        public string Description { get; set; }

        [Display(Name = "Buy Date")]
        public DateTime BuyDate { get; set; }

        [Display(Name = "Expire Date")]
        public DateTime ExpireDate { get; set; }

        [Display(Name = "Person")]
        public int PersonId { get; set; }
        
        [ForeignKey("PersonId")]
        public Person? Person { get; set; }
    }
}
