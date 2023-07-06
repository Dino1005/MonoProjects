using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mono.WebApi.Models
{
    public class AccountCreate
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public AccountCreate(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}