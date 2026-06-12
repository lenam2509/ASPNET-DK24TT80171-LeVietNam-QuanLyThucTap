using InternshipManagement.Data;
using InternshipManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InternshipManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string mssv, string password)
        {
            // 1. Check Admin (Hardcoded)
            if (mssv == "admin" && password == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim("FullName", "Quản trị viên"),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                await SignIn(claims);
                return RedirectToAction("Internships", "Admin");
            }

            // 2. Check Lecturer
            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(l => l.MaGV == mssv);
            if (lecturer != null && BCrypt.Net.BCrypt.Verify(password, lecturer.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, lecturer.MaGV),
                    new Claim("FullName", lecturer.HoTen),
                    new Claim(ClaimTypes.Role, "Lecturer")
                };
                await SignIn(claims);
                return RedirectToAction("Index", "Lecturer");
            }

            // 3. Check Student
            var student = await _context.Students.FirstOrDefaultAsync(s => s.MSSV == mssv);
            if (student != null && BCrypt.Net.BCrypt.Verify(password, student.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, student.MSSV),
                    new Claim("FullName", student.HoTen),
                    new Claim(ClaimTypes.Role, "Student")
                };
                await SignIn(claims);
                return RedirectToAction("Index", "Student");
            }

            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }

        private async Task SignIn(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("Cookies", principal);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login", "Auth");
        }
    }
}
