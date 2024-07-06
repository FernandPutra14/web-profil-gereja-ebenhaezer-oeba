using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PKMGerejaEbenhaezer.Domain.Entity
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        public DateTime LastChanged { get; set; }
    }

    public static class AppUserRoles
    {
        public const string Admin = "Admin";
        public const string SuperAdmin = "SuperAdmin";
    }
}
