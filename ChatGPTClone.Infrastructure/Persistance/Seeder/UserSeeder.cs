using ChatGPTClone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTClone.Infrastructure.Persistance.Seeder
{
    public class UserSeeder : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var initialUserId = Guid.Parse("2798212b-3e5d-4556-8629-a64eb70da4a8");

            var initialUser = new AppUser
            {
                Id = initialUserId,
                UserName="umut",
                NormalizedUserName="UMUT",
                Email="umut@gmail.com",
                NormalizedEmail="UMUT@GMAIL.COM",
                EmailConfirmed=true,
                CreatedByUserId=initialUserId.ToString(),
                CreatedOn=new DateTimeOffset(new DateTime(2024,08,28)),
                ConcurrencyStamp=Guid.NewGuid().ToString(),
                FirstName="Umut",
                LastName="Atraş",
                SecurityStamp=Guid.NewGuid().ToString(),
                LockoutEnabled=false,
                AccessFailedCount=0,
                    
            };
            initialUser.PasswordHash = new PasswordHasher<AppUser>().HashPassword(initialUser,"123umut123");

            builder.HasData(initialUser);
        }
    }
}
