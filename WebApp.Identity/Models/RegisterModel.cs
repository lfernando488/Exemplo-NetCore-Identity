﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Identity.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
