using Microsoft.EntityFrameworkCore;
using Project.Data.Models;

namespace Project.Data.Context
{
    public partial class ProjectContext : DbContext
    {
        public static string ConnectionString { get; set; }
        public static string CurrentURL { get; set; }
        public static string SecretKey { get; set; }
        public static string AppURL { get; set; }
        public static string TokenExpireMinute { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            Database.SetCommandTimeout(150000);
        }
        public ProjectContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ruchit\\SQLEXPRESS;Initial Catalog=Testv1;User id=sa;password=sa@123;");
            //optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<InfoCode> InfoCode { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

