using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TesttingServer.Models
{
    public class TesttingDbContext :DbContext
    {
        public TesttingDbContext(DbContextOptions<TesttingDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Qestion> Qestions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        { }
    }
}
