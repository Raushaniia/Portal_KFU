using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PortalKFU.Domain.Entities;

namespace PortalKFU
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("EFDbContext") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
        public DbSet<Event> Events { get; set; }
    }
}
