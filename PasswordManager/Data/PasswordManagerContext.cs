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

        public DbSet<PasswordManager.Models.Record> Record { get; set; }
    }
}
