using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SWArchitecture.Models;

namespace SWArchitecture
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("Server=(localdb)\\mssqllocaldb;Database=VirtualLab;Trusted_Connection=True;MultipleActiveResultSets=true")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemUser>().HasMany(u => u.Statistics).WithRequired(s=>s.User);
            modelBuilder.Entity<SystemUser>().HasOptional(u => u.Group).WithMany(g => g.Users);

            modelBuilder.Entity<StudyGroup>().HasOptional(g => g.Specialization).WithMany(s=>s.Groups);
            modelBuilder.Entity<Statistic>().HasRequired(s => s.Task).WithMany(s=>s.Statistics);
            modelBuilder.Entity<Task>().HasOptional(t => t.Language).WithMany(tl=>tl.Tasks);

        }

        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<TaskLanguage> TaskLanguages { get; set; }


    }
}
