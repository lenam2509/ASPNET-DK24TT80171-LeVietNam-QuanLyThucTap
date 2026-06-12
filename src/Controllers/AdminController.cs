using InternshipManagement.Data;
using InternshipManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            // 1. Thống kê sinh viên theo ngành
            var studentsByMajor = await _context.Students
                .GroupBy(s => s.Nganh)
                .Select(g => new { Nganh = g.Key, Count = g.Count() })
                .ToListAsync();

            // 2. Thống kê sinh viên theo doanh nghiệp (chỉ những đơn đã duyệt)
            var studentsByEnterprise = await _context.InternshipRegistrations
                .Where(r => r.TrangThai == 1 && r.Enterprise != null)
                .GroupBy(r => r.Enterprise.TenDN)
                .Select(g => new { TenDN = g.Key, Count = g.Count() })
                .ToListAsync();

            ViewBag.Majors = studentsByMajor.Select(x => string.IsNullOrEmpty(x.Nganh) ? "Khác" : x.Nganh).ToArray();
            ViewBag.MajorCounts = studentsByMajor.Select(x => x.Count).ToArray();

            ViewBag.Enterprises = studentsByEnterprise.Select(x => string.IsNullOrEmpty(x.TenDN) ? "Khác" : x.TenDN).ToArray();
            ViewBag.EnterpriseCounts = studentsByEnterprise.Select(x => x.Count).ToArray();

            return View();
        }

        // GET: Admin/Internships
        public async Task<IActionResult> Internships()
        {
            var data = await _context.InternshipRegistrations
                .Include(i => i.Student)
                .Include(i => i.Enterprise)
                .Include(i => i.Lecturer)
                .ToListAsync();

            ViewBag.Lecturers = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.Lecturers.ToListAsync(), "MaGV", "HoTen");
            
            return View(data);
        }

        // POST: Admin/Approve/5
        [HttpPost]
        public async Task<IActionResult> Approve(int id, string lecturerId)
        {
            var reg = await _context.InternshipRegistrations.FindAsync(id);
            if (reg != null)
            {
                reg.TrangThai = 1; // Approved
                reg.MaGV = lecturerId;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Internships));
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                var exists = await _context.Students.AnyAsync(s => s.MSSV == student.MSSV);
                if (exists)
                {
                    ModelState.AddModelError("MSSV", "Mã sinh viên này đã tồn tại!");
                    return View(student);
                }
                student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Thêm sinh viên thành công!";
                return RedirectToAction(nameof(CreateStudent));
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Students(string searchString, int pageNumber = 1)
        {
            int pageSize = 10;
            var students = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.MSSV.Contains(searchString) || 
                                               s.HoTen.Contains(searchString) || 
                                               s.Nganh.Contains(searchString));
            }

            ViewBag.SearchString = searchString;
            
            int totalItems = await students.CountAsync();
            var items = await students.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(string id)
        {
            if (id == null) return NotFound();
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(Student student, string newPassword)
        {
            var existingStudent = await _context.Students.FindAsync(student.MSSV);
            if (existingStudent == null) return NotFound();

            existingStudent.HoTen = student.HoTen;
            existingStudent.Lop = student.Lop;
            existingStudent.Nganh = student.Nganh;
            existingStudent.KhoaHoc = student.KhoaHoc;

            if (!string.IsNullOrEmpty(newPassword))
            {
                existingStudent.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Cập nhật thông tin sinh viên thành công!";
            return RedirectToAction(nameof(Students));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Đã xóa sinh viên thành công!";
            }
            return RedirectToAction(nameof(Students));
        }

        [HttpGet]
        public IActionResult CreateLecturer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLecturer(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                var exists = await _context.Lecturers.AnyAsync(l => l.MaGV == lecturer.MaGV);
                if (exists)
                {
                    ModelState.AddModelError("MaGV", "Mã giảng viên này đã tồn tại!");
                    return View(lecturer);
                }
                lecturer.Password = BCrypt.Net.BCrypt.HashPassword(lecturer.Password);
                _context.Lecturers.Add(lecturer);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Thêm giảng viên thành công!";
                return RedirectToAction(nameof(CreateLecturer));
            }
            return View(lecturer);
        }

        [HttpGet]
        public async Task<IActionResult> Lecturers(string searchString, int pageNumber = 1)
        {
            int pageSize = 10;
            var lecturers = _context.Lecturers.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                lecturers = lecturers.Where(l => l.MaGV.Contains(searchString) || 
                                               l.HoTen.Contains(searchString) || 
                                               l.Khoa.Contains(searchString));
            }

            ViewBag.SearchString = searchString;
            
            int totalItems = await lecturers.CountAsync();
            var items = await lecturers.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> EditLecturer(string id)
        {
            if (id == null) return NotFound();
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> EditLecturer(Lecturer lecturer, string newPassword)
        {
            var existingLecturer = await _context.Lecturers.FindAsync(lecturer.MaGV);
            if (existingLecturer == null) return NotFound();

            existingLecturer.HoTen = lecturer.HoTen;
            existingLecturer.Khoa = lecturer.Khoa;

            if (!string.IsNullOrEmpty(newPassword))
            {
                existingLecturer.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Cập nhật thông tin giảng viên thành công!";
            return RedirectToAction(nameof(Lecturers));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLecturer(string id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer != null)
            {
                _context.Lecturers.Remove(lecturer);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Đã xóa giảng viên thành công!";
            }
            return RedirectToAction(nameof(Lecturers));
        }

        [HttpGet]
        public IActionResult CreateEnterprise()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnterprise(Enterprise enterprise)
        {
            if (ModelState.IsValid)
            {
                _context.Enterprises.Add(enterprise);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Thêm doanh nghiệp thành công!";
                return RedirectToAction(nameof(CreateEnterprise));
            }
            return View(enterprise);
        }
        [HttpGet]
        public async Task<IActionResult> Enterprises(string searchString, int pageNumber = 1)
        {
            int pageSize = 10;
            var enterprises = _context.Enterprises.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                enterprises = enterprises.Where(e => e.TenDN.Contains(searchString) || 
                                                     e.DiaChi.Contains(searchString));
            }

            ViewBag.SearchString = searchString;
            
            int totalItems = await enterprises.CountAsync();
            var items = await enterprises.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> EditEnterprise(int id)
        {
            var enterprise = await _context.Enterprises.FindAsync(id);
            if (enterprise == null) return NotFound();
            return View(enterprise);
        }

        [HttpPost]
        public async Task<IActionResult> EditEnterprise(Enterprise enterprise)
        {
            if (ModelState.IsValid)
            {
                var existingEnterprise = await _context.Enterprises.FindAsync(enterprise.MaDN);
                if (existingEnterprise == null) return NotFound();

                existingEnterprise.TenDN = enterprise.TenDN;
                existingEnterprise.DiaChi = enterprise.DiaChi;

                await _context.SaveChangesAsync();
                TempData["Message"] = "Cập nhật thông tin doanh nghiệp thành công!";
                return RedirectToAction(nameof(Enterprises));
            }
            return View(enterprise);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEnterprise(int id)
        {
            var enterprise = await _context.Enterprises.FindAsync(id);
            if (enterprise != null)
            {
                _context.Enterprises.Remove(enterprise);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Đã xóa doanh nghiệp thành công!";
            }
            return RedirectToAction(nameof(Enterprises));
        }
    }
}
