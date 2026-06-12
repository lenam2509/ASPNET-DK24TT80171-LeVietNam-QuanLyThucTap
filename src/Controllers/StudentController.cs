using InternshipManagement.Data;
using InternshipManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;

namespace InternshipManagement.Controllers
{
    [Authorize(Roles = "Student,Admin")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StudentController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Student (Trang cá nhân sinh viên)
        public async Task<IActionResult> Index()
        {
            var mssv = User.Identity?.Name;
            var student = await _context.Students.FirstOrDefaultAsync(s => s.MSSV == mssv);
            var registration = await _context.InternshipRegistrations
                                        .Include(r => r.Enterprise)
                                        .Include(r => r.Lecturer)
                                        .FirstOrDefaultAsync(r => r.MSSV == mssv);

            ViewBag.Registration = registration;
            return View(student);
        }

        // GET: Student/RegisterInternship/5
        public IActionResult RegisterInternship(string id)
        {
            if (id == null) return NotFound();

            var student = _context.Students.FirstOrDefault(s => s.MSSV == id);
            if (student == null) return NotFound();

            ViewData["MaDN"] = new SelectList(_context.Enterprises, "MaDN", "TenDN");
            var registration = new InternshipRegistration { MSSV = id };
            return View(registration);
        }

        // POST: Student/RegisterInternship
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterInternship(InternshipRegistration registration)
        {
            ModelState.Remove("Student");
            ModelState.Remove("Enterprise");
            ModelState.Remove("Lecturer");

            if (ModelState.IsValid)
            {
                registration.TrangThai = 0; // Pending
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDN"] = new SelectList(_context.Enterprises, "MaDN", "TenDN", registration.MaDN);
            return View(registration);
        }

        [HttpPost]
        public async Task<IActionResult> UploadReport(IFormFile reportFile)
        {
            var mssv = User.Identity?.Name;
            var reg = await _context.InternshipRegistrations.FirstOrDefaultAsync(r => r.MSSV == mssv && r.TrangThai == 1);
            
            if (reg == null) return NotFound("Không tìm thấy đơn đăng ký hợp lệ hoặc chưa được duyệt.");

            if (reportFile != null && reportFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(reportFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await reportFile.CopyToAsync(fileStream);
                }

                reg.FileBaoCao = "/uploads/" + uniqueFileName;
                await _context.SaveChangesAsync();
                
                TempData["Message"] = "Tải lên báo cáo thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
