using Microsoft.EntityFrameworkCore;
using Session03.DataAccessLayer.Models;

namespace Session03.DataAccessLayer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("");


        //}
        public DbSet<Department> Departments { get; set; }
    }
}
