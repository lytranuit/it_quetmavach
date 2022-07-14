using Microsoft.AspNetCore.Mvc;
using it.Data;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace it.Areas.Admin.Controllers
{
    public class OrderChController : BaseController
    {
        protected readonly ChContext _contextCh;
        protected readonly HhContext _contextHh;
        public OrderChController(ItContext context, ChContext contextCh, HhContext contextHh) : base(context)
        {
            _contextCh = contextCh;
            _contextHh = contextHh;
        }

        // GET: Admin/Order
        public async Task<IActionResult> Index()
        {

            return View();
        }
        // GET: Admin/Order/get

        public async Task<JsonResult> Get(string sohd)
        {
            var HOADON_XUATModel = _contextCh.HOADON_XUATModel
                .Where(d => d.SoHD == sohd)
                .FirstOrDefault();

            if (HOADON_XUATModel == null)
            {
                return Json(new { success = 0, message = "Không tìm thấy hóa đơn" });
            }

            HOADON_XUATModel.list_items = _contextCh.CT_HOADON_XUATModel
            .Where(d => d.SoHD == sohd)
            .OrderBy(d => d.stt)
            .ToList();

            foreach (var item in HOADON_XUATModel.list_items)
            {
                item.product = _contextHh.ProductModel
                                .Where(d => d.MAHH == item.MaHH).FirstOrDefault();
            }

            return Json(new { success = 1, item = HOADON_XUATModel });
        }

        [HttpPost]
        public async Task<JsonResult> successkt(string sohd)
        {

            var CT_HOADON_XUAT = _contextCh.CT_HOADON_XUATModel
            .Where(d => d.SoHD == sohd)
            .ToList();
            foreach (var item in CT_HOADON_XUAT)
            {
                item.KTKHO = true;
                _contextCh.Update(item);
            }
            _contextCh.SaveChanges();
            return Json(new { success = 1 });
        }
    }
}
