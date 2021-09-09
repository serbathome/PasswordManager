using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Data
{
    public class PasswordManagerContext : DbContext
    {
        public PasswordManagerContext (DbContextOptions<PasswordManagerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add a key on User.LoginName to make it unique
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.LoginName);
        }

        public DbSet<PasswordManager.Models.Record> Record { get; set; }
        public DbSet<PasswordManager.Models.User> User { get; set; }
    }
}
