using it.Models;
using Microsoft.EntityFrameworkCore;
using it.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace it.Data
{
    public class ChContext : DbContext
    {
        private readonly ItContext _context;
        private IActionContextAccessor actionAccessor;
        private UserManager<UserModel> UserManager;
        public ChContext(DbContextOptions<ChContext> options, ItContext context, UserManager<UserModel> UserMgr, IActionContextAccessor ActionAccessor) : base(options)
        {
            _context = context;
            actionAccessor = ActionAccessor;
            UserManager = UserMgr;
        }

        public DbSet<CT_HOADON_XUATModel> CT_HOADON_XUATModel { get; set; }
        public DbSet<HOADON_XUATModel> HOADON_XUATModel { get; set; }
        //public DbSet<User2Model> User2Model { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HOADON_XUATModel>().ToTable("HOADON_XUAT");
            modelBuilder.Entity<CT_HOADON_XUATModel>().ToTable("CT_HOADON_XUAT").HasKey(table => new
            {
                table.SoHD,
                table.MaHH,
                table.MaCH
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var user_http = actionAccessor.ActionContext.HttpContext.User;
                var user_id = UserManager.GetUserId(user_http);
                var user = _context.UserModel.Where(d => d.Id == user_id).FirstOrDefault();
                if (user != null)
                {
                    var donvi = _context.DonviModel.Where(d => d.MACH == user.MACH).FirstOrDefault();
                    //var MACH = "KT_QN";
                    var connString = "data source=" + donvi.DIACHI + ";initial catalog=" + donvi.DULIEU + ";persist security info=True;user id=sa;password=" + donvi.PASS + ";MultipleActiveResultSets=True;";
                    optionsBuilder.UseSqlServer(connString);
                }
            }

        }
    }
}
