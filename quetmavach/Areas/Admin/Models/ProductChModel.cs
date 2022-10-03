using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace it.Areas.Admin.Models
{
    public class ProductChModel
    {
        [Required]
        [Key]
        public string MAHH { get; set; }

        public string TENHH { get; set; }
        public string? GIABAN { get; set; }
        public string? DVT { get; set; }
        public string? mavach { get; set; }
        public string? DVT3 { get; set; }
        public string? DVT2 { get; set; }
        public string? DVT1 { get; set; }
        public int? SL3 { get; set; }
        public int? SL2 { get; set; }
        public int? SL1 { get; set; }
    }
}
