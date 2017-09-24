using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Contacts.Models
{
    public class OrganContext : DbContext
    {
        public DbSet<Organ> Organ { get; set; }
    }
}