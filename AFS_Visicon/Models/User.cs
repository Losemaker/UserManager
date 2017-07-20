using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AFS_Visicon.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "First name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]{1,24}$", ErrorMessage = "Please use only etters, first letter must be capital amd length must be from interval <2,25>.")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]{1,24}$", ErrorMessage = "Please use only letters, first letter must be capital amd length must be from interval <2,25>.")]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Display(Name = "Mobile number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid format of phone number.")]
        [Required(ErrorMessage = "Please enter your phone number in this format 09xxxxxxxx")]
        public string Mobil { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Please enter your date of births")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "E-mail address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Please enter your e-mail address.")]
        public string Email { get; set; }
    }
}