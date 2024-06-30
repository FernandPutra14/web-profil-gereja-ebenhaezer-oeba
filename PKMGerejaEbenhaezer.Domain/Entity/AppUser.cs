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
    }
}
