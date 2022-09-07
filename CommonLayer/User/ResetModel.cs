using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class ResetModel
    {
        [Required]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+=-])[a-zA-Z0-9!@#$%^&*()_+=-]{8,}$", ErrorMessage = "Password is not valid 1.Min 8 Character , 2.Atleast 1 special character[@,#,$],3.Atleast 1 digit[0-9],4.Atleast 1 Capital Letter[A-Z] ")]
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}