using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipManagement.Models
{
    public class InternshipRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "MSSV")]
        public string MSSV { get; set; } = string.Empty;

        [ForeignKey("MSSV")]
        public virtual Student? Student { get; set; }

        [Required]
        [Display(Name = "Doanh nghiệp")]
        public int MaDN { get; set; }

        [ForeignKey("MaDN")]
        public virtual Enterprise? Enterprise { get; set; }

        [Display(Name = "Người hướng dẫn tại DN")]
        public string? NguoiHuongDanDN { get; set; }

        [Display(Name = "Thời gian thực tập")]
        public string? ThoiGianThucTap { get; set; }

        /// <summary>
        /// 0 - Chờ duyệt, 1 - Đã duyệt, 2 - Từ chối
        /// </summary>
        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; } = 0; 

        [Display(Name = "Giảng viên hướng dẫn")]
        public string? MaGV { get; set; }

        [ForeignKey("MaGV")]
        public virtual Lecturer? Lecturer { get; set; }

        [Display(Name = "Nhận xét của GV")]
        public string? NhanXetGV { get; set; }

        [Display(Name = "Điểm")]
        public double? Diem { get; set; }

        [Display(Name = "File báo cáo")]
        public string? FileBaoCao { get; set; }
    }
}
