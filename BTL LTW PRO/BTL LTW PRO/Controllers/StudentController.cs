using BTL_LTW_PRO.Data;
using BTL_LTW_PRO.Models;
using BTL_LTW_PRO.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW_PRO.Controllers
{
    public class StudentController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ShowCourse()
        {
            return View();
        }

        [RoleAuthorize("Admin", "Student")]
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            string? userIdString = HttpContext.Session.GetString("UserID");
            int? UserID = null;

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedId))
            {
                UserID = parsedId;
            }


            // JOIN Courses với Users để lấy Instructor Name
            var courses = await (from c in _context.Courses
                                 join u in _context.Users on c.InstructorID equals u.UserID
                                 select new
                                 {
                                     c.CourseID,
                                     c.CourseName,
                                     c.BeginTime,
                                     c.EndTime,
                                     InstructorName = u.FullName // Lấy tên giảng viên từ bảng Users
                                 }).ToListAsync();




            return Ok(courses); // Trả về JSON
        }

        [HttpPost]
        public async Task<IActionResult> JoinClass([FromBody] string courseId)
        {
            string? userIdString = HttpContext.Session.GetString("UserID");
            int? userId = null;
            var date = DateTime.Now;

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedId))
            {
                userId = parsedId;
            }
            if (userId == null)
            {
                return Json(new { success = false, redirectUrl = Url.Action("Login", "Account") });
            }

            // Kiểm tra nếu khóa học đã kết thúc
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseID == courseId);
            if (course == null)
            {
                return Json(new { success = false, message = "Khóa học không tồn tại." });
            }

            if (course.EndTime < date)
            {
                return Json(new { success = false, message = "Khóa học đã kết thúc." });
            }

            var exists = await _context.Enrollments
                .AnyAsync(e => e.UserID == userId && e.CourseID == courseId);
            if (exists)
            {
                return Json(new { success = false, message = "Bạn đã đăng ký hoặc đang chờ duyệt lớp học này." });
            }

            // Thêm đăng ký khóa học
            var enrollment = new Enrollment
            {
                UserID = userId.Value,
                CourseID = courseId,
                Sstatus = "Pending"
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            return Json(new { success = true });
        }





        public async Task<IActionResult> ShowMyCourse()
        {
            string? userIdString = HttpContext.Session.GetString("UserID");
            int? userId = null;

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedId))
            {
                userId = parsedId;
            }
            if (userId == null)
            {
                return Json(new { success = false, redirectUrl = Url.Action("Login", "Account") });
            }

            var courses = await (from e in _context.Enrollments
                                 join c in _context.Courses on e.CourseID equals c.CourseID
                                 join u in _context.Users on c.InstructorID equals u.UserID
                                 where e.UserID == userId && e.Sstatus == "Approved"
                                 select new
                                 {
                                     c.CourseID,
                                     c.CourseName,
                                     c.BeginTime,
                                     c.EndTime,
                                     InstructorName = u.FullName
                                 }).ToListAsync();

            return Json(courses);
        }


    }
}