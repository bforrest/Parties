using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace PartiesTests
{
    interface IPersonName
    {
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        [StringLength(35, ErrorMessage = "First Name must be between 2 and 35 characters in length.", MinimumLength = 2)]
        [RegularExpression(Constants.NameRegex, ErrorMessage = Constants.NameCharacters)]
        string First { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(35, ErrorMessage = "Last Name must be between 2 and 35 characters in length.", MinimumLength = 2)]
        [RegularExpression(Constants.NameRegex, ErrorMessage = Constants.NameCharacters)]
        string Last { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(35, ErrorMessage = "Middle Name must be between 2 and 35 characters in length.", MinimumLength = 2)]
        [RegularExpression(Constants.NameRegex, ErrorMessage = Constants.NameCharacters)]
        string Middle { get; set; }
    }
}