using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityServerClient.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string SlackUserId { get; set; }
    }
}
