using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
namespace it.Areas.Admin.Models
{
    public class CT_HOADON_XUATModel
    {
        [Key, Column(Order = 0)]
        public string SoHD { get; set; }
        [Key, Column(Order = 1)]
        public string MaHH { get; set; }
        [Key, Column(Order = 2)]
        public string MaCH { get; set; }
        public int stt { get; set; }
        public DateTime NgayLapHD { get; set; }

        public string TenHH { get; set; }
        public string? DVT { get; set; }

        public Boolean? KTKHO { get; set; }
        public decimal SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        public double? TyLeCK { get; set; }

        [NotMapped]
        public ProductModel product { get; set; }

    }
}
