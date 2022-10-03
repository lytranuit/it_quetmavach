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
        public DbSet<ProductChModel> ProductModel { get; set; }
        //public DbSet<User2Model> User2Model { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductChModel>().ToTable("DM_HANGHOA");
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
                if (user_id != null)
                {

                    var user = _context.UserModel.Where(d => d.Id == user_id).FirstOrDefault();
                    if (user != null)
                    {
                        var donvi = _context.DonviModel.Where(d => d.MACH == user.MACH).FirstOrDefault();
                        //var MACH = "KT_QN";
                        var connString = "data source=" + donvi.DIACHI + ";initial catalog=" + donvi.DULIEU + ";persist security info=True;user id=sa;password=" + donvi.PASS + ";MultipleActiveResultSets=True;";
                        optionsBuilder.UseSqlServer(connString);
                    }
                    else
                    {

                    }
                }
                else
                {
                    var connString = "data source=172.16.1.4;initial catalog=PTTT;persist security info=True;user id=sa;password=PMP_IT123456;MultipleActiveResultSets=True;";
                    //var connString = "data source=localhost;initial catalog=PTTT;persist security info=True;user id=sa;password=!PMP_it123456;MultipleActiveResultSets=True;";
                    optionsBuilder.UseSqlServer(connString);
                }
            }

        }
    }
}
