using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipManagement.Models
{
    public class Enterprise
    {
        [Key]
        public int MaDN { get; set; }

        [Required]
        [Display(Name = "Tên Doanh nghiệp")]
        public string TenDN { get; set; } = string.Empty;

        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }
    }
}
