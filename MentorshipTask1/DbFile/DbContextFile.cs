using MentorshipTask1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MentorshipTask1.DbFile
{
    public class DbContextFile : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

    }
}