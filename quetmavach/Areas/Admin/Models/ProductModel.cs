using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace it.Areas.Admin.Models
{
    public class ProductModel
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
        public decimal? SL3 { get; set; }
        public decimal? SL2 { get; set; }
    }
}
