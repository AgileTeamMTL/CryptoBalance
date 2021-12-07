//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CryptoBalance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class user
    {
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "*Username is required")]
        public string username { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "*Password is required")]
        public string password { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "*Firstname is required")]
        public string first_name { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "*Lastname is required")]
        public string last_name { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                            ErrorMessage = "Incorrect Format, Example: crypto@gmail.com")]
        [Required(ErrorMessage = "*Email is required")]
        public string email { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "*Phone is required")]
        public string phone { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "*City is required")]
        public string city { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "*Province is required")]
        public string province { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "*Country is required")]
        public string country { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "*Date of Birth is required")]
        [DisplayName("Date of Birth (MM/DD/YYYY)")]
        public System.DateTime date_of_birth { get; set; }
        public System.DateTime creation_date { get; set; }
        public System.DateTime modification_date { get; set; }
    }
}
