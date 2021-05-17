using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;
using Persistence;
using System;

namespace Application.UnitTest.Common
{
    public class ContextFactory
    {
        public static ApplicationDbContext Create(ICurrentUserService currentUser)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options, currentUser);

            context.Database.EnsureCreated();
             
            //Add Sample Data Here

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
