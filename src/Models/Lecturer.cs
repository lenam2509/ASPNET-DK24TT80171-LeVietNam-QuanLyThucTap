using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipManagement.Models
{
    public class Lecturer
    {
        [Key]
        [Display(Name = "Mã GV")]
        public string MaGV { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; } = string.Empty;

        [Display(Name = "Khoa/Bộ môn")]
        public string? Khoa { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
