using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.DAL.Data
{
    public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();

            optionsBuilder.UseSqlServer(
            "Server=CURSELFS\\SQLEXPRESS;Database=StoreSystemDb;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True");

            return new DBContext(optionsBuilder.Options);
        }
    }
}
