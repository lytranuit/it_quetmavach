using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace it.Areas.Admin.Models
{
    [Table("TBL_DANHMUCDONVI")]
    public class DonviModel
    {
        [Required]
        [Key]
        public string MACH { get; set; }

        public string TEN { get; set; }
        public string DULIEU { get; set; }
        public string DIACHI { get; set; }
        public string PASS { get; set; }
        public string VUNG { get; set; }
        public string MACN { get; set; }
        public bool? HIENTHI { get; set; }
        public bool? is_cuahang { get; set; }
    }
}
