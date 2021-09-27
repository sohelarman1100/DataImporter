using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataImporter.Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public IList<Group> Groups { get; set; }
    }
}
