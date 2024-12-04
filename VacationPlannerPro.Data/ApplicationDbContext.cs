using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VacationPlannerPro.Data.Entities;

namespace VacationPlannerPro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Leader> Leaders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Project -> Team (One-to-Many)
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Teams)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Leader -> Team (One-to-Many)
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Leader)
                .WithMany(l => l.Teams)
                .HasForeignKey(t => t.LeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Worker -> Vacation (One-to-Many)
            modelBuilder.Entity<Vacation>()
                .HasOne(v => v.Worker)
                .WithMany(w => w.Vacations)
                .HasForeignKey(v => v.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Worker -> Team (Many-to-Many)
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Workers)
                .WithMany(w => w.Teams);
        }
    }
}
