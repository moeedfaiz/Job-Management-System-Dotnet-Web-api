using Job_Management_System.errormodel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Job_Management_System.models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table configurations
            modelBuilder.Entity<JobPosting>().ToTable("JobPostings", "dbo");
            modelBuilder.Entity<ErrorLog>().ToTable("ErrorLogs", "dbo");
            modelBuilder.Entity<UserLogin>().ToTable("Users", "dbo");


            // JobPostings properties configurations
            modelBuilder.Entity<JobPosting>().HasKey(j => j.Id);
            modelBuilder.Entity<JobPosting>().Property(j => j.Salary).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<JobPosting>().Property(j => j.JobTitle).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<JobPosting>().Property(j => j.Description).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<JobPosting>().Property(j => j.CompanyName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<JobPosting>().Property(j => j.Location).IsRequired().HasMaxLength(100);

            // UserLogin properties configurations
            modelBuilder.Entity<UserLogin>().HasKey(u => u.Id);
            modelBuilder.Entity<UserLogin>().Property(u => u.Username).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<UserLogin>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);


            // UserRegistration properties configurations
         
        }
    }
}
