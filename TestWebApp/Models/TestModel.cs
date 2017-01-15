using System.Data.Entity;

namespace TestWebApp.Models
{
    public class TestModel : DbContext
    {
        public TestModel() : base("name=TestModelConnection")
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //}
    }
}
