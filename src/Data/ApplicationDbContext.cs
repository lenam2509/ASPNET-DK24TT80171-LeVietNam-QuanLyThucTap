using InternshipManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<InternshipRegistration> InternshipRegistrations { get; set; }
    }
}
