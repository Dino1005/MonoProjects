using System;

namespace Projects.Model.Common
{
    public interface IAccount
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
