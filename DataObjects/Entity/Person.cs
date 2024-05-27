using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name cannot be empty!")]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name cannot be empty!")]
        [StringLength(40)]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of birth field cannot be empty!")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone number cannot be empty!")]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(40)]
        public string Email { get; set; }

        [StringLength(60)]
        public string Address { get; set; }
    }
}