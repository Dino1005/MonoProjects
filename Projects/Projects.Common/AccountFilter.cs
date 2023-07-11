using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Common
{
    public class AccountFilter
    {
        public string FirstNameQuery { get; set; }
        public string LastNameQuery { get; set; }

        public AccountFilter(string firstNameQuery, string lastNameQuery)
        {
            FirstNameQuery = firstNameQuery;
            LastNameQuery = lastNameQuery;
        }
    }
}
