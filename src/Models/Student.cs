using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipManagement.Models
{
    public class Student
    {
        [Key]
        [Display(Name = "MSSV")]
        public string MSSV { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; } = string.Empty;

        [Display(Name = "Lớp")]
        public string? Lop { get; set; }

        [Display(Name = "Ngành")]
        public string? Nganh { get; set; }

        [Display(Name = "Khóa học")]
        public string? KhoaHoc { get; set; }

        [Display(Name = "Học kỳ")]
        public string? HocKy { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
