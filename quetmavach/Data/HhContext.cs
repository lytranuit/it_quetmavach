using it.Models;
using Microsoft.EntityFrameworkCore;
using it.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace it.Data
{
    public class HhContext : DbContext
    {
        public HhContext(DbContextOptions<HhContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> ProductModel { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().ToTable("TBL_DANHMUCHANGHOA");
        }
    }
}
