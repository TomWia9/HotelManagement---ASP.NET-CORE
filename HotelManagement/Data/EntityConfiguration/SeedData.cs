using HotelManagement.Models;
using HotelManagement.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.EntityConfiguration
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DatabaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DatabaseContext>>());

            if (context.Administrators.Any())
            {
                return;   // DB has been seeded
            }

            context.Administrators.Add(
                new Admin
                {
                    Email = "admin@admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Password = Hash.GetHash("admin")
                }
            );
            context.SaveChanges();
        }
    }
}
