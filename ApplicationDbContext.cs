using MyDiary.Models.Configurations;
using MyDiary.Models.Domains;
using MyDiary.Properties;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MyDiary
{
    public class ApplicationDbContext : DbContext
    {
        private static string _connectionString = $@"
            Server={Settings.Default.ServerAddress}\{Settings.Default.ServerName};
            initial catalog={Settings.Default.DbName};
            user id={Settings.Default.UserName};
            password={Settings.Default.Password};";

        public ApplicationDbContext()
            : base(_connectionString)
         //: base("name=ApplicationDbContext")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }


    }


}