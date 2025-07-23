using EmployeeMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Emp> employee { get; set; }
        public DbSet<Document> Document { get; set; }

        public DbSet<NewEmp> emps { get; set; }
        public DbSet<Auth> auth { get; set; }

        public DbSet<NewEmployee> newEmp { get; set; }
        public DbSet<NewManager> newManagers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewEmployee>()
                .HasOne(b => b.mans)
                .WithMany(p => p.employees)
                .HasForeignKey(b => b.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
