using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace it.Areas.Admin.Models
{
    public class HOADON_XUATModel
    {
        [Required]
        [Key]
        public string SoHD { get; set; }

        [NotMapped]
        public virtual List<CT_HOADON_XUATModel>? list_items { get; set; }

        public string MaCH { get; set; }

        public string? kihieu { get; set; }
        public decimal? TONGTIEN { get; set; }
        public decimal? thanhtien_hd { get; set; }

        public string? MAKH { get; set; }
        public string? DONVI { get; set; }
        public string? DIACHI { get; set; }
        public DateTime? NGAYGIAOHANG { get; set; }

    }
}
