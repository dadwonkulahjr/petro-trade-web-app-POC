using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        //[EmailDomainValidatorAttribute(emailDomain: "iamtuse.com", ErrorMessage = "Email domain must be iamtuse.com for successful login!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
