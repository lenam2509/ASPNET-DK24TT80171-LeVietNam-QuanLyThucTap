using InternshipManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;

namespace InternshipManagement.Controllers
{
    [Authorize(Roles = "Lecturer,Admin")]
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lecturer/Index
        public async Task<IActionResult> Index()
        {
            var magv = User.Identity?.Name;
            var data = await _context.InternshipRegistrations
                .Include(i => i.Student)
                .Include(i => i.Enterprise)
                .Where(i => i.MaGV == magv)
                .ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Grade(int id)
        {
            var magv = User.Identity?.Name;
            var reg = await _context.InternshipRegistrations
                .Include(i => i.Student)
                .Include(i => i.Enterprise)
                .FirstOrDefaultAsync(i => i.Id == id && i.MaGV == magv);

            if (reg == null) return NotFound();
            return View(reg);
        }

        [HttpPost]
        public async Task<IActionResult> Grade(int id, double score, string comment)
        {
            var magv = User.Identity?.Name;
            var reg = await _context.InternshipRegistrations.FirstOrDefaultAsync(i => i.Id == id && i.MaGV == magv);
            if (reg != null)
            {
                reg.Diem = score;
                reg.NhanXetGV = comment;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Đã lưu nhận xét và điểm số thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
