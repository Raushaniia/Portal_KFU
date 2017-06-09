using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PortalKFU.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public ApplicationUser()
        {
        }
    }
}
