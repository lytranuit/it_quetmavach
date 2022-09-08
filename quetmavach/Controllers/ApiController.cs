
using it.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using it.Data;
using Microsoft.EntityFrameworkCore;

namespace it.Controllers
{

    public class ApiController : Controller
    {

        protected readonly ChContext _contextCh;
        protected readonly HhContext _contextHh;
        public ApiController(ChContext contextCh, HhContext contextHh)
        {
            _contextCh = contextCh;
            _contextHh = contextHh;
        }
        public IActionResult Index()
        {
            return Redirect("/Admin");
        }

        public IActionResult invoices(DateTime date_from, DateTime date_to)
        {
            //return Json(new { date_from, date_to });

            if (Request.Headers["x-api-key"] != "7xfu6jswHVgL1wjLJZ1O0t1f12C5fgr7")
            {
                return NotFound();
            }
            var list_hoadon = _contextCh.HOADON_XUATModel.Where(d => d.POC == true && d.SoHD_DT != null && d.NgayLapHD.Date >= date_from.Date && d.NgayLapHD.Date <= date_to.Date).Include(d => d.list_items).Select(d => new
            {
                d.SoHD_DT,
                ngayLapHD = d.NgayLapHD.ToString("dd/MM/yyyy"),
                d.ThueSuat,
                d.MAKH,
                d.DONVI,
                d.DIACHI,
                d.MATHUE,
                d.MACTKM,
                d.POC,
                d.tienck,
                d.thanhtien_hd,
                d.TONGTIEN,
                list_items = d.list_items.Select(e => new
                {
                    e.MaHH,
                    e.TenHH,
                    e.DVT,
                    e.SoLuong,
                    e.DonGia,
                    e.TyLeCK
                }).ToList()
            }).ToList();
            return Json(list_hoadon);
            //return Redirect("/");
        }



    }

}
