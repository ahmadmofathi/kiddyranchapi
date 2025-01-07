using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class AppDbContext :IdentityDbContext<User, IdentityRole<string>, string>
    {
       
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CareerInterview>(entity =>
            {
                entity.Property(e => e.CvDescription).IsUnicode(true);
                entity.Property(e => e.Adderss).IsUnicode(true);
            });
            builder.Entity<StudentInterview>(entity =>
            {
                entity.Property(e => e.Address).IsUnicode(true);
            });
            builder.Entity<User>(entity =>
            {
                entity.HasDiscriminator<string>("Discriminator")
                      .HasValue<User>("User");

                
                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(256)
                    .IsUnicode(false);


                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber)
                    .IsUnique();

            });

           
        }

        public DbSet<User> User => Set<User>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<StudentInterview> studentInterviews => Set<StudentInterview>();

        public DbSet<CareerInterview> CareerInterviews => Set<CareerInterview>();
        public DbSet<Call> Calls => Set<Call>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<ImageFile> Images => Set<ImageFile>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Income> Incomes => Set<Income>();

    }
}
