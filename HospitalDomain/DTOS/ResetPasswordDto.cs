﻿using System.ComponentModel.DataAnnotations;

namespace HospitalDomain.DTOS
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "password is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is requird")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "dosent match")]
        public string ConfirmPassword { get; set; }

        public string token { get; set; }
        public string email { get; set; }
    }
}