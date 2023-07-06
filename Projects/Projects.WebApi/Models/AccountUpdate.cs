using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mono.WebApi.Models
{
    public class AccountUpdate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AccountUpdate(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}