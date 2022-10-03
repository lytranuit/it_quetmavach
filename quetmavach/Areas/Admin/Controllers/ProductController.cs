using Microsoft.AspNetCore.Mvc;
using it.Data;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Spire.Xls;
using Microsoft.AspNetCore.Identity;
using it.Areas.Admin.Models;

namespace it.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private string _type = "Product";
        protected readonly ChContext _contextCh;
        protected readonly KdContext _contextKd;
        private UserManager<UserModel> UserManager;
        public ProductController(ItContext context, ChContext contextCh, KdContext contextKd, UserManager<UserModel> UserMgr) : base(context)
        {
            _contextCh = contextCh;
            _contextKd = contextKd;
            UserManager = UserMgr;
            ViewData["controller"] = _type;

        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(string MAHH, string mavach)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await UserManager.GetUserAsync(currentUser);
            var MACH = user.MACH;
            var donvi = _context.DonviModel.Where(d => d.MACH == MACH).FirstOrDefault();
            var is_cuahang = false;
            if (donvi != null && donvi.is_cuahang == true)
                is_cuahang = true;
            if (ModelState.IsValid)
            {
                try
                {
                    if (is_cuahang)
                    {

                        var ProductModel_old = await _contextCh.ProductModel.FindAsync(MAHH);
                        ProductModel_old.mavach = mavach;
                        _contextCh.Update(ProductModel_old);
                        await _contextCh.SaveChangesAsync();
                        return Ok("1");
                    }
                    else
                    {
                        var ProductModel_old = await _contextKd.ProductModel.FindAsync(MAHH);
                        ProductModel_old.mavach = mavach;
                        _contextKd.Update(ProductModel_old);
                        await _contextKd.SaveChangesAsync();
                        return Ok("1");
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.InnerException as SqlException;
                    if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                    {

                        return Ok("Mã vạch bị trùng!");
                    }
                    else
                    {
                        throw;
                    }

                }

            }
            return Ok();
        }



        [HttpPost]
        public async Task<JsonResult> Table()
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
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var customerData = (from tempcustomer in _contextCh.ProductModel select tempcustomer);
                int recordsTotal = customerData.Count();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.TENHH.Contains(searchValue) || m.MAHH.Contains(searchValue));
                }
                int recordsFiltered = customerData.Count();
                var datapost = customerData.Skip(skip).Take(pageSize).ToList();
                var data = new ArrayList();
                foreach (var record in datapost)
                {
                    var soluong = record.SL3 != null && record.SL2 != null && record.SL2 != 0 ? Math.Round((decimal)(record.SL3 / record.SL2)) : 0;
                    var DVT3 = record.DVT3;
                    var DVT2 = record.DVT2 != null && record.DVT2 != "" ? record.DVT2 : "Hộp";
                    var data1 = new
                    {
                        action = "<div class='btn-group'></div>",
                        id = "<a class='editmavach' href='#' data-mahh='" + record.MAHH + "' data-mavach='" + record.mavach + "'  data-toggle='modal' data-animation='bounce' data-target='.modal-mavach'><i class='fas fa-pencil-alt mr-2'></i> " + record.MAHH + "</a>",
                        name = record.TENHH,
                        soluong = soluong + " " + DVT3 + "/ " + DVT2,
                        mavach = record.mavach
                    };
                    data.Add(data1);
                }
                var jsonData = new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }
            else
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var customerData = (from tempcustomer in _contextKd.ProductModel select tempcustomer);
                int recordsTotal = customerData.Count();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.TENHH.Contains(searchValue) || m.MAHH.Contains(searchValue));
                }
                int recordsFiltered = customerData.Count();
                var datapost = customerData.Skip(skip).Take(pageSize).ToList();
                var data = new ArrayList();
                foreach (var record in datapost)
                {
                    var soluong = record.SL3 != null && record.SL2 != null ? Math.Round((decimal)(record.SL3 / record.SL2)) : 0;
                    var DVT3 = record.DVT3;
                    var DVT2 = record.DVT2 != null && record.DVT2 != "" ? record.DVT2 : "Hộp";
                    var data1 = new
                    {
                        action = "<div class='btn-group'></div>",
                        id = "<a class='editmavach' href='#' data-mahh='" + record.MAHH + "' data-mavach='" + record.mavach + "'  data-toggle='modal' data-animation='bounce' data-target='.modal-mavach'><i class='fas fa-pencil-alt mr-2'></i> " + record.MAHH + "</a>",
                        name = record.TENHH,
                        soluong = soluong + " " + DVT3 + "/ " + DVT2,
                        mavach = record.mavach
                    };
                    data.Add(data1);
                }
                var jsonData = new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }

        }

        public async Task<IActionResult> import()
        {
            //return Ok();
            Workbook content = new Workbook();
            content.CalculateAllValue();
            content.LoadFromFile("./private/excel/template/TỔNG HỢP BARCODE.xlsx");
            int numberofsheet = content.Worksheets.Count;
            Console.WriteLine("Thông tin từ file Excel");
            var sheet = content.Worksheets[0];

            // đọc sheet này bắt đầu từ row 2 (0, 1 bỏ vì tiêu đề)

            var lastrow = sheet.LastRow;
            // nếu vẫn chưa gặp end thì vẫn lấy data
            Console.WriteLine(lastrow);
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await UserManager.GetUserAsync(currentUser);
            var MACH = user.MACH;
            var donvi = _context.DonviModel.Where(d => d.MACH == MACH).FirstOrDefault();
            var is_cuahang = false;
            if (donvi != null && donvi.is_cuahang == true)
                is_cuahang = true;
            for (int rowIndex = 4; rowIndex < lastrow; rowIndex++)
            {
                // lấy row hiện tại
                var nowRow = sheet.Rows[rowIndex];
                if (nowRow == null)
                    continue;
                // vì ta dùng 3 cột A, B, C => data của ta sẽ như sau
                //int numcount = nowRow.Cells.Count;
                //for(int y = 0;y<numcount - 1 ;y++)
                var code = nowRow.Cells[1].Value;
                var barcode = nowRow.Cells[4].Value;
                if (code == "" || barcode == "")
                {
                    continue;
                }
                var explode = decimal.Parse(barcode);
                var barcode1 = Math.Round(explode);
                code = code.ToUpper();
                // Xuất ra thông tin lên màn hình
                Console.WriteLine("MS: {0} | barcode: {1} ", code, barcode1);
                if (is_cuahang)
                {

                    var product = _contextCh.ProductModel.Where(d => d.MAHH == code).FirstOrDefault();
                    if (product == null)
                        continue;
                    product.mavach = barcode1.ToString();
                    _contextCh.Update(product);
                }
                else
                {

                    var product = _contextKd.ProductModel.Where(d => d.MAHH == code).FirstOrDefault();
                    if (product == null)
                        continue;
                    product.mavach = barcode1.ToString();
                    _contextKd.Update(product);
                }
            }
            if (is_cuahang)
            {
                _contextCh.SaveChanges();
            }
            else
            {
                _contextKd.SaveChanges();
            }
            return Ok("1");
        }

        // GET: Admin/Product/get

        public async Task<JsonResult> Get(string mavach)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await UserManager.GetUserAsync(currentUser);
            var MACH = user.MACH;
            var donvi = _context.DonviModel.Where(d => d.MACH == MACH).FirstOrDefault();
            var is_cuahang = false;
            if (donvi != null && donvi.is_cuahang == true)
                is_cuahang = true;
            if (is_cuahang == true)
            {
                var ProductModel = _contextCh.ProductModel
                                .Where(d => d.mavach == mavach)
                                .FirstOrDefault();

                if (ProductModel == null)
                {
                    return Json(new { success = 0, message = "Không tìm thấy sản phẩm" });
                }
                return Json(new { success = 1, item = ProductModel });
            }
            else
            {
                var ProductModel = _contextKd.ProductModel
                .Where(d => d.mavach == mavach)
                .FirstOrDefault();

                if (ProductModel == null)
                {
                    return Json(new { success = 0, message = "Không tìm thấy sản phẩm" });
                }
                return Json(new { success = 1, item = ProductModel });
            }

        }
    }
}
