
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using it.Areas.Admin.Models;
using it.Data;
using Microsoft.AspNetCore.Identity;

namespace it.Areas.Admin.Controllers
{

    public class HomeController : BaseController
    {
        private UserManager<UserModel> UserManager;


        public HomeController(ItContext context, UserManager<UserModel> UserMgr) : base(context)
        {
            UserManager = UserMgr;
        }
        public async Task<IActionResult> Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await UserManager.GetUserAsync(currentUser);
            var MACH = user.MACH;
            var donvi = _context.DonviModel.Where(d => d.MACH == MACH).FirstOrDefault();
            var is_cuahang = false;
            if (donvi != null && donvi.is_cuahang == true)
                is_cuahang = true;
            if (is_cuahang)
            {
                return RedirectToAction("Index", "OrderCh");
            }
            return RedirectToAction("Index", "Order");
        }


    }
}
