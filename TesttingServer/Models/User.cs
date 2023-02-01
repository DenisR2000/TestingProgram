using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace TesttingServer.Models
{
    public class User : IdentityUser
    {
        public int UserRoleType { get; set; }
    }
}
