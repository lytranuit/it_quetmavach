using it.Models;
using Microsoft.EntityFrameworkCore;
using it.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace it.Data
{
    public class HhContext : DbContext
    {
        private readonly ItContext _context;
        private IActionContextAccessor actionAccessor;
        private UserManager<UserModel> UserManager;
        public HhContext(DbContextOptions<HhContext> options, ItContext context, UserManager<UserModel> UserMgr, IActionContextAccessor ActionAccessor) : base(options)
        {
            _context = context;
            actionAccessor = ActionAccessor;
            UserManager = UserMgr;
        }

        //public virtual DbSet<ProductModel> ProductModel { get; set; }
        //public virtual DbSet<ProductChModel> ProductChModel { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProductModel>().ToTable("TBL_DANHMUCHANGHOA");
            //modelBuilder.Entity<ProductChModel>().ToTable("DM_HANGHOA");
        }
    }
}
